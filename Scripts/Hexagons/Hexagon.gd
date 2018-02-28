extends Node2D

var sprite
var area2D
var collisionShape2D
var hexagonWidth
var hexagonHeight
var hexagonsOffsetX = -32
var hexagonsOffsetY = 1.5

func _ready():
	sprite = Sprite.new()
	_create_hexagon_sprite()
	_add_as_child()
	_define_hexagon_size()
	pass
	
func _create_hexagon_sprite():
	sprite.texture = _create_hexagon_texture()
	pass

func _create_hexagon_texture():
	return preload("res://Textures/test_hexagon.png")

func _define_hexagon_size():
	hexagonWidth = sprite.texture.get_width()
	hexagonHeight = sprite.texture.get_height()
	pass

func _add_as_child():
	add_child(sprite, true)
	pass
	
func set_position(vector):
	position = _calculate_fixed_cords(vector)
	pass
	
func _calculate_fixed_cords(vector):
	var x = vector.x * (hexagonWidth + hexagonsOffsetX)
	var y = vector.y * (hexagonHeight + hexagonsOffsetY)
	return Vector2(x, y)

func get_width():
	return hexagonWidth

func get_height():
	return hexagonHeight