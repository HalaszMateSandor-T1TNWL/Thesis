using Godot;

public partial class Grinding : State
{
	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;
	private Node3D _meshroot;

	[Export] public float grindSpeed = 2f;
	[Export] public float grindDrag = 5f;
  
	public override void Enter()
  	{
		_grindCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");

		parent = GetNode<Player>($"../../..");
		_meshroot = GetNodeOrNull<Node3D>($"../..");
		
		if(_usedRail == null) StartRail();
  	}

	public override void PhysicsUpdate(float delta)
	{
		if(_usedRail != null)
		{
			parent.anim.Play("Grinding");
			float progress = _usedRail.pathFollow.Progress + -_usedRail.pathFollow.Basis.Z.Normalized().Dot(parent.Velocity.Normalized()) * parent.Velocity.Length() * grindSpeed * (float)delta;
			_usedRail.pathFollow.Progress = progress;
			
			parent.GlobalRotation = _usedRail.pathFollow.GlobalRotation;
			parent.GlobalPosition = _usedRail.pathFollow.GlobalPosition;

			parent.Velocity = -_usedRail.pathFollow.Basis.Z.Normalized() * parent.Velocity.Length() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(parent.Velocity.Normalized());
			parent.Velocity += -_usedRail.pathFollow.Basis.Z.Normalized() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(-Vector3.Up.Normalized()) * grindDrag * (float)delta;
		}
	}

	public void StartRail()
	{
		bool onRail = _grindCast.IsColliding() && _grindCast.GetCollider(0) is RailGrinding;

		if(onRail)
		{
			_usedRail = (RailGrinding)_grindCast.GetCollider(0);

			_usedRail.CalculateTargetRailPoint(parent.GlobalPosition);
			
		}
	}

	public override void Exit()
	{
		if(_usedRail != null)
		{
			_usedRail.pathFollow.ProgressRatio = 0.0f;
			_usedRail = null;
			parent.GlobalRotation = Vector3.Zero;
		}
	}
}
