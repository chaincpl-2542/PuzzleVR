using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI CD_Text;
    public GameObject CD_Panel;
    public float timer;
    public bool isCD;
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isCD = true;
        }

        if (isCD)
        {
            CD_Panel.SetActive(true);

            timer -= Time.deltaTime;
            CD_Text.text = timer.ToString("F0");

            if (timer <= 0)
            {
                CD_Panel.SetActive(false);
                isStart = true;
            }
        }

    }
}
