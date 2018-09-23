using System.Collections;
using System.Collections.Generic;
using ARobj;
using System.Xml.Serialization;
using System.IO;

namespace ARIO
{
    public class ObjectSaver
    {
        private List<ObjectData> dataToSave;
        private readonly string filepath = "";

        public ObjectSaver()
        {
            dataToSave = new List<ObjectData>();
        }

        public void AddData(string textToRecognize, string objFileName)
        {
            ObjectData obj = new ObjectData();
            obj.Text = textToRecognize;
            obj.FileName = objFileName;
            dataToSave.Add(obj);
        }

        public void AddData(params ObjectData[] datas)
        {
            dataToSave.AddRange(datas);
        }

        public bool SaveData()
        {
            try
            {
                if (dataToSave.Count != 0)
                {
                    ObjectData[] dataArray = dataToSave.ToArray();
                    XmlSerializer serializer = new XmlSerializer(typeof(ObjectData[]));
                    using (StreamWriter writer = new StreamWriter(filepath))
                    {
                        serializer.Serialize(writer, dataArray);
                        writer.Close();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
    }

    public class ObjectLoader
    {

    }
}
