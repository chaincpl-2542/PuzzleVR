using UnityEngine;
using System.Collections;

public class F3DCockpitCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public Transform BackgroundCamera;

    public float RotationTime = 5f;
    float mouseX, mouseY;

    // Update is called once per frame
    void Update () {

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

      //  mouseX = Mathf.Clamp(mouseX, -90f, 90f);
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(mouseY, mouseX, 0f), Time.deltaTime * RotationTime);

        if (BackgroundCamera)
            BackgroundCamera.rotation = Quaternion.Euler(0f, 0f, 0f) * transform.rotation;

    }
}
