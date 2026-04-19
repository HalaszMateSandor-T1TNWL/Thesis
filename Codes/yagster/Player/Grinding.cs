using Godot;

public partial class Grinding : State
{
	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;
	private new Player parent;
	private MeshInstance3D _playerBody;
  	private bool _frontFacing = false;
	private float grindSpeed = 10f;
  
	public override void Enter()
  	{
		_grindCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		parent = (Player)GetNode<Node3D>($"../../..");
		_playerBody = parent.playerBody; 
		if(_usedRail == null) StartRail();
  	}

  public override void PhysicsUpdate(float delta)
  {
	if(_usedRail != null)
	{
		switch (_frontFacing)
		{
			case true:
				if(_usedRail.pathFollow.ProgressRatio == 1)
				{
					_frontFacing = false;
				}
				else if(_usedRail.pathFollow.ProgressRatio < 1)
				{
					_usedRail.pathFollow.Progress += 6.5f * delta;
				}
			break;

			case false:
				if(_usedRail.pathFollow.ProgressRatio == 0)
				{
					_frontFacing = true;
				}
				else if(_usedRail.pathFollow.ProgressRatio > 0)
				{
					_usedRail.pathFollow.Progress -= 6.5f * delta;
				}
			break;
		}
	}
  }


	public void StartRail()
	{
		bool onRail = _grindCast.IsColliding() && _grindCast.GetCollider(0) is RailGrinding;

		if(onRail)
		{
			_usedRail = (RailGrinding)_grindCast.GetCollider(0);
			
			SetRailPosition();
			
			_usedRail.remoteTransform.RemotePath = parent.GetPath();
		
		}
	}

	public void SetRailPosition()
	{
		Vector3 railPoint = _usedRail.CalculateTargetRailPoint(_playerBody.Position);
		
		if(_usedRail.CalculateDirection(_usedRail.pathFollow.GlobalTransform.Basis.Z, _playerBody.GlobalTransform.Basis.Z))
		{
			GD.Print("Facing the Front");
			_frontFacing = true;
		}
		else
		{
			GD.Print("Facing the back");
			_frontFacing = false;
		}
		
		parent.GlobalPosition = new Vector3(
			(float)Mathf.Lerp(parent.GlobalPosition.X, railPoint.X, 0.5),
			(float)Mathf.Lerp(parent.GlobalPosition.Y, railPoint.Y, 0.5),
			(float)Mathf.Lerp(parent.GlobalPosition.Z, railPoint.Z, 0.5)
		);
	
		Vector3 firstPoint = _playerBody.GlobalPosition + _usedRail.path.Curve.GetBakedPoints()[0];
		_usedRail.pathFollow.Progress = firstPoint.DistanceTo(railPoint);
	}


	public override void Exit()
	{
		if(_usedRail != null)
		{
			_usedRail.remoteTransform.RemotePath = ".";
			_usedRail.remoteTransform.ForceUpdateCache();
			_usedRail.pathFollow.ProgressRatio = 0.0f;
			_usedRail = null;
			parent.GlobalRotation = new Vector3(0,0,0);
		}
	}
}
