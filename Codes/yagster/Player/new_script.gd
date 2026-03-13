extends Node

@export var grind_rays:Node3D

func get_valid_ray():
	for grind_ray in grind_rays.get_children():
		if grind_ray.is_colliding() and grind_ray.get_collider() and grind_ray.get_collider().is_in_group("Rails"):
			return grind_ray
	return null
