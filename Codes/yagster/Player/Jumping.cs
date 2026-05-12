using Godot;

public partial class Jumping : State
{
	[Signal] public delegate void DisableEventHandler();

	[Export] public float jumpForce = 8;

	public Vector3 velocity;
  	private ShapeCast3D _shapeCast;
	
	public override void Enter()
	{
		parent = GetNode<Player>($"../../..");

		EmitSignal(nameof(Disable));

		velocity = parent.Velocity;
		velocity.Y = jumpForce;
		parent.Velocity = velocity;
	}
	
	public override void PhysicsUpdate(float delta)
	{
		parent.anim.Play("Jumping");
		parent.MoveAndSlide();

		if(parent.Velocity.Y > 0.0f)
		{
			EmitSignal(nameof(Transition), "Falling", movementDirection);
		}
	}
}
