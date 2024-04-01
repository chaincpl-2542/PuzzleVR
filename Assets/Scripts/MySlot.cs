using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySlot : MonoBehaviour
{
    MyRow myRow;
    public Vector2 RowAndSlot;
    public Brick brick;
    public SlotsManager slotsManager;

    public MySlot SlotAbove;
    public MySlot SlotBelow;
    public MySlot SlotRight;
    public MySlot SlotLeft;
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
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (RowAndSlot.y - 1 > -1)
                {
                    MoveDown();
                }
                else
                {
                    brick.currentSlot = gameObject.GetComponent<MySlot>();
                }
                timer = 0.1f;
            }
           
        }
         //BrickSlotUpdate();
    }

    public void MoveDown()
    {
        if (!slotsManager.allRow[(int)RowAndSlot.y - 1].mySlots[(int)RowAndSlot.x].brick)
        {

            brick.currentPosition.y -= 1;

            slotsManager.allRow[(int)RowAndSlot.y - 1].mySlots[(int)RowAndSlot.x].brick = brick;
            brick.isLock = false;
            brick.currentSlot = null;
            brick = null;
        }
        else
        {

            brick.currentSlot = gameObject.GetComponent<MySlot>();
        }
    }

    public void BrickSlotUpdate()
    {
        if (brick)
        {
            brick.currentSlot = gameObject.GetComponent<MySlot>();
        }
    }
    public void SlotSetup()
    {
        brick = null;
        if(RowAndSlot.y + 1 < slotsManager.allRow.Count)
        {
            SlotAbove = slotsManager.allRow[(int)RowAndSlot.y + 1].mySlots[(int)RowAndSlot.x];
        }

        if(RowAndSlot.y - 1 > -1)
        {
            SlotBelow = slotsManager.allRow[(int)RowAndSlot.y - 1].mySlots[(int)RowAndSlot.x];
        }

        if(RowAndSlot.x + 1 < 10)
        {
            SlotRight = slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x + 1];
        }

        if(RowAndSlot.x - 1 > -1)
        {
            SlotLeft = slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x - 1];
        }
    }

    public void SlotChainExpolde(Brick.Element _element,Brick.Element _plasma)
    {
        
        //print(gameObject.name +" >> " +_element);
        //Destory Plasma
        if(brick)
        {
            if(!brick.checkExplode)
            {
                brick.BrickExpolde();
            }
        }

        if(SlotAbove)
        {
            if(SlotAbove.brick)
            {
                if(!SlotAbove.brick.checkExplode)
                {
                    if(SlotAbove.brick.element == _element || SlotAbove.brick.element == _plasma)
                    {
                        SlotAbove.SlotChainExpolde(_element,_plasma);
                    }
                }
            }  
            
        }

        if(SlotBelow)
        {

            if(SlotBelow.brick)
            {
                if(!SlotBelow.brick.checkExplode)
                {
                    if(SlotBelow.brick.element == _element || SlotBelow.brick.element == _plasma)
                    {
                        SlotBelow.SlotChainExpolde(_element,_plasma);

                    }  
                }
            } 
            
        }

        if(SlotLeft)
        {
            if(SlotLeft.brick)
            {
                if(!SlotLeft.brick.checkExplode)
                {
                    if(SlotLeft.brick.element == _element || SlotLeft.brick.element == _plasma)
                    {
                        SlotLeft.SlotChainExpolde(_element,_plasma);
                    }   
                }
            }
        
        }

        if(SlotRight)
        {

            if(SlotRight.brick)
            {
                if(!SlotRight.brick.checkExplode)
                {
                    if(SlotRight.brick.element == _element || SlotRight.brick.element == _plasma)
                    {
                        SlotRight.SlotChainExpolde(_element,_plasma);
                    } 
                }
            }    
        }
    }
}
