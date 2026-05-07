using System;
using Godot;

public partial class Grinding : State
{
	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;

	private MeshInstance3D _playerBody;
  	private bool _frontFacing = false;
	[Export] public float grindSpeed = 2f;
  
	public override void Enter()
  	{
		_grindCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");

		parent = GetNode<Player>($"../../..");
		
		if(_usedRail == null) StartRail();
  	}

	public override void PhysicsUpdate(float delta)
	{
		if(_usedRail != null)
		{
			parent.anim.Play("Grinding");
			float progress = _usedRail.pathFollow.Progress + -_usedRail.pathFollow.Basis.Z.Normalized().Dot(parent.Velocity.Normalized()) * parent.Velocity.Length() * grindSpeed * (float)delta;
			_usedRail.pathFollow.Progress = progress;

			parent.GlobalPosition = _usedRail.pathFollow.GlobalPosition;
			parent.GlobalRotation = _usedRail.pathFollow.GlobalRotation * -_usedRail.pathFollow.Basis.Z.Normalized().Dot(-parent.Basis.Z.Normalized());


			parent.Velocity = -_usedRail.pathFollow.Basis.Z.Normalized() * parent.Velocity.Length() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(parent.Velocity.Normalized());
			parent.Velocity += -_usedRail.pathFollow.Basis.Z.Normalized() * (-_usedRail.pathFollow.Basis.Z.Normalized()).Dot(-Vector3.Up.Normalized()) * 5f * (float)delta;
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
			parent.GlobalRotation = new Vector3(0,0,0);
		}
	}


}
