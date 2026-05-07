using Godot;

public partial class Idle : State
{
	private float _speed;
	private float _accel;

	public new Player parent;
	
	private Vector3 _velocity;
	
	public override void Enter()
	{
		parent = (Player)GetNodeOrNull<CharacterBody3D>($"../../..");
		_speed = parent.speed;
		_accel = parent.accel;
	}
	
	public override void PhysicsUpdate(float delta)
	{
		parent.anim.Play("Idle");
		_velocity = parent.Velocity;
		_velocity = _velocity.MoveToward(Vector3.Zero * _speed, _accel * delta);
		parent.Velocity = _velocity;
		
		parent.MoveAndSlide();
	}
}
