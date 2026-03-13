using Godot;
using System;

public partial class Idle : State
{
	private float _speed;
	private float _accel;
	
	private Vector3 _velocity;
	
	public override void Enter()
	{
		parent = GetNode<CharacterBody3D>($"../../..");
		_speed = new Player().speed;
		_accel = new Player().accel;
		GD.Print("Entering Idle State");
	}
	
	public override void Update(float delta)
	{
		if(Mathf.Abs(movementDirection.X) > 0 || Mathf.Abs(movementDirection.Z) > 0)
		{
			GD.Print("Enter Running!");
			EmitSignal(nameof(Transition), "Running", movementDirection);
		}
	}
	
	public override void PhysicsUpdate(float delta)
	{
		_velocity = parent.Velocity;
		_velocity = _velocity.MoveToward(Vector3.Zero * _speed, _accel * delta);
		parent.Velocity = _velocity;
		
		parent.MoveAndSlide();
	}
	
	public override void Exit()
	{
		GD.Print("Exiting Idle State");
	}
}
