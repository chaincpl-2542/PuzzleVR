using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRow : MonoBehaviour
{
    public int rowNumber;
    public List<MySlot> mySlots;
    // Start is called before the first frame update
    void Start()
    {

        foreach (MySlot slot in gameObject.GetComponentsInChildren<MySlot>())
        {
            mySlots.Add(slot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
