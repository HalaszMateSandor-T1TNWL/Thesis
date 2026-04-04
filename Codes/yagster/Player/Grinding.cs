using Godot;

public partial class Grinding : State
{
	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;
	private new Node3D parent;
  private bool _frontFacing = false;

	private float grindSpeed = 10f;

  public override void Enter()
  {
    _grindCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		parent = GetNode<Node3D>($"../../..");
  }

  public override void Update(float delta)
  {
    if(_usedRail == null) StartRail();
  }

  public override void PhysicsUpdate(float delta)
  {
    if(_usedRail != null)
    {
      switch (_frontFacing)
      {
        case true:
          GD.Print("Facing the front!");
          if(_usedRail.pathFollow.ProgressRatio == 1)
          {
            GD.Print("Progress ratio:\t" + _usedRail.pathFollow.ProgressRatio);
  	 			  Exit();
  	 		  }
          else if(_usedRail.pathFollow.ProgressRatio < 1)
  	 		  {
            GD.Print("Progress ratio:\t" + _usedRail.pathFollow.ProgressRatio);
  	 			  _usedRail.pathFollow.Progress += 6.5f * delta;
  	 		  }	
        break;
  
        case false:
          GD.Print("Going backwards!");
          if(_usedRail.pathFollow.ProgressRatio == 0)
          {
            GD.Print("Progress ratio:\t" + _usedRail.pathFollow.ProgressRatio);
            Exit();
   		    }
    	    else if(_usedRail.pathFollow.ProgressRatio > 0)
   		    {
            GD.Print("Progress ratio:\t" + _usedRail.pathFollow.ProgressRatio);
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
  	  _usedRail.remoteTransform.RemotePath = parent.GetPath();
			SetRailPosition();
		}
	}

  public void SetRailPosition()
  {
		Vector3 railPoint = _usedRail.CalculateTargetRailPoint(parent.GlobalPosition);
    if(_usedRail.CalculateDirection(-_usedRail.pathFollow.GlobalBasis.Z, -parent.GlobalBasis.Z))
		{
      _frontFacing = true;
		}
	  else
    {
      _frontFacing = false;
    }

	  GD.Print(railPoint);
    
    parent.GlobalPosition = new Vector3(
        (float)Mathf.Lerp(parent.GlobalPosition.X, railPoint.X, 0.05),
		    (float)Mathf.Lerp(parent.GlobalPosition.Y, railPoint.Y, 0.05),
		    (float)Mathf.Lerp(parent.GlobalPosition.Z, railPoint.Z, 0.05)
    );
  
		Vector3 firstPoint = parent.GlobalPosition + _usedRail.path.Curve.GetBakedPoints()[0];
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
      EmitSignal(nameof(Transition), "Jumping", movementDirection);
  	}
	}
}
