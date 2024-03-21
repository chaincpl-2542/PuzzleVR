using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public VectorType vectorType;
    public Vector2 currentPosition;
    public Vector2 offSetFormMainBrick;

    public Brick MainBrick;

    public bool isLock;
    public bool isHit;
    bool check;
    public SlotsManager slotsManager;

    public Element element;
    public ElementType elementType;
    public MySlot currentSlot;
    float myTimer = 0.5f;

    [Header("Score")]
    public ScoreManager scoreManager;
    public int scoreCount;
    public Brick plasmaExplode;
    public bool checkExplode;

    

    [Header("Elements")]
    public MeshRenderer normalBrick;

    [Header("Magma")]
    public GameObject MagmaElement;
    public GameObject MagmaElement_Effect; 
    public GameObject Plasma_MagmaElement;
    public GameObject Magma_ExplodeEffect;

    [Header("Electric")]
    public GameObject ElectricElement;
    public GameObject ElectricElement_Effect;
    public GameObject Plasma_ElectricElement;
    public GameObject Electric_ExplodeEffect;

    [Header("Water")]
    public GameObject WaterElement;
    public GameObject WaterElement_Effect;
    public GameObject Plasma_WaterElement;
    public GameObject Water_ExplodeEffect;

    [Header("Wind")]
    public GameObject WindElement;
    public GameObject WindElement_Effect;
    public GameObject Plasma_WindElement;
    public GameObject Wind_ExplodeEffect;

    [Header("Earth")]
    public GameObject EarthElement;
    public GameObject EarthElement_Effect;
    public GameObject Plasma_EarthElement;
    public GameObject Earth_ExplodeEffect;

    public enum ElementType
    {
        Element,
        Plasma
    }
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

        if(currentSlot)
        {
            ///////////
            // Check isLock เช็คจากล่างขึ้นบน
            //isLock คือ ลงมาชิดล่างสุดแล้วให้ทำคำสั่งอื่นต่อ
            if(currentPosition.y == 0 )
            {
                isLock = true;
            }

            if(isLock == true)
            {
                
                if(currentSlot.SlotAbove.brick)
                {
                    {
                        currentSlot.SlotAbove.brick.isLock = true;
                    }
                }
            }
            ///////////
            ///
            ///isLock แล้วให้เช็ค slot บนก่อนว่ามีอะไรหรือเปล่าแล้วค่อยทำอย่างอื่น กับบัค
            if(isLock && currentSlot.SlotAbove)
            {
                myTimer -= 1 * Time.deltaTime;
                if(myTimer <= 0)
                {
                    CheckPlasmaHit();
                }
            }
            else
            {
                myTimer = 0.5f;
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
                elementType = ElementType.Element;
                break;

            case Element.Electric:
                ActiveElectric();
                elementType = ElementType.Element;
                break;

            case Element.Water:
                ActiveWater();
                elementType = ElementType.Element;
                break;

            case Element.Wind:
                ActiveWind();
                elementType = ElementType.Element;
                break;

            case Element.Earth:
                ActiveEarth();
                elementType = ElementType.Element;
                break;
            case Element.Plasma_Magma:
                ActivePlasmaMagma();
                elementType = ElementType.Plasma;
                break;

            case Element.Plasma_Electric:
                ActivePlasmaElectric();
                elementType = ElementType.Plasma;
                break;

            case Element.Plasma_Water:
                ActivePlasmaWater();
                elementType = ElementType.Plasma;
                break;

            case Element.Plasma_Wind:
                ActivePlasmaWind();
                elementType = ElementType.Plasma;
                break;

            case Element.Plasma_Earth:
                ActivePlasmaEarth();
                elementType = ElementType.Plasma;
                break;
        }
    }

    public void CheckPlasmaHit()
    {
        //StartCoroutine(DelayPlasmaCheck());
        if(currentSlot)
        {
            if(currentSlot.SlotAbove)
            {
                if(currentSlot.SlotAbove.brick)
                {
                    if(currentSlot.SlotAbove.brick.isLock)
                    {
                        if(element == Element.Plasma_Magma)
                        {
                            if(currentSlot.SlotAbove.brick.element == Element.Magma || currentSlot.SlotAbove.brick.element == Element.Plasma_Magma)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Magma,Element.Plasma_Magma));
                                //currentSlot.SlotChainExpolde(Element.Magma);
                            }
                        }
                        else if(element == Element.Plasma_Electric)
                        {
                            if(currentSlot.SlotAbove.brick.element == Element.Electric || currentSlot.SlotAbove.brick.element == Element.Plasma_Electric)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Electric,Element.Plasma_Electric));
                                //currentSlot.SlotChainExpolde(Element.Electric);
                            }
                        }
                        else if(element == Element.Plasma_Water)
                        {
                            if(currentSlot.SlotAbove.brick.element == Element.Water || currentSlot.SlotAbove.brick.element == Element.Plasma_Water)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Water,Element.Plasma_Water));
                                //currentSlot.SlotChainExpolde(Element.Water);
                            }
                        }
                        else if(element == Element.Plasma_Wind)
                        {
                            if(currentSlot.SlotAbove.brick.element == Element.Wind || currentSlot.SlotAbove.brick.element == Element.Plasma_Wind)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Wind,Element.Plasma_Wind));
                                //currentSlot.SlotChainExpolde(Element.Wind);
                            }
                        }
                        else if(element == Element.Plasma_Earth)
                        {
                            if(currentSlot.SlotAbove.brick.element == Element.Earth || currentSlot.SlotAbove.brick.element == Element.Plasma_Earth)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Earth,Element.Plasma_Earth));
                                //currentSlot.SlotChainExpolde(Element.Earth);
                            }
                        }
                    }
                }
            }

            if(currentSlot.SlotBelow)
            {
                if(currentSlot.SlotBelow.brick)
                {
                    if(currentSlot.SlotBelow.brick.isLock)
                    {
                        if(element == Element.Plasma_Magma)
                        {
                            if(currentSlot.SlotBelow.brick.element == Element.Magma || currentSlot.SlotBelow.brick.element == Element.Plasma_Magma)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Magma,Element.Plasma_Magma));
                                //currentSlot.SlotChainExpolde(Element.Magma);
                            }
                        }
                        else if(element == Element.Plasma_Electric)
                        {
                            if(currentSlot.SlotBelow.brick.element == Element.Electric || currentSlot.SlotBelow.brick.element == Element.Plasma_Electric)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Electric,Element.Plasma_Electric));
                                //currentSlot.SlotChainExpolde(Element.Electric);
                            }
                        }
                        else if(element == Element.Plasma_Water)
                        {
                            if(currentSlot.SlotBelow.brick.element == Element.Water || currentSlot.SlotBelow.brick.element == Element.Plasma_Water)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Water,Element.Plasma_Magma));
                                //currentSlot.SlotChainExpolde(Element.Water);
                            }
                        }
                        else if(element == Element.Plasma_Wind)
                        {
                            if(currentSlot.SlotBelow.brick.element == Element.Wind || currentSlot.SlotBelow.brick.element == Element.Plasma_Wind)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Wind,Element.Plasma_Wind));
                                //currentSlot.SlotChainExpolde(Element.Wind);
                            }
                        }
                        else if(element == Element.Plasma_Earth)
                        {
                            if(currentSlot.SlotBelow.brick.element == Element.Earth || currentSlot.SlotBelow.brick.element == Element.Plasma_Earth)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Earth,Element.Plasma_Earth));
                                //currentSlot.SlotChainExpolde(Element.Earth);
                            }
                        }
                    }
                }
            }

            if(currentSlot.SlotLeft)
            {
                if(currentSlot.SlotLeft.brick)
                {
                    if(currentSlot.SlotLeft.brick.isLock)
                    {
                        if(element == Element.Plasma_Magma)
                        {
                            if(currentSlot.SlotLeft.brick.element == Element.Magma || currentSlot.SlotLeft.brick.element == Element.Plasma_Magma)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Magma,Element.Plasma_Magma));
                                //currentSlot.SlotChainExpolde(Element.Magma);
                            }
                        }
                        else if(element == Element.Plasma_Electric)
                        {
                            if(currentSlot.SlotLeft.brick.element == Element.Electric || currentSlot.SlotLeft.brick.element == Element.Plasma_Electric)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Electric,Element.Plasma_Electric));
                                //currentSlot.SlotChainExpolde(Element.Electric);
                            }
                        }
                        else if(element == Element.Plasma_Water)
                        {
                            if(currentSlot.SlotLeft.brick.element == Element.Water || currentSlot.SlotLeft.brick.element == Element.Plasma_Water)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Water,Element.Plasma_Water));
                                //currentSlot.SlotChainExpolde(Element.Water);
                            }
                        }
                        else if(element == Element.Plasma_Wind)
                        {
                            if(currentSlot.SlotLeft.brick.element == Element.Wind || currentSlot.SlotLeft.brick.element == Element.Plasma_Wind)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Wind,Element.Plasma_Wind));
                                //currentSlot.SlotChainExpolde(Element.Wind);
                            }
                        }
                        else if(element == Element.Plasma_Earth)
                        {
                            if(currentSlot.SlotLeft.brick.element == Element.Earth || currentSlot.SlotLeft.brick.element == Element.Plasma_Earth)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Earth,Element.Plasma_Earth));
                                //currentSlot.SlotChainExpolde(Element.Earth);
                            }
                        }
                    }
                }
            }

            if(currentSlot.SlotRight)
            {
                if(currentSlot.SlotRight.brick)
                {
                    if(currentSlot.SlotRight.brick.isLock)
                    {
                        if(element == Element.Plasma_Magma)
                        {
                            if(currentSlot.SlotRight.brick.element == Element.Magma || currentSlot.SlotRight.brick.element == Element.Plasma_Magma)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Magma,Element.Plasma_Magma));
                                //currentSlot.SlotChainExpolde(Element.Magma);
                            }
                        }
                        else if(element == Element.Plasma_Electric)
                        {
                            if(currentSlot.SlotRight.brick.element == Element.Electric || currentSlot.SlotRight.brick.element == Element.Plasma_Electric)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Electric,Element.Plasma_Electric));
                                //currentSlot.SlotChainExpolde(Element.Electric);
                            }
                        }
                        else if(element == Element.Plasma_Water)
                        {
                            if(currentSlot.SlotRight.brick.element == Element.Water || currentSlot.SlotRight.brick.element == Element.Plasma_Water)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Water,Element.Plasma_Water));
                                //currentSlot.SlotChainExpolde(Element.Water);
                            }
                        }
                        else if(element == Element.Plasma_Wind)
                        {
                            if(currentSlot.SlotRight.brick.element == Element.Wind || currentSlot.SlotRight.brick.element == Element.Plasma_Wind)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Wind,Element.Plasma_Wind));
                                //currentSlot.SlotChainExpolde(Element.Wind);
                            }
                        }
                        else if(element == Element.Plasma_Earth)
                        {
                            if(currentSlot.SlotRight.brick.element == Element.Earth || currentSlot.SlotRight.brick.element == Element.Plasma_Earth)
                            {
                                StartCoroutine(DelayPlasmaCheck(Element.Earth,Element.Plasma_Earth));
                                //currentSlot.SlotChainExpolde(Element.Earth);
                            }
                        }
                    }
                }
            }
        }
    }
    public IEnumerator DelayPlasmaCheck(Element _element, Element _plasma)
    {
        if(!check && gameObject.GetComponentInParent<BrickController>().allCheck)
        {
             check = true;
            yield return new WaitForSeconds(0.5f);

            if(isLock && currentSlot.SlotAbove)
            {
                myTimer -= 1f * Time.deltaTime;
                if(myTimer <= 0)
                {
                    
                    PlasmaSlotCheck(_element, _plasma);
                }
            }
           
        }
    }
    public void PlasmaSlotCheck(Element _element, Element _plasma)
    {
        currentSlot.SlotChainExpolde(_element,_plasma);
    }

    public void BrickExpolde()
    {
        checkExplode = true;
        scoreManager.score++;
        //print(elementType.ToString() + " : " + 1.ToString());
        
        if(element == Element.Magma || element == Element.Plasma_Magma)
        {
            GameObject effect = Instantiate(Magma_ExplodeEffect,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(effect,2);
        }
        else if(element == Element.Electric || element == Element.Plasma_Electric)
        {
            GameObject effect = Instantiate(Electric_ExplodeEffect,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(effect,2);
        }
        else if(element == Element.Water || element == Element.Plasma_Water)
        {
            GameObject effect = Instantiate(Water_ExplodeEffect,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(effect,2);
        }
        else if(element == Element.Wind || element == Element.Plasma_Wind)
        {
            GameObject effect = Instantiate(Wind_ExplodeEffect,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(effect,2);
        }
        else if(element == Element.Earth || element == Element.Plasma_Earth)
        {
            GameObject effect = Instantiate(Earth_ExplodeEffect,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(effect,2);
        }

        Destroy(gameObject);
    }

    public IEnumerator DelayScore()
    {
        yield return new WaitForSeconds(0.1f);
        scoreManager.score += scoreCount;
        Destroy(gameObject);
    }

#region Element
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
#endregion 
}
