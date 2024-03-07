using UnityEngine;
using System.Collections;

public class F3DSkysphere : MonoBehaviour {

    public Transform Cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = Cam.position;

	}
}
