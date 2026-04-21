using Godot;

public partial class Grinding : State
{
	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;
	private new Player parent;
	private MeshInstance3D _playerBody;
  	private bool _frontFacing = false;
	private float grindSpeed = 10f;

	float railSwapWeight = 1.0f;
  
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
			railSwapWeight = Mathf.Clamp(railSwapWeight + (float)delta * 2f, -1, 1);

			float progress = _usedRail.pathFollow.Progress + -_usedRail.pathFollow.Basis.Z.Normalized().Dot(parent.Velocity.Normalized()) * parent.Velocity.Length() * (float)delta;
			_usedRail.pathFollow.Progress = progress;

			if(railSwapWeight >= 1)
			{
				parent.GlobalPosition = _usedRail.pathFollow.GlobalPosition;
			}
			else
			{
				parent.GlobalPosition = parent.GlobalPosition.Lerp(_usedRail.pathFollow.GlobalPosition, Mathf.Clamp(railSwapWeight, 0, 1));	
			}
			parent.GlobalRotation = _usedRail.pathFollow.GlobalRotation;

			parent.Velocity = -_usedRail.pathFollow.Basis.Z.Normalized() * parent.Velocity.Length() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(parent.Velocity.Normalized());
			parent.Velocity += -_usedRail.pathFollow.Basis.Z.Normalized() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(-Vector3.Up.Normalized()) * 5f * (float)delta;

			parent.MoveAndSlide();	
		}
	}


	public void StartRail()
	{
		bool onRail = _grindCast.IsColliding() && _grindCast.GetCollider(0) is RailGrinding;

		if(onRail)
		{
			_usedRail = (RailGrinding)_grindCast.GetCollider(0);

			_frontFacing = _usedRail.CalculateTargetRailPoint(parent.GlobalPosition, parent.Velocity);

			GD.Print("Front Facing: " + _frontFacing.ToString());
		}
	}

	public override void Exit()
	{
		if(_usedRail != null)
		{
			_usedRail.pathFollow.ProgressRatio = 0.0f;
			_usedRail = null;
			parent.GlobalRotation = new Vector3(0,0,0);
		}
	}


}
