using Godot;
using Godot.Collections;

public partial class PlayerController : Node
{
	[Export] public NodePath initialState;
	[Signal] public delegate void TransitionToEventHandler(string key, Vector3 movementDirection);
	
	private Dictionary<string, State> _states;
	private State _currentState;
	private Control _ui;
	private Dictionary<string, CheckBox> _elements;
	
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
		
		_ui = GetNode<Control>($"../../CamRoot/Control");
		_elements = new Dictionary<string, CheckBox>();
		foreach(Node node in _ui.GetChildren())
		{
			if(node is CheckBox box)
			{
				_elements[node.Name] = box;
			}
		}
		_elements[_currentState.Name].ButtonPressed = true;
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
		
		_elements[_currentState.Name].ButtonPressed = false;
		_currentState.Exit();
		_currentState = _states[key];
		_currentState.Enter();
		_currentState.movementDirection = movementDirection;
		_elements[_currentState.Name].ButtonPressed = true;
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
