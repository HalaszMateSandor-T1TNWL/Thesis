using Godot;
using System;
using System.Linq;

public partial class Grinding : State
{
	private Node3D _playerBody;
	private RayCast3D _rayCast;

	private Path3D _path3D;
	private PathFollow3D _pathFollow;

	public override void Enter()
	{
		GD.Print("Entering Grinding State");
		_rayCast = GetNode<RayCast3D>($"../../../RayGrinds/RayCast3D");
	}

	public override void PhysicsUpdate(float delta)
	{
		bool onRail = _rayCast.IsColliding() && _rayCast.GetCollider() is RailGrinding;
		if (onRail)
		{
			if(_playerBody == null)
			{
				return;
			}
			_pathFollow.ProgressRatio += 0.5f * delta;
			_playerBody.LookAt(_pathFollow.GlobalPosition, Vector3.Up, true);
			_playerBody.GlobalPosition =  _pathFollow.GlobalPosition;
		}
	}

	public void StartSlide()
	{
		_playerBody = GetNode<Node3D>($"../..");
		Vector3 firstPoint = _playerBody.GlobalPosition + _path3D.Curve.GetBakedPoints()[0];
		Vector3 localBodyPosition = _playerBody.ToLocal(_playerBody.GlobalPosition);
		Vector3 closestPoint = _playerBody.GlobalPosition + _path3D.Curve.GetClosestPoint(localBodyPosition);

		_pathFollow.Progress = firstPoint.DistanceTo(closestPoint);

		_playerBody.GlobalPosition = closestPoint;
	}

	public void StopSlide()
	{
		_playerBody = null;
	}

	public override void Exit()
	{
		GD.Print("Exiting Grinding State");
	}


}
