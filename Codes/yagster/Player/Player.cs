using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Signal] public delegate void SetMovementStateEventHandler(string key, Vector3 movementDirection);
	[Signal] public delegate void SetMovementDirectionEventHandler(Vector3 movementDirection);
	
	[Export] public float speed = 50.0f;
	[Export] public float accel = 20.0f;
	[Export] public double DisableGroundRayTime { get; set; }
	public ShapeCast3D _shapecast;
	public RayCast3D RayCast;

	[Export] public float mouseSensitivity = 0.25f;
	
	private const double _tiltUpperLimit = Math.PI / 3.0;
	private const double _tiltLowerLimit = -Math.PI / 6.0;
	
	public bool Grounded = false;
	private Vector3 _movementDirection;
  	public MeshInstance3D playerBody;

	private Node3D _cameraYaw;
	private Node3D _cameraPitch;
	private Vector2 _cameraInputDirection = Vector2.Zero;
	private Camera3D _camera;

	private CheckBox _checkBox;
	private CheckBox _isGrindingDebug;
	private Label _speed;
	
	private Vector3 _velocity;
	
	public enum PlayerState{
		Idle,
		Running,
		Jumping,
		Falling,
		Grinding
	}
	
	public PlayerState CurrentState = PlayerState.Idle;

	public override void _Ready()
	{
		_cameraYaw = GetNode<Node3D>($"CamRoot/CamYaw");
		_cameraPitch = GetNode<Node3D>($"CamRoot/CamYaw/CamPitch");
		_camera = GetNode<Camera3D>($"CamRoot/CamYaw/CamPitch/SpringArm3D/Camera3D");
		
		_checkBox = GetNode<CheckBox>($"CamRoot/Control/CheckBox");
		_isGrindingDebug = GetNode<CheckBox>($"CamRoot/Control/IsGrinding");

		playerBody = GetNode<MeshInstance3D>($"Yagi");
		RayCast = GetNodeOrNull<RayCast3D>($"RayCast3D");

		_speed = GetNode<Label>($"CamRoot/Control/Speed/Label");

		_shapecast = GetNode<ShapeCast3D>($"ShapeCast3D");
		
	}

	public bool IsMoving()
	{
		return Mathf.Abs(_movementDirection.X) > 0 || Mathf.Abs(_movementDirection.Z) > 0;
	}

	public bool IsGrinding()
	{
		return _shapecast.IsColliding() && _shapecast.GetCollider(0) is RailGrinding;
	}
	
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("LeftClick"))
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		if(@event.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if(IsCameraMotion(@event))
		{
			InputEventMouseMotion m = (InputEventMouseMotion)@event;
			_cameraInputDirection = m.ScreenRelative * mouseSensitivity;
		}
	}
	
	public bool IsCameraMotion(InputEvent @event)
	{
		return @event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseModeEnum.Captured;
	}
	
	public override void _Process(double delta)
	{
		if (IsMoving())
		{
			EmitSignal(nameof(SetMovementDirection), _movementDirection);
		}
		
		switch(CurrentState)
		{
			case PlayerState.Idle:
				EmitSignal(nameof(SetMovementState), nameof(PlayerState.Idle), _movementDirection);
				break;
			case PlayerState.Running:
				EmitSignal(nameof(SetMovementState), nameof(PlayerState.Running), _movementDirection);
				break;
			case PlayerState.Jumping:
				EmitSignal(nameof(SetMovementState), nameof(PlayerState.Jumping), _movementDirection);
				break;
			case PlayerState.Falling:
				EmitSignal(nameof(SetMovementState), nameof(PlayerState.Falling), _movementDirection);
				break;
			case PlayerState.Grinding:
				EmitSignal(nameof(SetMovementState), nameof(PlayerState.Grinding), _movementDirection);
				break;
		}

		_checkBox.ButtonPressed = Grounded;
		_isGrindingDebug.ButtonPressed = IsGrinding();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector3 pitchRotation = _cameraPitch.Rotation;
		Vector3 yawRotation = _cameraYaw.Rotation;
		pitchRotation.Z += (float)(_cameraInputDirection.Y * delta);
		yawRotation.Y -= (float)(_cameraInputDirection.X * delta);
		
		_cameraPitch.Rotation = new Vector3(0, 0, (float)Math.Clamp(pitchRotation.Z, _tiltLowerLimit, _tiltUpperLimit));
		_cameraYaw.Rotation = yawRotation;
		
		_cameraInputDirection = Vector2.Zero;
		
		Vector3 forward = _camera.GlobalBasis.Z;
		Vector3 right = _camera.GlobalBasis.X;
		
		var rawInput = Input.GetVector("Left", "Right", "Forward", "Backward");
		
		_movementDirection = forward * rawInput.Y + right * rawInput.X;
		_movementDirection.Y = 0.0f;
		_movementDirection = _movementDirection.Normalized();
		
		_speed.Text = _movementDirection.ToString();
		
		if(Grounded)
		{
			if(Input.IsActionJustPressed("Movement"))
			{
				CurrentState = PlayerState.Running;
			}
			else if(_movementDirection.X == 0 && _movementDirection.Z == 0)
			{
				CurrentState = PlayerState.Idle;
			}
		}
		else if(Grounded == false)
		{
			CurrentState = PlayerState.Falling;
		}
		
		if(Grounded == false && !IsGrinding())
		{
			CurrentState = PlayerState.Falling;
		}

		if(IsGrinding())
		{
			CurrentState = PlayerState.Grinding;
		}

		if(Input.IsActionJustPressed("Jump"))
		{
			CurrentState = PlayerState.Jumping;
		}
	}
}
