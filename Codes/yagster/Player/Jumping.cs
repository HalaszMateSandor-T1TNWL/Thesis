using Godot;
using System;

public partial class Jumping : State
{
	[Export] public float jumpForce = 10;
	
	public Vector3 velocity;
	public Vector3 direction;
  private ShapeCast3D _shapeCast;
	
	public override void Enter()
	{
		parent = GetNode<CharacterBody3D>($"../../..");
		_shapeCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		_shapeCast.Enabled = false;
		velocity = parent.Velocity;
		velocity.Y = jumpForce;
		parent.Velocity = velocity;
	}
	
	public override void PhysicsUpdate(float delta)
	{
		velocity.Y -= gravity * delta;

		parent.Velocity = velocity;
		
		parent.Velocity = parent.Velocity.MoveToward(movementDirection, new Player().accel * delta);

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
