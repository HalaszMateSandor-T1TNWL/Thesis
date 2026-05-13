using Godot;
using System;

public partial class ShapeCast3d : ShapeCast3D
{
	private Timer _timer;
	private State _jumping;

	public override void _Ready()
	{
		_timer = GetNodeOrNull<Timer>($"Timer");
		_timer.Timeout += OnTimerUp;
	}

	private void OnTimerUp()
	{
		Enabled = true;
	}

	private void OnDisable()
	{
		_timer.Start();
		Enabled = false;
	}

}
