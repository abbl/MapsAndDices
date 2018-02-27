extends KinematicBody2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

export var playerSpeed = 5
onready var camera

func _ready():
	camera = get_node("Camera2D")
	pass

func _process(delta):
	read_player_input()
	camera.align()
	pass
	
func read_player_input():
	if Input.is_key_pressed(KEY_W):
		move_player(Vector2(0,-playerSpeed))
	elif Input.is_key_pressed(KEY_S):
		move_player(Vector2(0, playerSpeed))
	elif Input.is_key_pressed(KEY_D):
		move_player(Vector2(playerSpeed, 0))
	elif Input.is_key_pressed(KEY_A):
		move_player(Vector2(-playerSpeed, 0))
	pass	
	
	
func move_player(vector):
	move_and_collide(vector)
	pass

func move_camera(vector):
	camera.translate(vector)
	