using UnityEngine;
using System.Collections;

public class CameraTools
{
    public static void FocusCameraOnGameObject(Camera camera, GameObject target)
    {
        camera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, camera.transform.position.z);
    }
}
