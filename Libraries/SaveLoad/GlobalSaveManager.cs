using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static SaveManager;

namespace FirSaveLoad
{
    public static class GlobalSaveManager
    {
        public static void Save<T>(string path, T data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static OptionsParameters LoadOptions()
        {
            return Load<OptionsParameters>(GetOptionPath());
        }

        public static SaveData Load(int account)
        {
            Data = Load<SaveData>(GetPath(account));

            if (Data == null)
            {
                CreateNewSave(account);
            }

            return Data;
        }

        public static T Load<T>(string path)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
            {
                return default;
            }
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static bool FileExists(int i)
        {
            return FileExists(GetPath(i));
        }

        public static string GetPath(int i)
        {
            return GetPath() + $"data{i}.save";
        }

        public static string GetOptionPath()
        {
            return GetPath() + $"option.save";
        }

        public static string GetPath()
        {
            //C:\Users\<userprofile>\AppData\LocalLow\<companyname>\<productname>
            return Application.persistentDataPath;
        }

        public abstract class AbstractSaveData { }
    }
}
