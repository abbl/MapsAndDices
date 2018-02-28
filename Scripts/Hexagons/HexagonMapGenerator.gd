extends Node

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

var chunksArray

func _ready():
	_generate_map(8, 4)
	pass

func _generate_map(width, height):
	for x in width:
		for y in height:
			if(y % 2):
				create_chunk(Vector2(x + 0.3, y - 0.17))
				continue
			#if(x % 2):	
				#create_chunk(Vector2(x, y - 0.25))
			#else:
			create_chunk(Vector2(x, y))
	pass

func create_chunk(vector):
	var chunk = load("res://Scripts/Hexagons/HexagonChunk.gd").new()
	add_child(chunk)
	chunk.set_fixed_position(vector)
	return chunk


#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
