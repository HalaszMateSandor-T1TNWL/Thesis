using Godot;
using System;

public partial class PathFollower : PathFollow3D
{
	private Path3D _path3D;

	private float _moveSpeed;
	private float _localStartingPoint = 0f;
	private float? _originPoint = null;

	private bool _grinding = false;
	private bool _forward = true;
	private bool _detach = false;

	public override void _Ready()
	{
		_path3D = GetNode<Path3D>($"..");
		_originPoint = this.Progress;
		_moveSpeed = new Player().speed;
	}

	public override void _Process(double delta)
	{
		if(_grinding)
		{
			if(_forward)
			{
				this.Progress += _moveSpeed * (float)delta;
			}
			else if(!_forward)
			{
				this.Progress -= _moveSpeed * (float)delta;
			}

			if(this.ProgressRatio >= 0.99f)
			{
				_detach = true;
				_grinding = false;
			}

			if(this.ProgressRatio <= 0.002)
			{
				_detach = true;
				_grinding = false;
			}
		}
		if(HasNode("Player"))
		{
			_grinding = true;
		}
		else
		{
			_grinding = false;
			this.Progress = (float)_originPoint;
		}
	}

}
