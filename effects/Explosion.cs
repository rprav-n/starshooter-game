using Godot;
using System;

public class Explosion : CPUParticles2D
{

private AudioStreamPlayer sfx;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sfx = GetNode<AudioStreamPlayer>("Sfx");
	}
	
	public void start() 
	{
		sfx.Play();
		this.Emitting = true;
	}
}
