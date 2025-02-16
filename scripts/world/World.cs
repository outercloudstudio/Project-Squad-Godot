using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Networking;
using Riptide;

public partial class World : Node2D, NetworkPointUser {
    public static World Me;

    public TileMapLayer WallsTileMapLayer;
    public TileMapLayer RoofsTileMapLayer;
    public TileMapLayer ShadowsTileMapLayer;
    public TileMapLayer FloorsTileMapLayer;
    public TileMapLayer GrassTileMapLayer;

    public static Action<Rect2> RoomUnloaded;

    public NetworkPoint NetworkPoint { get; set; } = new NetworkPoint();

    private Biome[] _biomes;
    private Biome _activeBiome;
    private List<LoadableRoom> _unloadedRooms = new List<LoadableRoom>();
    private Dictionary<LoadableRoom, float> _loadedRooms = new Dictionary<LoadableRoom, float>();

    public override void _Ready() {
        Me = this;

        NetworkPoint.Setup(this);

        NetworkPoint.Register(nameof(SpawnEnemyRpc), SpawnEnemyRpc);
        NetworkPoint.Register(nameof(LoadRoomRpc), LoadRoomRpc);
        NetworkPoint.Register(nameof(UnloadRoomRpc), UnloadRoomRpc);

        WallsTileMapLayer = GetNode<TileMapLayer>("Walls");
        RoofsTileMapLayer = GetNode<TileMapLayer>("Roofs");
        ShadowsTileMapLayer = GetNode<TileMapLayer>("Shadows");
        FloorsTileMapLayer = GetNode<TileMapLayer>("Floors");
        GrassTileMapLayer = GetNode<TileMapLayer>("Grass");

        _biomes = new Biome[] { AssetManager.Get<Biome>("biome.golden_grove") };

        foreach (Biome biome in _biomes) {
            biome.Load();
        }
    }

    public override void _Process(double delta) {
        if (!NetworkManager.IsHost) return;

        foreach (Player player in Player.Players) {
            Load(player.GlobalPosition);
        }

        Unload((float)delta);
    }

    public static void Start() {
        Me._loadedRooms = new Dictionary<LoadableRoom, float>();
        Me._unloadedRooms = new List<LoadableRoom>();

        Me._activeBiome = Me._biomes[0];

        Stack<WorldGenerator.RoomPlacement> roomPlacements = WorldGenerator.Me.Generate(Game.Seed, Me._activeBiome);

        foreach (WorldGenerator.RoomPlacement roomPlacement in roomPlacements) {
            Me._unloadedRooms.Add(new LoadableRoom(roomPlacement, Me, Me._activeBiome));
        }

        if (!NetworkManager.IsHost) return;

        Me.Load(Vector2.Zero);
    }

    public static void Cleanup() {
        foreach (LoadableRoom loadableRoom in Me._loadedRooms.Keys) {
            loadableRoom.Unload();
            RoomUnloaded.Invoke(new Rect2(loadableRoom.RoomPlacement.GetTopLeftBound() * 16f, (loadableRoom.RoomPlacement.GetBottomRightBound() - loadableRoom.RoomPlacement.GetTopLeftBound()) * 16f));
        }
    }

    private void Load(Vector2 location) {
        List<LoadableRoom> loadedRooms = _loadedRooms.Keys.ToList();

        foreach (LoadableRoom room in loadedRooms) {
            if (location.DistanceTo(room.RoomPlacement.Location * 16) > 600) continue;

            _loadedRooms[room] = 10;
        }

        for (int index = 0; index < _unloadedRooms.Count; index++) {
            LoadableRoom loadableRoom = _unloadedRooms[index];

            if (loadableRoom.QueueLoaded) continue;

            if (location.DistanceTo(loadableRoom.RoomPlacement.Location * 16) > 600) continue;

            NetworkPoint.SendRpcToClients(nameof(LoadRoomRpc), message => message.AddUInt(loadableRoom.Id));
            loadableRoom.QueueLoaded = true;
        }
    }

    private void Unload(float delta) {
        List<LoadableRoom> loadedRooms = _loadedRooms.Keys.ToList();

        foreach (LoadableRoom room in loadedRooms) {
            if (!room.QueueLoaded) continue;

            _loadedRooms[room] -= delta;

            if (_loadedRooms[room] > 0) {
                room.Update(delta);

                continue;
            }

            NetworkPoint.SendRpcToClients(nameof(UnloadRoomRpc), message => message.AddUInt(room.Id));
            room.QueueLoaded = false;
        }
    }

    private LoadableRoom GetRoom(uint Id) {
        return _loadedRooms.Keys.ToList().Find(room => room.Id == Id);
    }

    public void LoadRoomRpc(Message message) {
        uint id = message.GetUInt();

        int index = _unloadedRooms.FindIndex(room => room.Id == id);
        LoadableRoom loadableRoom = _unloadedRooms[index];

        _unloadedRooms.RemoveAt(index);
        _loadedRooms.Add(loadableRoom, 10);

        loadableRoom.Load();
    }

    public void UnloadRoomRpc(Message message) {
        uint id = message.GetUInt();

        LoadableRoom room = GetRoom(id);

        _loadedRooms.Remove(room);
        _unloadedRooms.Add(room);

        room.Unload();

        RoomUnloaded.Invoke(new Rect2(room.RoomPlacement.GetTopLeftBound() * 16f, (room.RoomPlacement.GetBottomRightBound() - room.RoomPlacement.GetTopLeftBound()) * 16f));
    }

    public void SpawnEnemyRpc(Message message) {
        Vector2 position = new Vector2(message.GetFloat(), message.GetFloat());
        string enemyScenePath = message.GetString();
        uint roomId = message.GetUInt();

        Enemy enemy = NetworkManager.SpawnNetworkSafe<Enemy>(ResourceLoader.Load<PackedScene>(enemyScenePath), "Enemy");

        PackedScene spawnDustScene = ResourceLoader.Load<PackedScene>("res://scenes/particles/spawn_dust.tscn");
        Node2D spawnDust = spawnDustScene.Instantiate<Node2D>();

        AddChild(spawnDust);

        spawnDust.GlobalPosition = position;

        LoadableRoom room = GetRoom(roomId);
        room.AddEnemy(enemy);

        Delay.Execute(1, () => {
            if (!IsInstanceValid(this)) return;

            AddChild(enemy);

            enemy.GlobalPosition = position;

            enemy.Activate();
        });
    }
}
