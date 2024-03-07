using UnityEngine;
using System.Collections;

public class F3DShipFX : MonoBehaviour {

   // public SKShip SKShipRef;
    public Transform[] LeftEngine, RightEngine;

    [Range(0.0f, 1.0f)]
    public float ServoCurrentPos;
   
    [Range(0.0f, 45.0f)]
    public float ServoLimit;
    
    public float ServoSpeed;
   
    float servoPos;
    float servoStep;

	// Use this for initialization
	void Start () {
	
	}
    public static float ScaleRange(float value, float oldA, float oldB, float newA, float newB)
    { return (((newB - newA) * (value - oldA)) / (oldB - oldA)) + newA; }

    // Update is called once per frame
    void Update () {
        
        float offset = ScaleRange(Mathf.Clamp01(ServoCurrentPos), 0f, 1f, 0f, ServoLimit) - servoPos;
        servoStep = ServoSpeed * (offset / 2f) * Time.deltaTime;            

        for(int i = 0; i < LeftEngine.Length; ++i)
        {
            LeftEngine[i].transform.localRotation *= Quaternion.Euler(servoStep, 0, 0);
            RightEngine[i].transform.localRotation *= Quaternion.Euler(servoStep, 0, 0);            
        }

        servoPos += servoStep;

     

        
	}
}
