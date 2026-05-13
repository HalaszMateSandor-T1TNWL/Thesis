using Godot;
using Godot.Collections;

public partial class PlayerController : Node
{
	[Signal] public delegate void TransitionToEventHandler(string key, Vector3 movementDirection);

	[Export] public NodePath initialState;

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
				state.Enter();
				state.Exit();
			}
		}
		GD.Print(_states.ToString());
		if(initialState != null)
		{
			_currentState = GetNode<State>(initialState);
			_currentState.Enter();
		}
		else
			GD.Print("[ERROR] Initial state not set.");
		
		_ui = GetNode<Control>($"../../CamRoot/Control");
		_elements = new Dictionary<string, CheckBox>();

		Vector2 position = new Vector2(1051,0);
		foreach(string state in _states.Keys)
		{
			CheckBox box = new CheckBox{
				Name = state,
				Text = state,
				Position = position	
			};
			_ui.AddChild(box);

			_elements[state] = box;
			position.Y += 25f;
		}

		_elements[_currentState.Name].ButtonPressed = true;
	}
	
	public override void _Process(double delta)
	{
		if(_currentState != null)
			_currentState.Update((float)delta);
		else
			_currentState = GetNode<State>(initialState);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(_currentState != null)
			_currentState.PhysicsUpdate((float)delta);
		else
			_currentState = GetNode<State>(initialState);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if(_currentState != null)
			_currentState.HandleInput(@event);
		else
			_currentState = GetNode<State>(initialState);
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
}
