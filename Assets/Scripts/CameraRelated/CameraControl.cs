using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    private Camera camera;
    public float cameraSpeed;
    public float cameraScrollSpeed;
    public float maxCameraDistance;
    public float minCameraDistance;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        MoveCamera();
        ChangeCameraSize();
	}

    private void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * cameraSpeed, vertical * cameraSpeed, 0);
        camera.transform.position = new Vector3(camera.transform.position.x + movement.x, camera.transform.position.y + movement.y, 0);
    }

    private void ChangeCameraSize()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if((camera.orthographicSize - (1 * cameraScrollSpeed)) >= minCameraDistance)
            {
                camera.orthographicSize -= 1 * cameraScrollSpeed;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if ((camera.orthographicSize + (1 * cameraScrollSpeed)) <= maxCameraDistance)
            {
                camera.orthographicSize += 1 * cameraScrollSpeed;
            }
        }
    }
}
