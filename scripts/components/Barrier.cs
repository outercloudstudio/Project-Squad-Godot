using System.Collections.Generic;
using Godot;

public partial class Barrier : StaticBody2D {
	private List<Destructable> _destructables = new List<Destructable>();

	public override void _Ready() {
		Node decorations = GetNode("Decorations");

		foreach (Node node in decorations.GetChildren()) {
			if (!(node is Destructable destructable)) continue;

			_destructables.Add(destructable);
		}
	}

	public void Deactivate() {
		CollisionLayer = 0;

		foreach (Destructable destructable in _destructables) {
			if (!destructable.Invincible) continue;

			destructable.Invincible = false;
			destructable.SoundEffect = null;

			destructable.Damage(null);
		}
	}
}
