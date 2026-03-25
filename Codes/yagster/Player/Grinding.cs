using Godot;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

public partial class Grinding : State
{
	/*private Node3D _usedRail = null;
	private ShapeCast3D _rayCast;
	private Path3D _path3D;
	private float _grindSpeed = 10f;

	public override void Enter()
	{
		GD.Print("Entering Grinding State");
		_rayCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		this.parent = GetNode<CharacterBody3D>($"../../..");
	}
	
	public override void Update(float delta)
	{
		
	}

	public override void HandleInput(InputEvent @event)
	{
		if(Input.IsActionJustPressed("Shift") && _usedRail == null)
		{
			StartRail();
		}
		if(Input.IsActionJustPressed("Jump") && _usedRail != null)
		{
			StopRail();
		}
	}

	public override void PhysicsUpdate(float delta)
	{
		
	}

	public void StartRail()
	{
		bool onRail = _rayCast.IsColliding() && _rayCast.GetCollider(0) is RailGrinding;

		if (onRail)
		{
			_usedRail = (RailGrinding)_rayCast.GetCollider(0);
			_usedRail.Call("StartSlide");
			SetPhysicsProcess(false);
		}
	}

	public void StopRail()
	{
		_usedRail.Call("StopSlide");
		SetPhysicsProcess(true);
		_usedRail = null;

		parent.Velocity = new Vector3(0, 10, 0);
	}

	public override void Exit()
	{
		GD.Print("Exiting Grinding State");
	}*/

	private RailGrinding _usedRail = null;
	private ShapeCast3D _grindCast;
	private Path3D _path;
	private new Node3D parent;
	
	private float grindSpeed = 10f;

    public override void Enter()
    {
        _grindCast = GetNode<ShapeCast3D>($"../../../ShapeCast3D");
		parent = GetNode<Node3D>($"../../..");
    }

    public override void Update(float delta)
    {
        StartRail();
    }



	public void StartRail()
	{
		bool onRail = _grindCast.IsColliding() && _grindCast.GetCollider(0) is RailGrinding;

		if(onRail)
		{
			_usedRail = (RailGrinding)_grindCast.GetCollider(0);
			_path = _usedRail.path;
			SetRailPosition();
		}
	}

    public void SetRailPosition()
    {
		Vector3 railPoint;
        _usedRail.CalculateTargetRailPoint(parent.GlobalPosition, out railPoint);
        _usedRail.CalculateDirection(_usedRail.pathFollow.GlobalTransform.Basis.Z, parent.GlobalTransform.Basis.Z);
		GD.Print(railPoint);

		parent.GlobalPosition = new Vector3((float)Mathf.Lerp(parent.GlobalPosition.X, railPoint.X, 0.35),
		 									(float)Mathf.Lerp(parent.GlobalPosition.Y, railPoint.Y, 0.35),
		  									(float)Mathf.Lerp(parent.GlobalPosition.Z, railPoint.Z, 0.35)
		);
    }

}
