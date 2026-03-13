using Godot;
using System;

[GlobalClass]
public partial class RailGrinding : Path3D
{
	[Export] public PackedScene _railFollower = GD.Load<PackedScene>($"res://Scenes/PathFollower.tscn");
	[Export] public int _totalPoint = 20;
	
	private bool _hasSpawnedPoints = false;
	private float _pointCount = 0.0f;

	public override void _Ready()
	{
		SummonPoints();
	}

	private void SummonPoints()
	{
		float pathLength = Curve.GetBakedLength();
		float pointSpacing = pathLength / (_totalPoint - 1);
		float currentDistance = 0f;
		float startingPoint = 0.0001f;

		for(int i = 0; i < _totalPoint; i++)
		{
			PathFollow3D railPoint = (PathFollow3D)_railFollower.Instantiate();
			railPoint.Progress = startingPoint;
			this.AddChild(railPoint);
			startingPoint += 5;
			currentDistance += pointSpacing;
			_pointCount += 1.0f;
			if(i == _totalPoint)
			{
				_hasSpawnedPoints = true;
			}
		}

	}

}
