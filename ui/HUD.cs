using Godot;
using System;

public class HUD : Control
{
	private Label scoreLabel;	

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
	}
	
	public void updateScore(int score) 
	{
		scoreLabel.Text = "SCORE: " + score.ToString();
	}
}
