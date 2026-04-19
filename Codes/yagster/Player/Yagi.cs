using Godot;
using System;

public partial class Yagi : MeshInstance3D
{
	
	public override void _Ready()
	{
		GD.Print("Global Basis:\t" + GlobalBasis + "\nLocalBasis:\t" + Basis);
	}
	
	
	
}
