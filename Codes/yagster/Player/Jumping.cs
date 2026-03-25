using Godot;
using System;

public partial class Jumping : State
{
	[Export] public float jumpForce = 10;
	
	public Vector3 velocity;
	public Vector3 direction;
	
	public override void Enter()
	{
		GD.Print("Entering Jumping State");
		this.parent = GetNode<CharacterBody3D>($"../../..");
		velocity = parent.Velocity;
		velocity.Y = jumpForce;
		parent.Velocity = velocity;
	}
	
	public override void Exit()
	{
		GD.Print("Exiting Jumping State");
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
	
}
