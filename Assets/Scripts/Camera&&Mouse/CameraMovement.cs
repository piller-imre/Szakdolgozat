using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float speed = 1;
    public bool startMoving;
    Vector3 direction;

	void Update () {

        if (startMoving)
        {
            target.position += direction;
        }
	}

    public void StartMovingForward(bool opposite)
    {
        startMoving = true;

        direction = (!opposite) ? target.forward : -target.forward;
        direction *= speed;
    }

    public void StartMovingSlides(bool opposite)
    {
        startMoving = true;

        direction = (!opposite) ? target.right : -target.right;
        direction *= speed;
    }

    public void StopMoving()
    {
        startMoving = false;
    }
}
