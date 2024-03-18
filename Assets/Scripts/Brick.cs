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

    public bool isHit;
    public bool isLock;
    public SlotsManager slotsManager;

    public Element element;

    [Header("Elements")]
    public MeshRenderer normalBrick;
    [Header("Magma")]
    public GameObject MagmaElement;
    public GameObject MagmaElement_Effect; 
    public GameObject Plasma_MagmaElement;

    [Header("Electric")]
    public GameObject ElectricElement;
    public GameObject ElectricElement_Effect;
    public GameObject Plasma_ElectricElement;

    [Header("Water")]
    public GameObject WaterElement;
    public GameObject WaterElement_Effect;
    public GameObject Plasma_WaterElement;

    [Header("Wind")]
    public GameObject WindElement;
    public GameObject WindElement_Effect;
    public GameObject Plasma_WindElement;

    [Header("Earth")]
    public GameObject EarthElement;
    public GameObject EarthElement_Effect;
    public GameObject Plasma_EarthElement;

    public enum Element
    {
        None,
        Magma,
        Electric,
        Water,
        Wind,
        Earth,
        Plasma_Magma,
        Plasma_Electric,
        Plasma_Water,
        Plasma_Wind,
        Plasma_Earth
    }
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
            if (!isHit) 
            {
                currentPosition = gameObject.GetComponentInParent<BrickController>().RowAndSlot;
            }
        }
        else
        {
            if (!isHit)
            {
                currentPosition = MainBrick.currentPosition + offSetFormMainBrick;
            }
        }

        if (isHit)
        {
            if (slotsManager)
            {
                transform.position = slotsManager.allRow[(int)currentPosition.y].mySlots[(int)currentPosition.x].transform.position;
            }
        }

        
    }

    public void CheckElement()
    {
        switch (element)
        {
            case Element.None:

                break;

            case Element.Magma:
                ActiveMagma();
                break;

            case Element.Electric:
                ActiveElectric();
                break;

            case Element.Water:
                ActiveWater();
                break;

            case Element.Wind:
                ActiveWind();
                break;

            case Element.Earth:
                ActiveEarth();
                break;
            case Element.Plasma_Magma:
                ActivePlasmaMagma();
                break;

            case Element.Plasma_Electric:
                ActivePlasmaElectric();
                break;

            case Element.Plasma_Water:
                ActivePlasmaWater();
                break;

            case Element.Plasma_Wind:
                ActivePlasmaWind();
                break;

            case Element.Plasma_Earth:
                ActivePlasmaEarth();
                break;
        }
    }

    public void ActiveMagma()
    {
        MagmaElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActiveElectric()
    {
        ElectricElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActiveWater()
    {
        WaterElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActiveWind()
    {
        WindElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActiveEarth()
    {
        EarthElement.SetActive(true);
        normalBrick.enabled = false;
    }
    public void ActivePlasmaMagma()
    {
        Plasma_MagmaElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActivePlasmaElectric()
    {
        Plasma_ElectricElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActivePlasmaWater()
    {
        Plasma_WaterElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActivePlasmaWind()
    {
        Plasma_WindElement.SetActive(true);
        normalBrick.enabled = false;
    }

    public void ActivePlasmaEarth()
    {
        Plasma_EarthElement.SetActive(true);
        normalBrick.enabled = false;
    }
}
