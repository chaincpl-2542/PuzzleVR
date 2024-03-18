using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySlot : MonoBehaviour
{
    MyRow myRow;
    public Vector2 RowAndSlot;
    public Brick brick;
    SlotsManager slotsManager;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.1f;
        slotsManager = gameObject.GetComponentInParent<SlotsManager>();
        myRow = gameObject.GetComponentInParent<MyRow>();
        RowAndSlot.y = myRow.rowNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (brick)
        {
            if (RowAndSlot.y - 1 > -1)
            {
                if (!slotsManager.allRow[(int)RowAndSlot.y - 1].mySlots[(int)RowAndSlot.x].brick)
                {
                    slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x].brick.isLock = false;

                        brick.currentPosition.y -= 1;

                        slotsManager.allRow[(int)RowAndSlot.y - 1].mySlots[(int)RowAndSlot.x].brick = brick;
                        brick = null;
                    
                }
                else
                {
                    slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x].brick.isLock = true;
                }
            }
            else
            {
                slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x].brick.isLock = true;
            }
        }
    }
}
