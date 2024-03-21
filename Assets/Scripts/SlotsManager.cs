using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    public List<MyRow> rowList;
    public List<Row> allRow;
    // Start is called before the first frame update

    [System.Serializable]
    public class Row
    {
        public MyRow myrow;
        public List<MySlot> mySlots;
    }
    void Start()
    {
        for (int i = 0; i < allRow.Count; i++)
        {
            foreach (MySlot slot in allRow[i].myrow.GetComponentsInChildren<MySlot>())
            {
                allRow[i].mySlots.Add(slot);
            }
        }

        for (int i = 0; i < allRow.Count; i++)
        {
            for (int a = 0; a < allRow[i].mySlots.Count; a++)
            {
                allRow[i].mySlots[a].SlotSetup();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
