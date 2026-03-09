using Godot;
using System;

public partial class Running : State
{
	private float _speed;
	private float _accel;
	private float _rotationSpeed = 10.0f;
	
	private Vector3 _lastMoveDirection = Vector3.Back;
	private Vector3 _velocity;
	
	private Node3D _meshRoot;
	
	public override void Enter()
	{
		parent = GetNode<CharacterBody3D>($"../../..");
		_meshRoot = GetNode<Node3D>($"../..");
		_speed = new Player().speed;
		_accel = new Player().accel;
	}
	
	public override void PhysicsUpdate(float delta)
	{
		_velocity = parent.Velocity;
		
		_velocity = _velocity.MoveToward(movementDirection * _speed, _accel * delta);
		
		parent.Velocity = _velocity;
		
		parent.MoveAndSlide();
		
		if(movementDirection.Length() > 0.2)
		{
			_lastMoveDirection = movementDirection;
		}
		
		var targetAngle = Vector3.Left.SignedAngleTo(_lastMoveDirection, Vector3.Up);
		
		_meshRoot.Rotation = new Vector3(0, Mathf.LerpAngle(_meshRoot.Rotation.Y, targetAngle, _rotationSpeed * delta), 0);
	}
	
	
	
}
