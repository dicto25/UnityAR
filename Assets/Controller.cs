using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using ARobj;
using System.Xml.Serialization;
using System.IO;
using ARIO;

public class Controller : MonoBehaviour
{
    //CAUTION: XML may not be loaded in Android platform

    //TODO: SQL server data storage and access (may do)
    //TODO: y-coordinate offset checking
    //TODO: automatic object resize and rotate
    //TODO: finish all the TODOs

    public BoxCollider[] Boxes;
    public GameObject[] Targets;
    public string RecognizedString { get; private set; }  //read-only properity

    private string lastString;
    private List<BoxCollider> boxesPresent;
    private BoxCollider[] arrangedBoxes;
    private ObjectData[] objList;

    private readonly string filepath = Path.Combine(Environment.CurrentDirectory,"data.xml");  //name of the xml file MUST be data.xml

    private int currentArrayIndex = 0;
    private readonly float timeToWait = 0.1f;  //need to set higher for weak cameras   //the higher the time-to-wait, the slower the program runs
    private float deltaT = 0;
    private bool isLoopDone = false;

    // Use this for initialization
    void Start()
    {   
        ObjectLoader loader = new ObjectLoader();
        objList = loader.LoadXML(this.filepath);
        if (objList == null)
            throw new Exception("FAILIED TO LOAD XML FILE");

        foreach(GameObject t in Targets)
        {
            t.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLoopDone)
        {
            deltaT += Time.deltaTime;

            if (deltaT < timeToWait)
            {
                if (currentArrayIndex == 26)
                {
                    isLoopDone = true;
                    currentArrayIndex = 0;
                    deltaT = 0;
                }

                else
                {
                    Targets[currentArrayIndex].SetActive(true);

                    if (Boxes[currentArrayIndex].enabled)
                    {
                        boxesPresent.Add(Boxes[currentArrayIndex]);
                        Targets[currentArrayIndex].SetActive(false);
                        currentArrayIndex++;
                        deltaT = 0;
                    }
                }
            }
            else
            {
                currentArrayIndex++;
                deltaT = 0;
            }
        }

        else
        {
            arrangedBoxes = boxesPresent.ToArray();

            Array.Sort(arrangedBoxes, delegate (BoxCollider b1, BoxCollider b2)
            {
                return b1.transform.position.x.CompareTo(b2.transform.position.x);
            });

            //TODO: add a y-offset. Targets y-position exceeding the offset would be ignored

            RecognizedString = "";
            string str = "";
            foreach (BoxCollider bc in arrangedBoxes)
            {
                RecognizedString += bc.name;

                str += ", " + bc.name;  //for debug use
            }
            Debug.Log(RecognizedString);  //for debug use
            if (objList != null)
            {
                if (checkDetectChange())
                {
                    foreach (ObjectData od in objList)
                    {
                        if (RecognizedString == od.Text)
                        {
                            GameObject obj = Instantiate(Resources.Load(od.FileName)) as GameObject;

                            int medianPos = arrangedBoxes.Length / 2;
                            obj.transform.parent = Targets[medianPos].transform;
                            obj.SetActive(true);
                        }
                    }

                    lastString = RecognizedString;
                }
            }
        isLoopDone = false;
        }
    }

    private bool checkDetectChange()
    {
        if (RecognizedString == lastString)
            return false;
        else
            return true;
    }
}
