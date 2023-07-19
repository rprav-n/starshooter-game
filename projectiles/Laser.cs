using Godot;
using System;

public class Laser : Area2D
{
	[Export]
	private int speed = 1000;

	[Export]
	private int verticalDirection = 1;
	
	[Export]
	public int damage = 1;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		var newPosition = Vector2.Zero;
		
		newPosition.y = speed * verticalDirection * delta;
		
		this.GlobalPosition += newPosition;
	}
}
