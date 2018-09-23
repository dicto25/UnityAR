using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Controller : MonoBehaviour
{
    public BoxCollider[] Boxes;
    public GameObject[] Targets;

    private List<BoxCollider> boxesPresent;
    private BoxCollider[] arrangedBoxes;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //try
        {
            boxesPresent = new List<BoxCollider>();

            foreach (BoxCollider bc in Boxes)
            {
                if (bc.enabled)
                    boxesPresent.Add(bc);
            }

            float currentLeast = 9999;
            arrangedBoxes = new BoxCollider[boxesPresent.Count];
            for (int i = 0; i < boxesPresent.Count; i++)
            {
                int x = 0;
                for (;x<boxesPresent.Count ; x++)
                {
                    GameObject imageObj = Targets[x];
                    if (imageObj.transform.position.x < currentLeast)
                    {
                        currentLeast = imageObj.transform.position.x;
                        break;
                    }
                    else if (i > boxesPresent.Count)
                        throw new System.Exception("Exception occured in line30 loop");
                }
                try
                {
                    arrangedBoxes[i] = boxesPresent[x];
                }
                catch
                {
                    Debug.Log("Index: " + x + ", " + i);
                }
            }

            string message = "";
            foreach (BoxCollider bc in arrangedBoxes)
            {
                message += bc.name;
            }
            Debug.Log(message);
            //}
            /*catch(System.Exception e)
            {
                Debug.Log(e.Message);
            }*/
        }
    }
}
