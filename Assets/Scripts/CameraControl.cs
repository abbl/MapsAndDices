using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    private GameObject mainCamera;
    public float cameraSpeed;
    public float cameraScrollSpeed;

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Camera");
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
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + movement.x, mainCamera.transform.position.y + movement.y, 0);
    }

    private void ChangeCameraSize()
    {
        Camera camera = mainCamera.GetComponentInChildren<Camera>();

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            camera.orthographicSize += 1 * cameraScrollSpeed;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            camera.orthographicSize -= 1 * cameraScrollSpeed;
        }
    }
}
