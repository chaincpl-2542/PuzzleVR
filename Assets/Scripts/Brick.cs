using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public VectorType vectorType;
    public Vector2 currentPosition;
    public Vector2 offSetFormMainBrick;

    Vector2 newValue;
    public Brick MainBrick;

    public bool isGhost;
    public bool outOfArea;

    public enum VectorType
    {
        None,
        PP,
        PN,
        NN,
        NP
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!MainBrick)
        {
            currentPosition = gameObject.GetComponentInParent<BrickController>().RowAndSlot;
        }
        else
        {
            currentPosition = MainBrick.currentPosition + offSetFormMainBrick;
        }
        
    }

    /*private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Slot")
        {
            currentPosition = other.gameObject.GetComponent<MySlot>().RowAndSlot;
            if (isGhost)
            {
                outOfArea = false;
            }

        }
        else if(other.gameObject.tag == "OutLine")
        {
            if (isGhost)
            {
                print("0");
                outOfArea = true;
            }
        }
    }*/
}
