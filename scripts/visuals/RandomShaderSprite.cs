using Godot;
using System;

public partial class RandomShaderSprite : Sprite2D {
	public override void _Ready() {
		RandomNumberGenerator random = new RandomNumberGenerator();
		int id = random.RandiRange(0, 100);

		SetInstanceShaderParameter("id", id);
	}
}
