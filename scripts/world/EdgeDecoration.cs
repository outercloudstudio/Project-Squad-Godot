using System;
using System.Collections.Generic;
using Godot;

public class EdgeDecoration {
    /*
    List<Vector2I> is a reference to a list of occupied cells  
    */
    public Func<WorldGenerator.RoomPlacement, List<Vector2I>, List<WorldGenerator.DecorationPlacement>> Generate;
}