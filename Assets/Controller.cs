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
    public BoxCollider[] Boxes;
    public GameObject[] Targets;
    public string RecognizedString { get; private set; }  //read-only properity

    private string lastString;
    private List<BoxCollider> boxesPresent;
    private BoxCollider[] arrangedBoxes;
    private ObjectData[] objList;

    private readonly string filepath = Path.Combine(Environment.CurrentDirectory,"data.xml");  //name of the xml file MUST be data.xml

    // Use this for initialization
    void Start()
    {   
        ObjectLoader loader = new ObjectLoader();
        objList = loader.LoadXML(this.filepath);
        if (objList == null)
            throw new Exception("FAILIED TO LOAD XML FILE");

        //Load XML file at start
        /*XmlSerializer deserializer = new XmlSerializer(typeof(ObjectData[]));
        using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        {
            objList = deserializer.Deserialize(fs) as ObjectData[];
        }
        if (objList == null)
            throw new Exception("FAILED TO LOAD XML");
            */
    }

    // Update is called once per frame
    void Update()
    {
        //try
        //{
            boxesPresent = new List<BoxCollider>();

            foreach (BoxCollider bc in Boxes)  //add all presenting BoxCollider in a list
            {
                if (bc.enabled)
                    boxesPresent.Add(bc);
            }

            arrangedBoxes = boxesPresent.ToArray();

            Array.Sort(arrangedBoxes, delegate (BoxCollider b1, BoxCollider b2)  //arrange the BoxColliders accroding to their x position using delegate method
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

        //}
        //catch (Exception e)
        //{
            //Debug.Log(e.Message);
        //}
    }

    private bool checkDetectChange()
    {
        if (RecognizedString == lastString)
            return false;
        else
            return true;
    }
}
