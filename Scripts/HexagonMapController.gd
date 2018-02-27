extends Node2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

var exampleHexagon

func _ready():
	add_hexagon_on_screen()
	pass

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass

func add_hexagon_on_screen():
	add_child(prepare_hexagon_entity(Vector2(300, 300)))
	pass

func prepare_hexagon_entity(vector):
	exampleHexagon = load("res://Scripts/Hexagon.gd").new()
	return exampleHexagon

