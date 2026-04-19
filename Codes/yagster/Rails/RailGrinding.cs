using Godot;

[GlobalClass]
public partial class RailGrinding : StaticBody3D
{
	/*
	public override void _PhysicsProcess(double delta)
	{
		if(_playerBody == null)
		{
			return;
		}

		if(_pathFollow.ProgressRatio == 1)
			StopSlide();
		_pathFollow.ProgressRatio += (float)(0.5 * delta);
		_playerBody.GlobalPosition = _pathFollow.GlobalPosition;
	}

	public void StartSlide()
	{
		_playerBody = GetNode<Node3D>($"../Player");

		Vector3 localBodyPosition = _playerBody.ToLocal(_playerBody.GlobalPosition);
		Vector3 closestPoint = _playerBody.GlobalPosition + _path.Curve.GetClosestPoint(localBodyPosition);
		Vector3 firstPoint = _playerBody.GlobalPosition + _path.Curve.GetBakedPoints()[0];

		_pathFollow.Progress = firstPoint.DistanceTo(closestPoint);

		_playerBody.GlobalPosition = closestPoint;
	}
	*/


	public float length;
	public Path3D path;
	public PathFollow3D pathFollow;
	public CsgPolygon3D railBody;

	public override void _Ready()
	{
		AddToGroup("Rails");
		path = GetNode<Path3D>($"Path3D");
		length = path.Curve.GetBakedLength();
		pathFollow = GetNode<PathFollow3D>($"Path3D/PathFollow3D");
		railBody = GetNode<CsgPolygon3D>($"CSGPolygon3D");
		railBody.Polygon = Vector2[(
			Vector2()
		)];
	}

	public bool CalculateTargetRailPoint(Vector3 playerPosition, Vector3 playerVelocity)
	{
		float progress = path.Curve.GetClosestOffset(playerPosition - path.GlobalPosition);
		Transform3D sample = path.GlobalTransform * path.Curve.SampleBakedWithRotation(progress);

		pathFollow.Progress = progress;
		pathFollow.GlobalTransform = sample;
		pathFollow.ResetPhysicsInterpolation();

		return (-sample.Basis.Z.Normalized()).Dot(playerVelocity.Normalized()) >= 0;
	}
}
