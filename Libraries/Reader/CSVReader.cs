using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace FirSaveLoad
{
    public static class CSVReader
    {
        //[SerializeField]
        //private string pathToFile = "/Units/_UnitData.csv";

        public static List<List<string>> GetData(string pathToFile, int startLine = 1, char split = ';')
        {
            string path = Application.dataPath + pathToFile;

            string[] AllText = File.ReadAllLines(path, Encoding.GetEncoding("windows-1251"));

            return StringReader.GetData(AllText, startLine, split);
        }
    }
}
