using Godot;

[GlobalClass]
public partial class RailGrinding : StaticBody3D
{
	/*private Node3D _playerBody;
	private Path3D _path;
	private PathFollow3D _pathFollow;
	private bool _normalDirection;
	
	private bool _hasSpawnedPoints = false;
	private float _pointCount = 0.0f;

	public override void _Ready()
	{
		_path = GetNode<Path3D>($"Path3D");
		_pathFollow = GetNode<PathFollow3D>($"Path3D/PathFollow3D");
	}
	
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

	public Vector3 Lerp(Vector3 firstVec, Vector3 secondVec, float by)
	{
		float x = Mathf.Lerp(firstVec.X, secondVec.X, by);
		float y = Mathf.Lerp(firstVec.Y, secondVec.Y, by);
		float z = Mathf.Lerp(firstVec.Z, secondVec.Z, by);

		return new Vector3(x, y, z);
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

	public void CalculateDirection(Vector3 railForward, Vector3 playerForward)
	{
		float angle = Mathf.RadToDeg(playerForward.AngleTo(railForward));
		if(angle > 90f)
		{
			_normalDirection = false;
		}
		else
		{
			_normalDirection = true;
		}
	}

	public void StopSlide()
	{
		_playerBody = null;
	}*/


	public float length;
	public Path3D path;
	public PathFollow3D pathFollow;
	public RemoteTransform3D remoteTransform;
	
  public override void _Ready()
  {
		AddToGroup("Rails");
		path = GetNode<Path3D>($"Path3D");
		length = path.Curve.GetBakedLength();
		pathFollow = GetNode<PathFollow3D>($"Path3D/PathFollow3D");
		remoteTransform = GetNode<RemoteTransform3D>($"Path3D/PathFollow3D/RemoteTransform3D");
  }

	public Vector3 CalculateTargetRailPoint(Vector3 playerPosition)
	{
		Vector3 nearestPoint =	path.Curve.GetClosestPoint(path.ToLocal(playerPosition));
  	Vector3 railPoint = playerPosition + path.ToGlobal(nearestPoint);

		return railPoint;
	}
	
	public bool CalculateDirection(Vector3 railForward, Vector3 playerForward)
	{
		float angle = Mathf.RadToDeg(playerForward.AngleTo(railForward));
		
		return angle > 90 ? true : false;
	}
}
