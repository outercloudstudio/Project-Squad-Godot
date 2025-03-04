using System.Collections.Generic;
using System.Linq;
using Godot;

public class Assets {
    public static void Load() {
        AssetManager.Load("res://content");
        AssetManager.Load("res://generated");

        CreateBiomes();
    }

    public static void CreateBiomes() {
        AssetManager.Register("biome.golden_grove", new Biome() {
            Rooms = new PackedScene[] {
                AssetManager.GetScene("room.golden_grove.twist"),
                AssetManager.GetScene("room.golden_grove.turn_up_right"),
                AssetManager.GetScene("room.golden_grove.turn_up_left"),
                AssetManager.GetScene("room.golden_grove.turn_down_right"),
                AssetManager.GetScene("room.golden_grove.turn_down_left"),
                AssetManager.GetScene("room.golden_grove.plain"),
                AssetManager.GetScene("room.golden_grove.horizontal"),
                AssetManager.GetScene("room.golden_grove.branch"),
                AssetManager.GetScene("room.golden_grove.branch_left"),
                AssetManager.GetScene("room.golden_grove.large"),
                AssetManager.GetScene("room.golden_grove.obstacle"),
            },
            SpawnRooms = new PackedScene[] {
                 AssetManager.GetScene("room.golden_grove.spawn")
            },
            FinalRooms = new PackedScene[] {
                AssetManager.GetScene("room.golden_grove.final")
            },
            FinalBranchRooms = new PackedScene[] {
                AssetManager.GetScene("room.golden_grove.branch_end_left"),
                AssetManager.GetScene("room.golden_grove.branch_end_right"),
                AssetManager.GetScene("room.golden_grove.branch_end_up"),
            },
            BranchRanges = new Vector2[] {
                new Vector2(1, 3),
                new Vector2(4, 8),
                new Vector2(9, 13),
            },
            Color = new Color("#8a361e"),
            Tileset = new SmartTileset {
                TileSet = AssetManager.Get<TileSet>("tileset.golden_grove"),
                Tiles = new SmartTile[] {
                    new SmartWallTile() {
                        Id = "walls",
                        WallCenter = new Vector2(1, 6)
                    },
                    new SmartRoofTile() {
                        Id = "roofs",
                        RoofCenter = new Vector2(1, 1)
                    },
                    new SmartSimpleTile() {
                        Id = "floors",
                        SimpleTile = new Vector2(4, 0)
                    },
                    new SmartShadowTile() {
                        Id = "shadows",
                        ShadowCenter = new Vector2(1, 8)
                    },
                    new SmartHalfShiftTile() {
                        Id = "grass",
                        Center = new Vector2(1, 10),
                        Modifiers = new SmartTile.Modifier[] {
                            CreateRandomVariantModifier(
                                new Vector2(0, 0),
                                new Vector2[] {
                                    new Vector2(-1, 2),
                                    new Vector2(0, 2),
                                    new Vector2(1, 2),
                                }
                            )
                        }
                    }
                }
            },
            EnemyPool = new EnemyPool {
                Entries = new EnemyPool.Entry[] {
                    new EnemyPool.Entry(AssetManager.GetScene("enemy.slime"), 1f),
                    new EnemyPool.Entry(AssetManager.GetScene("enemy.stone_golem"), 1f),
                    new EnemyPool.Entry(AssetManager.GetScene("enemy.crow"), 0.3f),
                    new EnemyPool.Entry(AssetManager.GetScene("enemy.log_spirit"), 2f),
                }
            },
            Decorations = new Decoration[] {
                new Decoration {
                    Generate = (roomPlacement, openDecorationLocations, random) => {
                        PackedScene treeScene = AssetManager.GetScene("decoration.golden_grove.tree");

                        int amount = (int)(openDecorationLocations.Count * 0.02f);

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < amount; index++) {
                            int randomLocationIndex = random.RandiRange(0, openDecorationLocations.Count - 1);

                            Vector2I location = openDecorationLocations[randomLocationIndex];

                            openDecorationLocations.Remove(location);

                            placements.Add(new WorldGenerator.DecorationPlacement {
                                Location = location * 16 + Vector2.One * 8f,
                                Scene = treeScene
                            });
                        }

                        return placements;
                    }
                },
                new Decoration {
                    Generate = (roomPlacement, openDecorationLocations, random) => {
                        PackedScene[] bushes = new PackedScene[] {
                            AssetManager.GetScene("decoration.golden_grove.bush_1"),
                            AssetManager.GetScene("decoration.golden_grove.bush_2"),
                            AssetManager.GetScene("decoration.golden_grove.bush_3"),
                        };

                        int amount = (int)(openDecorationLocations.Count * 0.02f);

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < amount; index++) {
                            int randomLocationIndex = random.RandiRange(0, openDecorationLocations.Count - 1);

                            Vector2I location = openDecorationLocations[randomLocationIndex];

                            openDecorationLocations.Remove(location);

                            placements.Add(new WorldGenerator.DecorationPlacement {
                                Location = location * 16 + Vector2.One * 8f,
                                Scene = bushes[random.RandiRange(0, bushes.Length -1)],
                            });
                        }

                        return placements;
                    }
                },
                new Decoration {
                    Generate = (roomPlacement, openDecorationLocations, random) => {
                        PackedScene[] grasTufts = new PackedScene[] {
                            AssetManager.GetScene("decoration.golden_grove.grass_tuft_1"),
                            AssetManager.GetScene("decoration.golden_grove.grass_tuft_2"),
                        };

                        int amount = (int)(openDecorationLocations.Count * 0.04f);

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < amount; index++) {
                            int randomLocationIndex = random.RandiRange(0, openDecorationLocations.Count - 1);

                            Vector2I location = openDecorationLocations[randomLocationIndex];

                            openDecorationLocations.Remove(location);

                            placements.Add(new WorldGenerator.DecorationPlacement {
                                Location = location * 16 + Vector2.One * 8f,
                                Scene = grasTufts[random.RandiRange(0, grasTufts.Length -1)],
                            });
                        }

                        return placements;
                    }
                }
            },
            EdgeDecorations = new EdgeDecoration[] {
                new EdgeDecoration {
                    Generate = (roomPlacement, occupiedDecorationLocations, random) => {
                        PackedScene tree1Scene = AssetManager.GetScene("decoration.golden_grove.edge.tree_1");
                        PackedScene tree2Scene = AssetManager.GetScene("decoration.golden_grove.edge.tree_2");

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < roomPlacement.EdgeFieldLocations.Count; index++) {
                            bool shouldPlace = random.Randf() < 0.15f;

                            if(!shouldPlace) continue;

                            Vector2I location = (Vector2I)roomPlacement.EdgeFieldLocations[index];
                            int distance = roomPlacement.EdgeFieldDistances[index];

                            if(distance < 3) continue;

                            Vector2I[] occupyingCells = new Vector2I[] { location, location + new Vector2I(1, 0), location + new Vector2I(0, 1), location + new Vector2I(1, 1) };

                            if(occupyingCells.Any(cell => occupiedDecorationLocations.Contains(cell))) continue;

                            foreach(Vector2I cell in occupyingCells) {
                                occupiedDecorationLocations.Add(cell);
                            }

                            if(random.Randf() < 0.5f) {
                                placements.Add(new WorldGenerator.DecorationPlacement {
                                    Location = location * 16 + Vector2.One * 16f,
                                    Scene = tree1Scene
                                });
                            } else {
                                placements.Add(new WorldGenerator.DecorationPlacement {
                                    Location = location * 16 + Vector2.One * 16f,
                                    Scene = tree2Scene
                                });
                            }
                        }

                        return placements;
                    }
                },
                new EdgeDecoration {
                    Generate = (roomPlacement, occupiedDecorationLocations, random) => {
                        PackedScene treeSmallScene = AssetManager.GetScene("decoration.golden_grove.edge.tree_small");

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < roomPlacement.EdgeFieldLocations.Count; index++) {
                            bool shouldPlace = random.Randf() < 0.05f;

                            if(!shouldPlace) continue;

                            Vector2I location = (Vector2I)roomPlacement.EdgeFieldLocations[index];
                            int distance = roomPlacement.EdgeFieldDistances[index];

                            if(distance < 3) continue;

                            Vector2I[] occupyingCells = new Vector2I[] { location };

                            if(occupyingCells.Any(cell => occupiedDecorationLocations.Contains(cell))) continue;

                            foreach(Vector2I cell in occupyingCells) {
                                occupiedDecorationLocations.Add(cell);
                            }

                            placements.Add(new WorldGenerator.DecorationPlacement {

                                Location = location * 16 + Vector2.One * 8f,
                                Scene = treeSmallScene
                            });
                        }

                        return placements;
                    }
                },
                new EdgeDecoration {
                    Generate = (roomPlacement, occupiedDecorationLocations, random) => {
                        PackedScene bushScene = AssetManager.GetScene("decoration.golden_grove.edge.bush");

                        List<WorldGenerator.DecorationPlacement> placements = new List<WorldGenerator.DecorationPlacement>();

                        for(int index = 0; index < roomPlacement.EdgeFieldLocations.Count; index++) {
                            bool shouldPlace = random.Randf() < 0.05f;

                            if(!shouldPlace) continue;

                            Vector2I location = (Vector2I)roomPlacement.EdgeFieldLocations[index];
                            int distance = roomPlacement.EdgeFieldDistances[index];

                            if(distance < 3) continue;

                            Vector2I[] occupyingCells = new Vector2I[] { location };

                            if(occupyingCells.Any(cell => occupiedDecorationLocations.Contains(cell))) continue;

                            foreach(Vector2I cell in occupyingCells) {
                                occupiedDecorationLocations.Add(cell);
                            }

                            placements.Add(new WorldGenerator.DecorationPlacement {

                                Location = location * 16 + Vector2.One * 8f,
                                Scene = bushScene
                            });
                        }

                        return placements;
                    }
                }
            },
            HorizontalBarrier = AssetManager.GetScene("barrier.golden_grove.horizontal"),
            VerticalBarrier = AssetManager.GetScene("barrier.golden_grove.vertical"),
        });
    }

    private static SmartTile.Modifier CreateRandomVariantModifier(Vector2 target, Vector2[] variants) {
        return (Vector2I center, Vector2I location, RandomNumberGenerator random) => {
            if (location == center + target) {
                int index = random.RandiRange(0, variants.Length);

                if (index == 0) {
                    return location;
                } else {
                    return center + (Vector2I)variants[index - 1];
                }
            }

            return location;
        };
    }
}