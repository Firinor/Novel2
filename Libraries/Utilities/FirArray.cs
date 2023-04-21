using System;
using System.Collections.Generic;

namespace FirUtilities
{
    public static class ArrayUtilities
    {
        public static string[] StringArraySlicing(string fullText, int textCapacity)
        {
            string[] straws1 = GetStringStraws(fullText, "? ", "! ", ". ", Environment.NewLine);

            for (int i = 0; i < straws1.Length; i++)
            {
                if (straws1[i].Length > textCapacity)
                {
                    string[] straws2 = GetStringStraws(straws1[i], ", ");
                    for (int j = 0; j < straws2.Length; j++)
                    {
                        if (straws2[j].Length > textCapacity)
                        {
                            string[] straws3 = GetStringStraws(straws2[j], " ");
                            ExpandArray(ref straws2, ref j, straws3);
                        }
                    }
                    ExpandArray(ref straws1, ref i, straws2);
                }
            }

            return straws1;
        }

        public static void ExpandArray<T>(ref T[] mainArray, ref int index, T[] insertedArray)
        {
            //Putting the array in the center of the parent array
            //mainStraws {22, 22, (index) 333 333 333, 22, 22} index = 2; Length = 5;
            //insertedStraws {333, 333, 333} Length = 3;
            //new mainStraws {22, 22, 333, 333, (index) 333, 22, 22} index = 4; Length = 7;

            //{ , , , , , , }
            T[] buffer = new T[mainArray.Length - 1 + insertedArray.Length];
            //{22, 22, , , , , }
            Array.Copy(mainArray, 0, buffer, 0, index);
            //{22, 22, 333, 333, 333, , }
            Array.Copy(insertedArray, 0, buffer, index, insertedArray.Length);
            //{22, 22, 333, 333, 333, 22, 22}
            Array.Copy(mainArray, index + 1, buffer, index + insertedArray.Length, mainArray.Length - index - 1);

            mainArray = buffer;

            index += insertedArray.Length - 1;
        }
        public static string[] СleanArray(string[] mainArray)
        {
            List<string> temp = new List<string>();
            foreach (string s in mainArray)
            {
                if (!string.IsNullOrWhiteSpace(s))
                    temp.Add(s);
            }
            return temp.ToArray();
        }
        public static T[] СleanArrayFromNull<T>(T[] mainArray)
        {
            if (mainArray == null)
                throw new ArgumentNullException("The array is null!");

            List<T> temp = new List<T>();
            foreach (T s in mainArray)
            {
                if (s != null)
                    temp.Add(s);
            }
            return temp.ToArray();
        }
        public static List<T> СleanArrayFromNull<T>(List<T> mainArray)
        {
            if(mainArray == null)
                throw new ArgumentNullException("The array is null!");

            List<T> temp = new List<T>();
            foreach (T s in mainArray)
            {
                if (s != null)
                    temp.Add(s);
            }
            return temp;
        }

        public static string[] GetStringStraws(string fullText, params string[] spliters)
        {
            string aRareSequenceOfCharacters = "&&&&%&&&&;@$№&&";
            string CutText = fullText.Substring(0, fullText.Length);
            foreach (string spliter in spliters)
            {
                CutText = CutText.Replace(spliter, spliter + aRareSequenceOfCharacters);
            }

            return CutText.Split(new string[1] { aRareSequenceOfCharacters }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
