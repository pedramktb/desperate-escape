using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UnityCore.Data
{
    public class SaveLoader
    {
        readonly string m_fileName;

        public SaveLoader(string fileName)
        {
            m_fileName = fileName;
        }

        #region Public Functions
        public void SaveData(IData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + m_fileName;
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public IData LoadData() 
        {
            string path = Application.persistentDataPath + "/" + m_fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                IData data = formatter.Deserialize(stream) as IData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogWarning("Save File Was Not Found in " + path);
                return null;
            }
        }
        #endregion
    }
}