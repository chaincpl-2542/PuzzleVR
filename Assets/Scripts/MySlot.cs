using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySlot : MonoBehaviour
{
    MyRow myRow;
    public Vector2 RowAndSlot;
    public Brick brick;
    // Start is called before the first frame update
    void Start()
    {
        myRow = gameObject.GetComponentInParent<MyRow>();
        RowAndSlot.y = myRow.rowNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
