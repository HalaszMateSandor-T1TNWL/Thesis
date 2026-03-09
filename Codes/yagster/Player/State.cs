using Godot;
using System;

public partial class State : Node
{
	[Signal] public delegate void TransitionEventHandler(string key, Vector3 movementDirection);
	public PlayerController fsm;
	
	public int gravity = (int)ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	public CharacterBody3D parent;
	
	public Vector3 movementDirection;
	public Vector2 cameraDirection;
	
	public virtual void Enter() {}
	public virtual void Exit() {}
	
	public virtual void Update(float delta) {}
	public virtual void PhysicsUpdate(float delta) {}
	
	public virtual void HandleInput(InputEvent @event) {}
}
