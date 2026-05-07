using Godot;

public partial class Jumping : State
{
	[Export] public float jumpForce = 8;

	public Vector3 velocity;
  	private ShapeCast3D _shapeCast;
	
	public override void Enter()
	{
		parent = GetNode<Player>($"../../..");

		_shapeCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		_shapeCast.Enabled = false;

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

  	public override void Exit()
	{
		if(_shapeCast != null)
			_shapeCast.Enabled = true;
	}
}
