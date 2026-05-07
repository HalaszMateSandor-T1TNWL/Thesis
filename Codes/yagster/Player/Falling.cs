using Godot;

public partial class Falling : State
{
	private Vector3 _velocity;
	private Vector3 _lastMoveDirection;
	
	private Node3D _meshRoot;

	private float _accel;
	private float _speed;
	private const float _mass = 1.5f;
	private float _rotationSpeed = 10.0f;
	
	public override void Enter()
	{
		parent = GetNode<Player>($"../../..");
		_meshRoot = GetNode<Node3D>($"../..");
		
		_velocity = parent.Velocity;
		_accel = parent.accel;
		_speed = parent.speed;
		_lastMoveDirection = Vector3.Back;
	}

	public override void PhysicsUpdate(float delta)
	{
		parent.anim.Play("Falling");
		_velocity.Y -= gravity * _mass * delta;
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
		
		var targetAngle = Vector3.Forward.SignedAngleTo(_lastMoveDirection, Vector3.Up);
		_meshRoot.Rotation = new Vector3(0, Mathf.LerpAngle(_meshRoot.Rotation.Y, targetAngle, _rotationSpeed * delta), 0);
	}
	
	public override void Update(float delta)
	{
		if(parent.IsOnFloor())
		{
			EmitSignal(nameof(Transition), "Running", movementDirection);
		}
	}
}
