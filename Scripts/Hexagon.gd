extends Node2D

var sprite
var area2D
var collisionShape2D

func _ready():
	sprite = Sprite.new()
	create_hexagon_sprite()
	add_as_child()
	pass
	
func create_hexagon_sprite():
	sprite.texture = create_hexagon_texture()
	pass

func create_hexagon_texture():
	return preload("res://Textures/test_hexagon.png")
	
func add_as_child():
	add_child(sprite, true)
	pass