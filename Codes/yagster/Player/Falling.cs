using Godot;
using System;

public partial class Falling : State
{
	private Vector3 _velocity;
	private Vector3 _lastMoveDirection;
	
	private Node3D _meshRoot;
	
	private float _accel;
	private float _speed;
	private float _rotationSpeed = 10.0f;
	
	public override void Enter()
	{
		parent = GetNode<CharacterBody3D>($"../../..");
		_meshRoot = GetNode<Node3D>($"../..");
		
		_velocity = parent.Velocity;
		_accel = new Player().accel;
		_speed = new Player().speed;
		_lastMoveDirection = Vector3.Back;
		
		GD.Print("Entering Falling State");
	}

	public override void PhysicsUpdate(float delta)
	{
		_velocity.Y -= gravity * delta;
		if(Mathf.Abs(movementDirection.X) > 0 || Mathf.Abs(movementDirection.Z) > 0)
		{
			_velocity = _velocity.MoveToward(movementDirection * _speed, _accel * delta);
		}
		parent.Velocity = _velocity;
		parent.MoveAndSlide();
		
		if(movementDirection.Length() > 0.2)
		{
			_lastMoveDirection = movementDirection;
		}
		
		var targetAngle = Vector3.Left.SignedAngleTo(_lastMoveDirection, Vector3.Up);
		_meshRoot.Rotation = new Vector3(0, Mathf.LerpAngle(_meshRoot.Rotation.Y, targetAngle, _rotationSpeed * delta), 0);
	}
	
	public override void Update(float delta)
	{
		if(parent.IsOnFloor())
		{
			EmitSignal(nameof(Transition), "Running", movementDirection);
			Exit();
		}
		else
			return;
	}
	
	public override void Exit()
	{
		GD.Print("Exiting Falling State");
	}
	
}
