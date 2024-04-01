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
    public List<Brick> brickRight;
    public List<Brick> brickTop;
    public Brick brickConer;
    public bool isMerge;
    public Brick mergeBy;
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
        brickTop = new List<Brick>((int)brickSizeYX.y);

        currentPosition = gameObject.GetComponent<Brick>().currentPosition;
        element = gameObject.GetComponent<Brick>().element;
        if(isSet)
        {
            /*
            if((currentPosition.y > -1 && currentPosition.y + (int)brickSizeYX.y < 16) && (currentPosition.x > -1 && currentPosition.x + (int)brickSizeYX.x < 10))
            {
                if(slotsManager.allRow[(int)currentPosition.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick)
                {
                    if(slotsManager.allRow[(int)currentPosition.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.element == element)
                    {
                        brickRight.Add(slotsManager.allRow[(int)currentPosition.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick);

                        if(!mainBrickToMerge)
                        {
                            slotsManager.allRow[(int)currentPosition.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.GetComponent<ElementMerge>().mainBrickToMerge = gameObject.GetComponent<Brick>();
                        }
                    }
                }

                if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x].brick)
                {
                    if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x].brick.element == element)
                    {
                        brickTop.Add(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x].brick);
                        
                        if(!mainBrickToMerge)
                        {
                            slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x].brick.GetComponent<ElementMerge>().mainBrickToMerge = gameObject.GetComponent<Brick>();
                        }
                    }
                }
            }*/

            if(!isMerge)
            {
                ////Coner for 1x1
                if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick)
                {
                    if(slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.element == element)
                    {
                        brickConer = slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick;
                        if(!mainBrickToMerge)
                        {
                            slotsManager.allRow[(int)currentPosition.y + (int)brickSizeYX.y].mySlots[(int)currentPosition.x + (int)brickSizeYX.x].brick.GetComponent<ElementMerge>().mainBrickToMerge = gameObject.GetComponent<Brick>();
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
                                if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.element == element)
                                {
                                    brickTop.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick);
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
                                if(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick.element == element)
                                {
                                    brickRight.Add(slotsManager.allRow[(int)currentPosition.y + y].mySlots[(int)currentPosition.x + x].brick);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                brickRight = new List<Brick>();
                brickTop = new List<Brick>();
                brickConer = null;
            }
        }


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

        if(brickSizeYX != new Vector2(1,1))
        {
            if(brickTop.Count == brickSizeYX.x)
            {
                brickSizeYX.x++;
            }
            if(brickRight.Count == brickSizeYX.y)
            {
                brickSizeYX.y++;
            }
        }
    }
}
