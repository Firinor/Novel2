using System;
using Object = UnityEngine.Object;

namespace FirParser
{
    public static class StringParser
    {
        public static T ParseTo<T>(string Name, bool ToLower = true) where T : System.Enum
        {
            var enume = typeof(T);

            var values = enume.GetEnumValues();

            foreach(var value in values)
            {
                if (ToLower)
                {
                    if(value.ToString().ToLower() == Name.ToLower())
                        return (T)value;
                }
                else
                {
                    if (value.ToString() == Name)
                        return (T)value;
                }
            }

            return default(T);
        }

        public static T NotSafeFindField<T>(string Name, object Librari, bool ToLower = true) 
            where T : class
        {
            var classe = Librari.GetType();

            if (ToLower)
            {
                var values = classe.GetFields();

                string name = $"{typeof(T).ToString().ToLower()} {Name.ToLower()}";

                foreach (var value in values)
                {
                    if (value.ToString().ToLower() == name)
                        return (T)value.GetValue(Librari);
                }
            }
            else
            {
                try
                {
                    T result = (T)classe.GetField(Name).GetValue(Librari);
                    return result;
                }
                catch
                {
                    //Exception below in the script
                }
            }

            throw new InvalidCastException($"Couldn't find in {classe} a field by string {Name}!");
            //return default(T1);
        }

        public static T FindField<T>(string Name, T[] Librari, bool ToLower = true)
            where T : Object
        {
            if (ToLower)
            {   
                string name = Name.ToLower();
                foreach (var value in Librari)
                {
                    if (value.name.ToLower() == name)
                        return value;
                }
            }
            else
            {
                foreach (var value in Librari)
                {
                    if (value.ToString() == Name)
                        return value;
                }
            }

            throw new InvalidCastException($"The \"{Name}\" field could not be found in the library {Librari}");
            //return default(T1);
        }
    }
}
