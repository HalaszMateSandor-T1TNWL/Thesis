using Godot;

[GlobalClass]
public partial class RailGrinding : StaticBody3D
{
	public Path3D path;
	public PathFollow3D pathFollow;

	public override void _Ready()
	{
		AddToGroup("Rails");
		path = GetNode<Path3D>($"Path3D");
		pathFollow = GetNode<PathFollow3D>($"Path3D/PathFollow3D");
	}

	public void CalculateTargetRailPoint(Vector3 playerPosition)
	{
		float progress = path.Curve.GetClosestOffset(playerPosition - path.GlobalPosition);
		
		Transform3D sample = path.GlobalTransform * path.Curve.SampleBakedWithRotation(progress);

		pathFollow.Progress = progress;
		pathFollow.GlobalTransform = sample;
		
		pathFollow.ResetPhysicsInterpolation();
	}
}
