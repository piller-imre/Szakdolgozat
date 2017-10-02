using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {

    public GameObject cameraHolder;

    public void RotateVertical(float angle)
    {
        cameraHolder.transform.Rotate(new Vector3(1, 0, 0), angle);
    }

    public void RotateHorizontal(float angle)
    {
        cameraHolder.transform.Rotate(new Vector3(0, 1, 0), angle);
    }
}
