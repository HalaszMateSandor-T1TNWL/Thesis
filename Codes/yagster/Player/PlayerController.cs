using Godot;
using Godot.Collections;
using System;

public partial class PlayerController : Node
{
	[Export] public NodePath initialState;
	[Signal] public delegate void TransitionToEventHandler(string key, Vector3 movementDirection);
	
	private Dictionary<string, State> _states;
	private State _currentState;
	
	public override void _Ready()
	{
		_states = new Dictionary<string, State>();
		foreach(Node node in GetChildren())
		{
			if(node is State state)
			{
				_states[node.Name] = state;
				state.fsm = this;
				state.Transition += OnTransitionTo;
				state._Ready();
				state.Exit();
			}
		}
		if(initialState != null)
		{
			_currentState = GetNode<State>(initialState);
			_currentState.Enter();
		}
		else
			GD.Print("[ERROR] Initial state not set.");
	}
	
	public override void _Process(double delta)
	{
		_currentState.Update((float)delta);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate((float)delta);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}
	
	public void OnTransitionTo(string key, Vector3 movementDirection)
	{
		if(!_states.ContainsKey(key) || _currentState == _states[key])
		{
			return;
		}
		
		_currentState.Exit();
		_currentState = _states[key];
		_currentState.Enter();
		_currentState.movementDirection = movementDirection;
	}
	
	public void OnSetMovementState(string key, Vector3 movementDirection)
	{
		OnTransitionTo(key, movementDirection);
	}
	
	public void OnSetMovementDirection(Vector3 movementDirection)
	{
		_currentState.movementDirection = movementDirection;
	}
	
	public void OnSetCameraDirection(Vector2 cameraDirection)
	{
		_currentState.cameraDirection = cameraDirection;
	}
	
}
