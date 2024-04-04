using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using BNG;

public class BrickController : MonoBehaviour
{
    public BrickSpawner brickSpawner;

    public Transform mainSlot;
    public SlotsManager slotsManager;
    public Vector2 RowAndSlot;
    public List<Brick> brickList;
    public GameObject ghostBlockPrefab;
    public ScoreManager scoreManager;
    public bool isPlayingThis;
    public bool isMoving;
    public bool canRotate;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool canMoveDown;
    public bool allCheck;
    int checkLockNum;
    
    
    bool callSpawn;

    public int MaxY;
    public int MaxX;

    public RotationType rotationType;
    // For 2 sides
    int count;
    // For 2 sides

    float timer;

    GameObject ghostPivot;
    public enum RotationType
    {
        None,
        TwoSides,
        FourSides

    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.7f;
        isMoving = true;
        canMoveLeft = true;
        canMoveRight = true;
        canMoveDown = true;
        brickSpawner = gameObject.GetComponentInParent<BrickSpawner>();
        RowAndSlot = mainSlot.GetComponent<MySlot>().RowAndSlot;

        //CreateGhostBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingThis)
        {
            if (isMoving)
            {
                if (timer > 0)
                {
                    timer -= 1 * Time.deltaTime;
                }
                else
                {
                    //BrickGoDown();
                }


                if (Input.GetKeyDown(KeyCode.S))
                {
                    BrickGoDown();

                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    if (canMoveLeft)
                    {
                        if (RowAndSlot.x > 0)
                        {
                            RowAndSlot.x -= 1;
                            UpdateBrickPosition();
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (canMoveRight)
                    {
                        if (RowAndSlot.x < 9)
                        {
                            RowAndSlot.x += 1;
                            UpdateBrickPosition();
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isMoving)
                    {
                        if (canRotate)
                        {
                            BrickRotate();
                        }
                    }
                }

                if(InputBridge.Instance.RightThumbstickDown)
                {
                     BrickGoDown();
                }

                if(InputBridge.Instance.RightThumbstickAxis.y > 1)
                {
                    if (canMoveRight)
                    {
                        if (RowAndSlot.x < 9)
                        {
                            RowAndSlot.x += 1;
                            UpdateBrickPosition();
                        }
                    }
                }

                if(InputBridge.Instance.RightThumbstickAxis.y < -1)
                {
                    if (canMoveLeft)
                    {
                        if (RowAndSlot.x > 0)
                        {
                            RowAndSlot.x -= 1;
                            UpdateBrickPosition();
                        }
                    }
                }

                if(InputBridge.Instance.AButton || InputBridge.Instance.BButton)
                {
                    if (isMoving)
                    {
                        if (canRotate)
                        {
                            BrickRotate();
                        }
                    }
                }


                RotateCheck();
                MoveLeftRightCheck();
                BrickGoDownCheck();
            }
        }
        if(gameObject.transform.childCount <= 0)
        {
            Destroy(gameObject);
        }

    #region Check all bricks is lock
        for(int i = 0; i < brickList.Count; i++)
        {
            
            if(brickList[i].isLock)
            {
                checkLockNum++;
            }
            else
            {
                checkLockNum = 0;
            }
        }
        if(checkLockNum >= brickList.Count)
        {
            allCheck=true;
            if (!callSpawn)
            {
                StartCoroutine(brickSpawner.DelaySpawn());
                callSpawn = true;
            }

        }
    }
    #endregion

    public void GetAllBricks()
    {
        foreach (Brick brick in gameObject.GetComponentsInChildren<Brick>())
        {
            brickList.Add(brick);
            
            brick.slotsManager = slotsManager;
        }
    }

    public void UpdateBrickPosition()
    {
        if (RowAndSlot.y < slotsManager.allRow.Count && 
            (RowAndSlot.x < slotsManager.allRow[(int)RowAndSlot.y].mySlots.Count))
        {
            gameObject.transform.position = slotsManager.allRow[(int)RowAndSlot.y].mySlots[(int)RowAndSlot.x].transform.position;
        }

        //UpdatePosition
        for (int i = 0; i < brickList.Count; i++)
        {
            if (brickList[i].MainBrick)
            {
                brickList[i].currentPosition = RowAndSlot + brickList[i].offSetFormMainBrick;
                brickList[i].transform.position = slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].transform.position;
            }
        } 
    }
    public void MoveLeftRightCheck()
    {
        //Check move Left/Right
        canMoveLeft = true;
        canMoveRight = true;
        for (int i = 0; i < brickList.Count; i++)
        {

            if (brickList[i].currentPosition.x == 0 )
            {
                canMoveLeft = false;
                
            }

            if ((int)brickList[i].currentPosition.y > -1 && (int)brickList[i].currentPosition.x -1 > -1)
            {
                if (slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x - 1].brick)
                {
                    {
                        canMoveLeft = false;
                    }
                }
            }
            //เพิ่มช่องแล้วเพิ่มอันนี้ด้วย
            if (brickList[i].currentPosition.x == MaxX - 1)
            {
                canMoveRight = false;
            }

            if ((int)brickList[i].currentPosition.y > -1 && (int)brickList[i].currentPosition.x + 1 < MaxX)
            {
                //print(((int)brickList[i].currentPosition.y).ToString() + " : " + ((int)brickList[i].currentPosition.x + 1).ToString());
                if (slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x + 1].brick)
                {
                    {
                        canMoveRight = false;
                    }
                }
            }
        }
    }

    public void BrickGoDownCheck()
    {
        canMoveDown = true;

        for (int i = 0; i < brickList.Count; i++)
        {
            if (brickList[i].currentPosition.y - 1 > -1)
            {
                if (slotsManager.allRow[(int)brickList[i].currentPosition.y - 1].mySlots[(int)brickList[i].currentPosition.x].brick)
                {
                    canMoveDown = false;
                    break;
                }
            }
            else
            {
                canMoveDown = false;
                break;
            }
        }
    }

    public void BrickGoDown()
    {
        timer = 0.7f;
        if (canMoveDown)
        {
            if (RowAndSlot.y > -1)
            {
                RowAndSlot.y -= 1;
            }
        }
        else
        {
            BrickLock();
        }

        UpdateBrickPosition();
    }

    public void BrickLock()
    {
        isMoving = false;
        for (int i = 0; i < brickList.Count; i++)
        {
            if (brickList[i])
            {
                slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].GetComponent<MySlot>().brick = brickList[i];
                brickList[i].isHit = true;

                brickList[i].currentSlot = slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].GetComponent<MySlot>();

                
            }
        }
        

    }

    public void RotateCheck()
    {
        canRotate = true;
        if (rotationType == RotationType.FourSides)
        {
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].MainBrick)
                {
                    Vector2 xy;
                    xy.x = brickList[i].offSetFormMainBrick.y;
                    xy.y = brickList[i].offSetFormMainBrick.x;

                     if (brickList[i].vectorType == Brick.VectorType.PP)
                    {
                        xy.y = xy.y * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.NN)
                    {
                        xy.y = xy.y * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.PN)
                    {
                        xy.x = xy.x * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.NP)
                    {
                        xy.y = xy.y * -1;
                    }

                    if (brickList[i].vectorType == Brick.VectorType.PP)
                    {

                        if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX))
                        {
                            //print("CanRotateX" + " : " + (xy.x + RowAndSlot.x) + " : " + brickList[i].name);
                        }
                        else
                        {
                            canRotate = false;
                            break;
                        }

                        if(xy.y + RowAndSlot.y > -1)
                        {
                            //print("CanRotateY" + " : " + (xy.y + RowAndSlot.y) + " : " + brickList[i].name);

                        }
                        else
                        {
                            canRotate = false;
                            break;
                        }
                        
                        //CheckNearAnotherBrick
                        if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                        {
                            if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                            {

                                canRotate = false;
                                break;
                            }
                        }
                    }

                    if (brickList[i].vectorType == Brick.VectorType.NN)
                    {

                        if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX))
                        {
                            //print("CanRotateX" + " : " + (xy.x + RowAndSlot.x) + " : " + brickList[i].name);
                        }
                        else
                        {
                            canRotate = false;
                            break;
                        }

                        if(xy.y + RowAndSlot.y > -1)
                        {
                            //print("CanRotateY" + " : " + (xy.y + RowAndSlot.y) + " : " + brickList[i].name);

                        }
                        else
                        {
                            canRotate = false;
                            break;
                        }
                        
                        //CheckNearAnotherBrick
                        if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                        {
                            if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                            {

                                canRotate = false;
                                break;
                            }
                        }
                    }

                    else if (brickList[i].vectorType == Brick.VectorType.PN || brickList[i].vectorType == Brick.VectorType.NP)
                    {
                        if ((xy.y * -1) + RowAndSlot.y > -1 && (xy.y * -1) + RowAndSlot.y < MaxY)
                        {
                            //print("CanRotateX" + " : " + (xy.y + RowAndSlot.y) + " : " + brickList[i].name);
                        }
                        else
                        {
                            canRotate = false;
                            break;
                        }

                        //CheckNearAnotherBrick
                        if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                        {
                            if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                            {
                                canRotate = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        else if (rotationType == RotationType.TwoSides)
        {
            if (count == 0)
            {
                for (int i = 0; i < brickList.Count; i++)
                {
                    if (brickList[i].MainBrick)
                    {
                        Vector2 xy = new Vector2();
                        xy.x = brickList[i].offSetFormMainBrick.y;
                        xy.y = brickList[i].offSetFormMainBrick.x;

                        if (brickList[i].vectorType == Brick.VectorType.PP || brickList[i].vectorType == Brick.VectorType.NN)
                        {

                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX))
                            {
                                //print("CanRotateX" + " : " + (xy.x + RowAndSlot.x) + " : " + brickList[i].name);

                            }
                            else
                            {
                                canRotate = false;
                                break;
                            }

                            //CheckNearAnotherBrick
                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                            {
                                if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                                {
                                    canRotate = false;
                                    break;
                                }
                            }

                        }
                        else if (brickList[i].vectorType == Brick.VectorType.PN || brickList[i].vectorType == Brick.VectorType.NP)
                        {

                            if ((xy.x * -1) + RowAndSlot.x > -1 && (xy.x * -1) + RowAndSlot.x < MaxX)
                            {
                                //print((xy.x * -1) + RowAndSlot.x);
                                //print("CanRotateX" + brickList[i].name);
                            }
                            else
                            {
                                canRotate = false;
                                break;
                            }

                            //CheckNearAnotherBrick
                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                            {
                                if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                                {
                                    canRotate = false;
                                    break;
                                }
                            }
                        }

                    }
                }
            }
            else if (count == 1)
            {
                for (int i = 0; i < brickList.Count; i++)
                {
                    if (brickList[i].MainBrick)
                    {
                        Vector2 xy;
                        xy.x = brickList[i].offSetFormMainBrick.y;
                        xy.y = brickList[i].offSetFormMainBrick.x;

                        if (brickList[i].vectorType == Brick.VectorType.PP || brickList[i].vectorType == Brick.VectorType.NN)
                        {

                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX))
                            {
                                //print("CanRotateX" + " : " + (xy.x + RowAndSlot.x) + " : " + brickList[i].name);

                            }
                            else
                            {
                                canRotate = false;
                                break;
                            }

                            //CheckNearAnotherBrick
                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                            {
                                if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                                {
                                    canRotate = false;
                                    break;
                                }
                            }
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.PN || brickList[i].vectorType == Brick.VectorType.NP)
                        {

                            if ((xy.y * -1) + RowAndSlot.y > 0 && (xy.y * -1) + RowAndSlot.y < MaxY)
                            {
                                //print("CanRotate");

                            }
                            else
                            {
                                canRotate = false;
                                break;
                            }

                            //CheckNearAnotherBrick
                            if ((xy.x + RowAndSlot.x > -1) && (xy.x + RowAndSlot.x < MaxX) && xy.y + RowAndSlot.y > -1)
                            {
                                if (slotsManager.allRow[(int)xy.y + (int)RowAndSlot.y].mySlots[(int)xy.x + (int)RowAndSlot.x].brick)
                                {
                                    canRotate = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void BrickRotate()
    {
        if (rotationType == RotationType.FourSides)
        {
            for (int i = 0; i < brickList.Count; i++)
            {
                if (brickList[i].MainBrick)
                {
                    Vector2 xy;
                    xy = brickList[i].offSetFormMainBrick;
                    brickList[i].offSetFormMainBrick.x = xy.y;
                    brickList[i].offSetFormMainBrick.y = xy.x;

                    if (brickList[i].vectorType == Brick.VectorType.PP)
                    {
                        brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.NN)
                    {
                        brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.PN)
                    {
                        brickList[i].offSetFormMainBrick.x = brickList[i].offSetFormMainBrick.x * -1;
                    }
                    else if (brickList[i].vectorType == Brick.VectorType.NP)
                    {
                        brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                    }

                    brickList[i].transform.position = slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].transform.position;
                }
            }
        }
        else if (rotationType == RotationType.TwoSides)
        {
            if (count == 0)
            {
                count++;
                for (int i = 0; i < brickList.Count; i++)
                {
                    if (brickList[i].MainBrick)
                    {
                        Vector2 xy;
                        xy = brickList[i].offSetFormMainBrick;
                        brickList[i].offSetFormMainBrick.x = xy.y;
                        brickList[i].offSetFormMainBrick.y = xy.x;


                        if (brickList[i].vectorType == Brick.VectorType.PP)
                        {
                            brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.NN)
                        {
                            brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.PN)
                        {
                            brickList[i].offSetFormMainBrick.x = brickList[i].offSetFormMainBrick.x * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.NP)
                        {
                            brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                        }

                        brickList[i].transform.position = slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].transform.position;
                    }
                }
            }
            else if (count == 1)
            {
                count = 0;
                for (int i = 0; i < brickList.Count; i++)
                {
                    if (brickList[i].MainBrick)
                    {
                        Vector2 xy;
                        xy = brickList[i].offSetFormMainBrick;
                        brickList[i].offSetFormMainBrick.x = xy.y;
                        brickList[i].offSetFormMainBrick.y = xy.x;

                        if (brickList[i].vectorType == Brick.VectorType.PP)
                        {
                            brickList[i].offSetFormMainBrick.x = brickList[i].offSetFormMainBrick.x * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.NN)
                        {
                            brickList[i].offSetFormMainBrick.x = brickList[i].offSetFormMainBrick.x * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.PN)
                        {
                            brickList[i].offSetFormMainBrick.y = brickList[i].offSetFormMainBrick.y * -1;
                        }
                        else if (brickList[i].vectorType == Brick.VectorType.NP)
                        {
                            brickList[i].offSetFormMainBrick.x = brickList[i].offSetFormMainBrick.x * -1;  
                        }

                        brickList[i].transform.position = slotsManager.allRow[(int)brickList[i].currentPosition.y].mySlots[(int)brickList[i].currentPosition.x].transform.position;
                    }
                }
            }
        }
        UpdateBrickPosition();
    }

    
}
