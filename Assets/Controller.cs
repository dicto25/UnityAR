using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Controller : MonoBehaviour
{
    public BoxCollider[] Boxes;
    //public GameObject[] Targets;
    public string RecognizedString { get; private set; }  //read-only properity

    private List<BoxCollider> boxesPresent;
    private BoxCollider[] arrangedBoxes;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            boxesPresent = new List<BoxCollider>();

            foreach (BoxCollider bc in Boxes)  //add all presenting BoxCollider in a list
            {
                if (bc.enabled)
                    boxesPresent.Add(bc);
            }

            arrangedBoxes = boxesPresent.ToArray();

            Array.Sort(arrangedBoxes, delegate (BoxCollider b1, BoxCollider b2)  //arrange the BoxColliders accroding to their x position
            {
                return b1.transform.position.x.CompareTo(b2.transform.position.x);
            });

            //TODO: add a y-offset. Targets y-position exceeding the offset would be ignored

            RecognizedString = "";
            string str = "";
            foreach(BoxCollider bc in arrangedBoxes)
            {
                RecognizedString += bc.name;

                str += ", " + bc.name;  //for debug use
            }
            Debug.Log(str);  //for debug use
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
