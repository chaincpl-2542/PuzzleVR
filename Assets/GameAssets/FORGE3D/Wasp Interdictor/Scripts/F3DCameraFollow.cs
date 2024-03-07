using UnityEngine;
using System.Collections;

public class F3DCameraFollow : MonoBehaviour {

    public Transform BGCamera;
    public Transform CameraOrigin;
    public float Pos, Rot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //  transform.position = Vector3.LerpUnclamped(transform.position, CameraOrigin.position, Time.deltaTime * Pos);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, CameraOrigin.rotation, Time.deltaTime * Rot);

	}

    void LateUpdate()
    {
       // BGCamera.rotation = Camera.main.transform.rotation;
        transform.position = CameraOrigin.position;
    }
}
