using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ElementMerge : MonoBehaviour
{   
    public SlotsManager slotsManager;
    public Brick mainBrickToMerge;
    public Brick.Element element;
    public Vector2 currentPosition;
    public Vector2 brickSizeYX;

    public bool isSet;
    /////เช็ค Brick ที่จะรวมเป็นก้อน
    public List<Brick> brickRight;
    /////เช็ค Brick ที่จะรวมเป็นก้อน
    public List<Brick> brickLeft;
    /////เช็ค Brick ที่จะรวมเป็นก้อน
    public List<Brick> brickTop;
    /////เก็บ Brick ทั้งหมดที่จะรวมเป็นก้อนไว้ใน brickMergedList
    public List<Brick> brickMergedList;
    public Brick brickConer;
    public bool isMerge;
    public Brick mergeBy;

    public bool mergeHead;
    // Start is called before the first frame update
    void Start()
    {
        brickSizeYX = new Vector2(1,1);
        slotsManager = gameObject.GetComponent<Brick>().slotsManager;
        
    }

    // Update is called once per frame
    void Update()
    {
        brickRight = new List<Brick>((int)brickSizeYX.x);
        brickLeft = new List<Brick>((int)brickSizeYX.x);
        brickTop = new List<Brick>((int)brickSizeYX.y);

        currentPosition = gameObject.GetComponent<Brick>().currentPosition;
        element = gameObject.GetComponent<Brick>().element;
        if(isSet)
        {
            if(!isMerge)
            {
                ////Coner for 1x1
                if((int)currentPosition.x + (int)brickSizeYX.x < 10 && (int)currentPosition.y + (int)brickSizeYX.y < 16)
                {
                    if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick)
                    {
                        if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.isLock)
                        {
                            if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.element == element)
                            {
                                if(!slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.isMerged)
                                {
                                    brickConer = slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick;
                                    if(!mainBrickToMerge)
                                    {
                                        slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.GetComponent<ElementMerge>().mainBrickToMerge = gameObject.GetComponent<Brick>();
                                    }
                                }
                            }
                            else
                            {
                                brickConer = null;
                            }
                        }
                    }
                    else
                    {
                        brickConer = null;
                    }
                }

                for(int y = (int)brickSizeYX.y; y < brickSizeYX.y+1; y++)
                {
                    for(int x = 0;x < brickSizeYX.x; x++)
                    {
                        if(y != 0 && (int)currentPosition.y + y < 16)
                        {
                            if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick)
                            {
                                if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.isLock)
                                {
                                    if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.element == element)
                                    {
                                        if(!slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.isMerged)
                                        {
                                            brickTop.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for(int y = 0; y < brickSizeYX.y; y++)
                {
                    for(int x = (int)brickSizeYX.x;x < brickSizeYX.x+1; x++)
                    {
                        if(x != 0 && (int)currentPosition.x + x < 10)
                        {
                            if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick)
                            {
                                if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.isLock)
                                {
                                    if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.element == element)
                                    {
                                        if(!slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.isMerged)
                                        {
                                            brickRight.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for(int y = 0; y < brickSizeYX.y; y++)
                {
                    if(currentPosition.x > 0)
                    {
                        if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x - 1].brick)
                        {
                            if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x - 1].brick.isLock)
                            {
                                if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x - 1].brick.element == element)
                                {
                                    if(!slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x - 1].brick.isMerged)
                                    {
                                        brickLeft.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x - 1].brick);
                                    }
                                }
                            }
                        }
                    }
                }
                
                if(mergeHead){
                    if(brickLeft.Count == brickSizeYX.y)
                    {
                        for(int i = 0; i < brickMergedList.Count;i++)
                        {
                            brickMergedList[i].GetComponent<ElementMerge>().isMerge = true;
                            brickMergedList[i].GetComponent<ElementMerge>().mergeBy = brickLeft[0];
                            brickLeft[0].GetComponent<ElementMerge>().mergeHead = true;
                            brickLeft[0].GetComponent<ElementMerge>().brickSizeYX = brickSizeYX;
                            brickLeft[0].GetComponent<ElementMerge>().brickSizeYX.x = brickSizeYX.x+1;
                            brickMergedList[i].GetComponent<ElementMerge>().brickMergedList = new List<Brick>();
                        }
                    }
                }

                /////เก็บ Brick ทั้งหมดที่จะรวมเป็นก้อนไว้ใน brickMergedList
                brickMergedList = new List<Brick>((int)brickSizeYX.x * (int)brickSizeYX.y);
        
                for(int y = 0;y < brickSizeYX.y;y++)
                {
                    for(int x = 0;x < brickSizeYX.x;x++)
                    {
                        brickMergedList.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick);
                    }
                }

                ///กำหนด Merge By ให้ทุกอันใน list
                if(mergeHead == true)
                {
                    for(int i = 0; i < brickMergedList.Count;i++)
                    {
                        brickMergedList[i].ActiveMergedEffect(element);
                        brickMergedList[i].isMerged = true;
                        if(i == 0)
                        {
                            brickMergedList[i].GetComponent<ElementMerge>().mergeBy = null;
                            brickMergedList[i].GetComponent<ElementMerge>().isMerge = false;
                        }
                        else
                        {
                            brickMergedList[i].GetComponent<ElementMerge>().mergeBy = gameObject.GetComponent<Brick>();
                            brickMergedList[i].GetComponent<ElementMerge>().isMerge = true;
                            brickMergedList[i].GetComponent<ElementMerge>().mergeHead = false;
                        }
                        //if(brickMergedList[i].GetComponent<ElementMerge>().mergeBy != )
                    }
                }

            }
            else
            {
                brickRight = new List<Brick>();
                brickTop = new List<Brick>();
                brickConer = null;
                //brickMergedList = new List<Brick>();
            }
        }

        

        /////Update size of brickset
        if(brickTop.Count == brickSizeYX.x && brickRight.Count == brickSizeYX.y && brickConer)
        {
            brickConer.GetComponent<ElementMerge>().isMerge = true;
            brickTop[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().isMerge = true;
            brickRight[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().isMerge = true;

            if(mergeBy)
            {
                brickTop[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().mergeBy = mergeBy;

                brickRight[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().mergeBy = mergeBy;   

                brickConer.GetComponent<ElementMerge>().mergeBy = mergeBy;
            }
            else
            {
                brickTop[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().mergeBy = gameObject.GetComponent<Brick>();

                brickRight[(int)brickSizeYX.x-1].GetComponent<ElementMerge>().mergeBy = gameObject.GetComponent<Brick>();   

                brickConer.GetComponent<ElementMerge>().mergeBy = gameObject.GetComponent<Brick>();
            } 
        }

        if(!isMerge)
        {
            if(brickSizeYX != new Vector2(1,1))
            {
                if(brickTop.Count == brickSizeYX.x)
                {
                    brickSizeYX.y++;
                    mergeHead = true;
                }
                if(brickRight.Count == brickSizeYX.y)
                {
                    brickSizeYX.x++;
                    mergeHead = true;
                }
            }
            else if(brickRight.Count == 1 && brickTop.Count == 1 && brickConer)
            {
                brickSizeYX = new Vector2(2,2);
                mergeHead = true;
            }
        }

    }
}
