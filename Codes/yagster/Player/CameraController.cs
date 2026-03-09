using Godot;
using System;

public partial class CameraController : Node3D
{
	[Signal] public delegate void SetCameraDirectionEventHandler(Vector2 cameraDirection);
	
	[Export] public float mouseSensitivity = 20.0f;
	
	private Vector2 _cameraInputDirection = Vector2.Zero;
	
	private const double _tiltUpperLimit = Math.PI / 3.0f;
	private const double _tiltLowerLimit = -Math.PI / 6.0f;
	
	private Camera3D _camera;
	private Node3D _yawCam;
	private Node3D _pitchCam;
	
	public override void _Ready()
	{
		_camera = GetNode<Camera3D>($"CamYaw/CamPitch/SpringArm3D/Camera3D");
		_yawCam = GetNode<Node3D>($"CamYaw");
		_pitchCam = GetNode<Node3D>($"CamYaw/CamPitch");
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
			InputEventMouseMotion m = (InputEventMouseMotion) @event;
			_cameraInputDirection = m.ScreenRelative * mouseSensitivity;
			EmitSignal(nameof(SetCameraDirection), _cameraInputDirection);
		}
	}
	
	public override void _Process(double delta)
	{
		/*Vector3 pitchRotation = _pitchCam.Rotation;
		Vector3 yawRotation = _yawCam.Rotation;
		
		pitchRotation.Z = (float)(_cameraInputDirection.Y * delta);
		yawRotation.Y = (float)(_cameraInputDirection.X * delta);
		
		_pitchCam.Rotation = new Vector3(0, 0, (float)Math.Clamp(pitchRotation.Z, _tiltLowerLimit, _tiltUpperLimit));
		_yawCam.Rotation = yawRotation;
		
		_cameraInputDirection = Vector2.Zero;*/
	}
	
	public bool IsCameraMotion(InputEvent @event)
	{
		return @event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseModeEnum.Captured;
	}
	
}
