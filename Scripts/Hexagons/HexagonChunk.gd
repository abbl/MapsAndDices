extends Node2D

var hexagonArray
var hexagonsPredefinedPosition 

func _ready():
	_init_fields()
	_generate_hexagons_node()
	_place_hexagons_in_defined_position()
	pass

func _init_fields():
	hexagonArray = []
	hexagonsPredefinedPosition = [Vector2(0, 0), Vector2(0, 1), Vector2(1, 0.5), 
	Vector2(1, -0.5), Vector2(0, -1), Vector2(-1, -0.5), Vector2(-1, 0.5)]

func _generate_hexagons_node():
	for i in 7:
		hexagonArray.append(_load_hexagon())
	pass

func _load_hexagon():
	var hexagon = load("res://Scripts/Hexagons/Hexagon.gd").new()
	add_child(hexagon, true)
	return hexagon

func _place_hexagons_in_defined_position():
	var index = 0
	for i in hexagonArray:
		i.set_position(hexagonsPredefinedPosition[index])
		index += 1
	pass

func set_fixed_position(vector):
	var chunkWidth = 2.5 * hexagonArray[0].get_width()
	var chunkHeight = 3.05 * hexagonArray[0].get_height()
	position = Vector2(vector.x * (chunkWidth), vector.y * (chunkHeight))
	pass

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
