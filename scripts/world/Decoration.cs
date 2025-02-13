using System;
using System.Collections.Generic;
using Godot;

public class Decoration {
    /*
    List<Vector2I> is a reference to a list of empty cells  
    */
    public Func<WorldGenerator.RoomPlacement, List<Vector2I>, RandomNumberGenerator, List<WorldGenerator.DecorationPlacement>> Generate;
}