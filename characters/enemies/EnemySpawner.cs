using System.Collections.Generic;
using Godot;
using System;

public class EnemySpawner : Node2D
{
	[Export]
	private List<PackedScene> enemies;
	
	[Signal]
	private delegate void spawnEnemy(PackedScene Enemy, Vector2 spawnPosition);

	private Node2D spawnPositions;
	private List<Position2D> spawnPositionArr = new List<Position2D>();
	Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnPositions = GetNode<Node2D>("SpawnPositions");
		var arr = spawnPositions.GetChildren();
		foreach (var item in arr)
		{
			spawnPositionArr.Add(item as Position2D);
		}
	}
	
	public void _on_SpawnTimer_timeout() 
	{
		spawnRandomEnemy();
	}
	
	public void spawnRandomEnemy() 
	{
		
		GD.Print("SPAWN");
		var randomEnemyIndex = random.Next(0, enemies.Count);
		var randomEnemy = enemies[randomEnemyIndex];
		
		var randomSpawnPositionIndex = random.Next(0, spawnPositionArr.Count);
		var randomPosition = spawnPositionArr[randomSpawnPositionIndex];
		
		this.EmitSignal("spawnEnemy", randomEnemy, randomPosition.GlobalPosition);	
	}
}
