using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public List<GameObject> allBricks;
    public GameObject shape1;
    public GameObject shape2;
    public GameObject shape3;
    public GameObject shape4;
    public GameObject shape5;
    public GameObject shape6;
    public GameObject shape7;

    public Transform spawnPoint;
    public Transform bricksContainer;
    SlotsManager slotsManager;
    // Start is called before the first frame update
    void Start()
    {
        slotsManager = gameObject.GetComponentInChildren<SlotsManager>();

        SpawnBrick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBrick()
    {
        int num = Random.Range(0, allBricks.Count);
        GameObject BrickSet = Instantiate(allBricks[num], spawnPoint.position, spawnPoint.rotation);
        BrickSet.transform.SetParent(bricksContainer);
        BrickSet.GetComponent<BrickController>().mainSlot = spawnPoint;
        BrickSet.GetComponent<BrickController>().slotsManager = slotsManager;
    }
}
