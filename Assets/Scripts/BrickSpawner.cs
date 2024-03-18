using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> allBricks;

    public Transform spawnPoint;
    public Transform bricksContainer;
    public Transform BrickOrderContainer;
    SlotsManager slotsManager;

    private BrickController brickController;

    public List<BricksElementList> bricksElementList = new List<BricksElementList>();
    public int MaxBricksSetList;

    bool startRandomBrickSet;
    bool startSpawn;

    GameObject nextExampleSet;

    [System.Serializable]
    public class BricksElementList
    {
        public GameObject setList;
        public List<Brick.Element> elements = new List<Brick.Element>();
    }

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        slotsManager = gameObject.GetComponentInChildren<SlotsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager)
        {
            if (gameManager.isCD)
            {
                if (!startRandomBrickSet)
                {
                    RandomBricksElementList();
                    RandomElementList();
                    startRandomBrickSet = true;
                }
            }

            if (gameManager.isStart)
            {
                if (!startSpawn)
                {
                    SpawnBrick();
                    startSpawn = true;
                }
            }
        }
    }

    public void RandomBricksElementList()
    {
        for (int i = 0; i < MaxBricksSetList; i++)
        {
            BricksElementList newBrickselementList = new BricksElementList();
            bricksElementList.Add(newBrickselementList);

            BricksElementList newBricksSet = new BricksElementList();
            int setNum = Random.Range(0, allBricks.Count);
            bricksElementList[i].setList = allBricks[setNum];
        }
    }

    public void SpawnExampleSet()
    {
        if (nextExampleSet)
        {
            Destroy(nextExampleSet);
        }

        nextExampleSet = Instantiate(bricksElementList[bricksElementList.Count - 1].setList, BrickOrderContainer.position, BrickOrderContainer.rotation);
        nextExampleSet.GetComponent<BrickController>().isMoving = false;
        nextExampleSet.GetComponent<BrickController>().enabled = false;
        BrickController nextExampleSet_BrickController = nextExampleSet.GetComponent<BrickController>();

        for (int i = 0; i < nextExampleSet_BrickController.brickList.Count; i++)
        {
            nextExampleSet_BrickController.brickList[i].GetComponent<Brick>().element = bricksElementList[bricksElementList.Count - 1].elements[i];
            nextExampleSet_BrickController.brickList[i].CheckElement();
            nextExampleSet_BrickController.brickList[i].slotsManager = slotsManager;
        }
    }

    public void SpawnBrick()
    {
        if (bricksElementList.Count > 0) 
        {
            GameObject BrickSet = Instantiate(bricksElementList[bricksElementList.Count - 1].setList, spawnPoint.position, spawnPoint.rotation);
            BrickSet.transform.SetParent(bricksContainer);
            brickController = BrickSet.GetComponent<BrickController>();
            brickController.mainSlot = spawnPoint;
            brickController.slotsManager = slotsManager;
            brickController.isPlayingThis = true;


            for (int i = 0; i < brickController.brickList.Count; i++)
            {
                brickController.brickList[i].GetComponent<Brick>().element = bricksElementList[bricksElementList.Count - 1].elements[i];
                brickController.brickList[i].CheckElement();
                brickController.brickList[i].slotsManager = slotsManager;
            }

            bricksElementList.RemoveAt(bricksElementList.Count - 1);
            //RandomElement();
        }
        SpawnExampleSet();
    }

    public void RandomElementList()
    {
        for (int i = 0; i < bricksElementList.Count; i++)
        {
            for (int a = 0; a < 4; a++)
            {
                Brick.Element newElement = new Brick.Element();
                bricksElementList[i].elements.Add(newElement);

                int numRandomPlasma = Random.Range(0, 5);
                if (numRandomPlasma > 0)
                {
                    int brickNum = Random.Range(0, 5);
                    switch (brickNum)
                    {
                        case 0:
                            bricksElementList[i].elements[a] = Brick.Element.Magma;

                            break;
                        case 1:
                            bricksElementList[i].elements[a] = Brick.Element.Electric;

                            break;
                        case 2:
                            bricksElementList[i].elements[a] = Brick.Element.Water;

                            break;
                        case 3:
                            bricksElementList[i].elements[a] = Brick.Element.Wind;

                            break;
                        case 4:
                            bricksElementList[i].elements[a] = Brick.Element.Earth;

                            break;
                    }
                }
                else
                {
                    int plsamaNum = Random.Range(0, 5);
                    switch (plsamaNum)
                    {
                        case 0:
                            bricksElementList[i].elements[a] = Brick.Element.Plasma_Magma;

                            break;
                        case 1:
                            bricksElementList[i].elements[a] = Brick.Element.Plasma_Electric;

                            break;
                        case 2:
                            bricksElementList[i].elements[a] = Brick.Element.Plasma_Water;

                            break;
                        case 3:
                            bricksElementList[i].elements[a] = Brick.Element.Plasma_Wind;

                            break;
                        case 4:
                            bricksElementList[i].elements[a] = Brick.Element.Plasma_Earth;

                            break;
                    }
                }
            }
        }
    }

  
}
