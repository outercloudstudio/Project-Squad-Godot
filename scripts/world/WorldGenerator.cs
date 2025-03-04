using Godot;
using Networking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class WorldGenerator : Node, NetworkPointUser {
    public class RoomPlacement {
        public enum RoomType {
            None,
            Spawn,
            Final,
            Loot
        }

        public RoomLayout RoomLayout;
        public Vector2I Location;
        public RoomType Type = RoomType.None;
        public RoomLayout.Connection EntranceConnection;
        public List<DecorationPlacement> Decorations = new List<DecorationPlacement>();
        public List<Vector2> EdgeFieldLocations = new List<Vector2>();
        public List<int> EdgeFieldDistances = new List<int>();
        public uint Seed;
        public uint Id;

        public RoomPlacement(RandomNumberGenerator random, uint id) {
            Seed = random.Randi();
            Id = id;
        }

        public virtual bool Intersects(RoomPlacement otherRoom) {
            if (otherRoom.GetTopLeftBound().X >= GetTopLeftBound().X && otherRoom.GetTopLeftBound().X < GetBottomRightBound().X && otherRoom.GetTopLeftBound().Y >= GetTopLeftBound().Y && otherRoom.GetTopLeftBound().Y < GetBottomRightBound().Y) return true;

            if (otherRoom.GetBottomRightBound().X > GetTopLeftBound().X && otherRoom.GetBottomRightBound().X <= GetBottomRightBound().X && otherRoom.GetTopLeftBound().Y >= GetTopLeftBound().Y && otherRoom.GetTopLeftBound().Y < GetBottomRightBound().Y) return true;

            if (otherRoom.GetTopLeftBound().X >= GetTopLeftBound().X && otherRoom.GetTopLeftBound().X < GetBottomRightBound().X && otherRoom.GetBottomRightBound().Y > GetTopLeftBound().Y && otherRoom.GetBottomRightBound().Y <= GetBottomRightBound().Y) return true;

            if (otherRoom.GetBottomRightBound().X > GetTopLeftBound().X && otherRoom.GetBottomRightBound().X <= GetBottomRightBound().X && otherRoom.GetBottomRightBound().Y > GetTopLeftBound().Y && otherRoom.GetBottomRightBound().Y <= GetBottomRightBound().Y) return true;


            if (GetTopLeftBound().X >= otherRoom.GetTopLeftBound().X && GetTopLeftBound().X < otherRoom.GetBottomRightBound().X && GetTopLeftBound().Y >= otherRoom.GetTopLeftBound().Y && GetTopLeftBound().Y < otherRoom.GetBottomRightBound().Y) return true;

            if (GetBottomRightBound().X > otherRoom.GetTopLeftBound().X && GetBottomRightBound().X <= otherRoom.GetBottomRightBound().X && GetTopLeftBound().Y >= otherRoom.GetTopLeftBound().Y && GetTopLeftBound().Y < otherRoom.GetBottomRightBound().Y) return true;

            if (GetTopLeftBound().X >= otherRoom.GetTopLeftBound().X && GetTopLeftBound().X < otherRoom.GetBottomRightBound().X && GetBottomRightBound().Y > otherRoom.GetTopLeftBound().Y && GetBottomRightBound().Y <= otherRoom.GetBottomRightBound().Y) return true;

            if (GetBottomRightBound().X > otherRoom.GetTopLeftBound().X && GetBottomRightBound().X <= otherRoom.GetBottomRightBound().X && GetBottomRightBound().Y > otherRoom.GetTopLeftBound().Y && GetBottomRightBound().Y <= otherRoom.GetBottomRightBound().Y) return true;


            return false;
        }

        public Vector2 GetTopLeftBound() {
            return Location + RoomLayout.TopLeftBound;
        }

        public Vector2 GetBottomRightBound() {
            return Location + RoomLayout.BottomRightBound;
        }

        public bool IsTileWallOrBounds(Vector2I location) {
            if (RoomLayout.Walls.Contains(location - Location)) return true;

            if (location.X < GetTopLeftBound().X) return true;
            if (location.Y < GetTopLeftBound().Y) return true;
            if (location.X >= GetBottomRightBound().X) return true;
            if (location.Y >= GetBottomRightBound().Y) return true;

            return false;
        }

        public int GetWidth() {
            return (int)(RoomLayout.BottomRightBound.X - RoomLayout.TopLeftBound.X);
        }

        public int GetHeight() {
            return (int)(RoomLayout.BottomRightBound.Y - RoomLayout.TopLeftBound.Y);
        }
    }

    private class BranchedRoomPlacement : RoomPlacement {
        public List<Stack<RoomPlacement>> BranchRoomPlacements;

        public BranchedRoomPlacement(RandomNumberGenerator random, uint id) : base(random, id) { }

        public override bool Intersects(RoomPlacement otherRoom) {
            if (base.Intersects(otherRoom)) return true;

            foreach (Stack<RoomPlacement> branchStack in BranchRoomPlacements) {
                foreach (RoomPlacement roomPlacement in branchStack) {
                    if (roomPlacement.Intersects(otherRoom)) return true;
                }
            }

            return false;
        }
    }

    public struct DecorationPlacement {
        public PackedScene Scene;
        public Vector2 Location;
    }

    public static WorldGenerator Me;

    public NetworkPoint NetworkPoint { get; set; } = new NetworkPoint();

    private RandomNumberGenerator _random;
    private uint _nextId;

    public override void _Ready() {
        Me = this;

        NetworkPoint.Setup(this);
    }

    public Stack<RoomPlacement> Generate(ulong seed, Biome biome) {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        _random = new RandomNumberGenerator();
        _random.Seed = seed;

        _nextId = 0;

        RoomLayout.Connection lastConnection;

        RoomLayout spawnRoomLayout = biome.SpawnRoomLayouts[0];

        Vector2 spawnRoomPlaceLocation = Vector2.Zero;
        lastConnection = spawnRoomLayout.GetConnections()[0];
        lastConnection.Location += spawnRoomPlaceLocation;

        RoomPlacement spawnRoomPlacement = new RoomPlacement(_random, _nextId) {
            RoomLayout = spawnRoomLayout,
            Location = new Vector2I((int)spawnRoomPlaceLocation.X, (int)spawnRoomPlaceLocation.Y),
            Type = RoomPlacement.RoomType.Spawn
        };
        _nextId++;

        GD.Print($"[Worldgen] Setup: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Restart();

        Stack<RoomPlacement> placedRooms = new Stack<RoomPlacement>();
        placedRooms.Push(spawnRoomPlacement);

        int size = _random.RandiRange(biome.Size.X, biome.Size.Y);

        bool result = TryPlaceRooms(biome, placedRooms, lastConnection, size - 1, size, 0);

        GD.Print($"[Worldgen] Place Rooms: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Restart();

        if (!result) return Generate(seed + 1, biome);

        placedRooms = FlattenPlacedRooms(placedRooms);

        GD.Print($"[Worldgen] Flatten placements: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Restart();

        ResolveEdgeFields(placedRooms);

        GD.Print($"[Worldgen] Resolve edge fields: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Restart();

        foreach (RoomPlacement roomPlacement in placedRooms) {
            GenerateDecorations(roomPlacement, biome);
        }

        GD.Print($"[Worldgen] Generate decorations: {stopwatch.ElapsedMilliseconds}ms");
        stopwatch.Restart();

        return placedRooms;
    }

    private bool TryPlaceRooms(Biome biome, Stack<RoomPlacement> placedRooms, RoomLayout.Connection lastConnection, int roomsToPlace, int size, int branches) {
        List<RoomLayout> roomLayouts = new List<RoomLayout>(biome.RoomLayouts).OrderBy(item => _random.Randf()).ToList();

        int roomIndex = size - roomsToPlace;

        // GD.Print("=============================== " + roomIndex);

        bool mustBranch = branches < biome.BranchRanges.Length && biome.BranchRanges.Any(range => range.Y == roomIndex);

        if (mustBranch) {
            roomLayouts = roomLayouts.Where(layout => layout.GetConnectionCount() > 2).ToList();

            // GD.Print("Must place branch " + roomIndex);
        } else {
            bool couldBranch = branches < biome.BranchRanges.Length && biome.BranchRanges[branches..biome.BranchRanges.Length].Any(range => roomIndex >= range.X);

            if (couldBranch) {
                roomLayouts = roomLayouts.OrderByDescending(layout => layout.GetConnectionCount() <= 2 ? 1 : -1).ToList();

                // GD.Print("Can place branch " + roomIndex);
            } else {
                roomLayouts = roomLayouts.Where(layout => layout.GetConnectionCount() <= 2).ToList();

                // GD.Print("Can not place branch " + roomIndex);
            }
        }

        if (roomsToPlace == 1) roomLayouts = new List<RoomLayout>(biome.FinalRoomLayouts).OrderBy(item => _random.Randf()).ToList();

        foreach (RoomLayout roomLayout in roomLayouts) {
            // GD.Print("---------- " + roomLayout.ResourcePath.Substring("res://generated/rooms/".Length));

            List<RoomLayout.Connection> connections = roomLayout.GetConnections().ToList();
            var validConnections = connections.Where(connection => connection.Direction == new Vector2(-lastConnection.Direction.X, -lastConnection.Direction.Y));

            if (validConnections.Count() == 0) {
                // GD.Print("NO VALID CONNECTIONS!");

                continue;
            }

            RoomLayout.Connection targetConnection = validConnections.First();

            connections.Remove(targetConnection);

            Vector2 placeLocation = lastConnection.Location - targetConnection.Location;

            RoomPlacement placement = new RoomPlacement(_random, _nextId) {
                RoomLayout = roomLayout,
                Location = new Vector2I((int)placeLocation.X, (int)placeLocation.Y),
                EntranceConnection = targetConnection
            };
            _nextId++;

            if (roomsToPlace == 1) placement.Type = RoomPlacement.RoomType.Final;

            bool valid = true;

            foreach (RoomPlacement otherRoom in placedRooms) {
                if (!otherRoom.Intersects(placement)) continue;

                valid = false;

                break;
            }

            if (!valid) {
                // GD.Print("COLLIDES!");

                continue;
            }

            // GD.Print("Found one valid placement!");

            placedRooms.Push(placement);

            if (roomsToPlace == 1) return true;

            connections = connections.OrderBy(item => _random.Randf()).ToList();

            RoomLayout.Connection nextConnection = new RoomLayout.Connection {
                Location = connections[0].Location + placeLocation,
                Direction = connections[0].Direction
            };

            if (connections.Count > 1) {
                bool nextConnectionValid = false;

                for (int nextConnectionIndex = 0; nextConnectionIndex < connections.Count; nextConnectionIndex++) {
                    // GD.Print("Attempting next connection index " + nextConnection);

                    nextConnection = new RoomLayout.Connection {
                        Location = connections[nextConnectionIndex].Location + placeLocation,
                        Direction = connections[nextConnectionIndex].Direction
                    };

                    BranchedRoomPlacement branchedRoomPlacement = new BranchedRoomPlacement(_random, _nextId) {
                        RoomLayout = placement.RoomLayout,
                        Location = placement.Location,
                        EntranceConnection = placement.EntranceConnection,
                        BranchRoomPlacements = new List<Stack<RoomPlacement>>(),
                    };
                    _nextId++;

                    bool branchesValid = true;

                    for (int localBranchConnectionIndex = 0; localBranchConnectionIndex < connections.Count; localBranchConnectionIndex++) {
                        if (localBranchConnectionIndex == nextConnectionIndex) continue;

                        RoomLayout.Connection localBranchConnection = connections[localBranchConnectionIndex];

                        Stack<RoomPlacement> branchStack = new Stack<RoomPlacement>();

                        RoomLayout.Connection branchConnection = new RoomLayout.Connection {
                            Location = localBranchConnection.Location + placeLocation,
                            Direction = localBranchConnection.Direction
                        };

                        // GD.Print("Branching in direction " + branchConnection.Direction);

                        bool branchResult = TryPlaceBranchRooms(biome, placedRooms, branchStack, branchConnection, _random.RandiRange(biome.BranchSize.X, biome.BranchSize.Y));

                        // GD.Print("Got branch result " + branchResult);

                        if (!branchResult) {
                            branchesValid = false;

                            break;
                        }

                        branchedRoomPlacement.BranchRoomPlacements.Add(branchStack);
                    }

                    // GD.Print("Branches valid " + branchesValid);

                    if (!branchesValid) {
                        // GD.Print("NO VALID BRANCHES");

                        continue;
                    }

                    nextConnectionValid = true;

                    branches++;

                    placedRooms.Pop();
                    placedRooms.Push(branchedRoomPlacement);

                    // GD.Print("Found valid placements with branches");

                    break;
                }

                if (!nextConnectionValid) {
                    // GD.Print("NO VALID BRANCHES IN ALL CONNECTIONS");

                    placedRooms.Pop();

                    continue;
                }
            }

            bool result = TryPlaceRooms(biome, placedRooms, nextConnection, roomsToPlace - 1, size, branches);

            if (result) return true;

            if (connections.Count > 1) branches--;

            placedRooms.Pop();

            // GD.Print("xxxxxxxxxxxxxxx Couldn't find a placement! " + roomIndex + " " + branches);
        }

        return false;
    }

    private bool TryPlaceBranchRooms(Biome biome, Stack<RoomPlacement> placedRooms, Stack<RoomPlacement> branchPlacedRooms, RoomLayout.Connection lastConnection, int roomsToPlace) {
        List<RoomLayout> roomLayouts = new List<RoomLayout>(biome.RoomLayouts).OrderBy(item => _random.Randf()).Where(layout => layout.GetConnectionCount() <= 2).ToList();

        if (roomsToPlace == 1) roomLayouts = new List<RoomLayout>(biome.FinalBranchRoomLayouts).OrderBy(item => _random.Randf()).ToList();

        // GD.Print("BRANCH: =============================== " + roomsToPlace);

        foreach (RoomLayout roomLayout in roomLayouts) {
            // GD.Print("BRANCH: ---------- " + roomLayout.ResourcePath.Substring("res://generated/rooms/".Length));

            List<RoomLayout.Connection> connections = roomLayout.GetConnections().ToList();
            var validConnections = connections.Where(connection => connection.Direction == new Vector2(-lastConnection.Direction.X, -lastConnection.Direction.Y));

            if (validConnections.Count() == 0) {
                // GD.Print("BRANCH: NO VALID CONNECTIONS!");

                continue;
            }

            RoomLayout.Connection targetConnection = validConnections.First();

            connections.Remove(targetConnection);

            Vector2 placeLocation = lastConnection.Location - targetConnection.Location;

            RoomPlacement placement = new RoomPlacement(_random, _nextId) {
                RoomLayout = roomLayout,
                Location = new Vector2I((int)placeLocation.X, (int)placeLocation.Y),
                EntranceConnection = targetConnection
            };
            _nextId++;

            if (roomsToPlace == 1) placement.Type = RoomPlacement.RoomType.Loot;

            bool valid = true;

            foreach (RoomPlacement otherRoom in placedRooms) {
                if (!otherRoom.Intersects(placement)) continue;

                valid = false;

                break;
            }

            if (!valid) {
                // GD.Print("BRANCH: COLLIDES MAIN!");

                continue;
            }

            foreach (RoomPlacement otherRoom in branchPlacedRooms) {
                if (!otherRoom.Intersects(placement)) continue;

                valid = false;

                break;
            }

            if (!valid) {
                // GD.Print("BRANCH: COLLIDES BRANCH!");

                continue;
            }

            // GD.Print("BRANCH: Found one valid placement!");

            branchPlacedRooms.Push(placement);

            if (roomsToPlace == 1) return true;

            RoomLayout.Connection nextConnection = new RoomLayout.Connection {
                Location = connections[0].Location + placeLocation,
                Direction = connections[0].Direction
            };

            bool result = TryPlaceBranchRooms(biome, placedRooms, branchPlacedRooms, nextConnection, roomsToPlace - 1);

            if (result) return true;

            // GD.Print("BRANCH: xxxxxxxxxxxxxxx Couldn't find a placement! " + roomsToPlace);

            branchPlacedRooms.Pop();
        }

        // GD.Print("BRANCH: xxxxxxxxxxxxxxx Exiting " + roomsToPlace + " with fail");

        return false;
    }

    private void GenerateDecorations(RoomPlacement roomPlacement, Biome biome) {
        List<DecorationPlacement> decorations = new List<DecorationPlacement>();

        List<Vector2I> openDecorationLocations = new List<Vector2I>();

        foreach (Vector2 location in roomPlacement.RoomLayout.SpawnLocations) {
            openDecorationLocations.Add((Vector2I)(location + roomPlacement.Location));
        }

        foreach (Decoration decoration in biome.Decorations) {
            decorations.AddRange(decoration.Generate(roomPlacement, openDecorationLocations, _random));
        }

        List<Vector2I> occupiedDecorationLocations = new List<Vector2I>();

        foreach (EdgeDecoration decoration in biome.EdgeDecorations) {
            decorations.AddRange(decoration.Generate(roomPlacement, occupiedDecorationLocations, _random));
        }

        roomPlacement.Decorations = decorations;
    }

    private void ResolveEdgeFields(Stack<RoomPlacement> placedRooms) {
        Dictionary<Vector2, int> edgeField = new Dictionary<Vector2, int>();

        foreach (RoomPlacement roomPlacement in placedRooms) {
            if (roomPlacement.RoomLayout.EdgeFieldPosition == null) continue;

            for (int index = 0; index < roomPlacement.RoomLayout.EdgeFieldPosition.Length; index++) {
                Vector2 location = roomPlacement.RoomLayout.EdgeFieldPosition[index] + roomPlacement.Location;

                if (edgeField.ContainsKey(location)) {
                    edgeField[location] = Math.Min(roomPlacement.RoomLayout.EdgeFieldDistance[index], edgeField[location]);
                } else {
                    edgeField.Add(location, roomPlacement.RoomLayout.EdgeFieldDistance[index]);
                }
            }
        }

        foreach (RoomPlacement roomPlacement in placedRooms) {
            if (roomPlacement.RoomLayout.EdgeFieldPosition == null) continue;

            for (int index = 0; index < roomPlacement.RoomLayout.EdgeFieldPosition.Length; index++) {
                Vector2 location = roomPlacement.RoomLayout.EdgeFieldPosition[index] + roomPlacement.Location;

                if (!edgeField.ContainsKey(location)) continue;

                if (edgeField[location] != roomPlacement.RoomLayout.EdgeFieldDistance[index]) continue;

                roomPlacement.EdgeFieldLocations.Add(location);
                roomPlacement.EdgeFieldDistances.Add(edgeField[location]);

                edgeField.Remove(location);
            }
        }
    }

    private Stack<RoomPlacement> FlattenPlacedRooms(Stack<RoomPlacement> placedRooms) {
        Stack<RoomPlacement> flattenedRoomPlacements = new Stack<RoomPlacement>();

        foreach (RoomPlacement roomPlacement in placedRooms) {
            flattenedRoomPlacements.Push(roomPlacement);

            if (roomPlacement is BranchedRoomPlacement branchedRoomPlacement) {
                foreach (Stack<RoomPlacement> branches in branchedRoomPlacement.BranchRoomPlacements) {
                    foreach (RoomPlacement branchRoomPlacement in branches) {
                        flattenedRoomPlacements.Push(branchRoomPlacement);
                    }
                }
            }
        }

        return flattenedRoomPlacements;
    }
}
