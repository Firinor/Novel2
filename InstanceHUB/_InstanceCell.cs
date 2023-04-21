using System;
using UnityEngine;

namespace FirInstanceCell
{
    public class InstanceCell<T>
    {
        private T value;
        public void SetValue(T value)
        {
            if (value == null)
            {
                string msg = $"You cannot pass a null value {typeof(T).Name}!";
                Debug.LogError(msg);
                throw new Exception(msg);
            }

            this.value = value;
        }
        public T GetValue()
        {
            if (value == null)
            {
                string msg = $"At the moment, the value {typeof(T).Name} has not yet been assigned!";
                Debug.LogError(msg);
                throw new Exception(msg);
            }

            return value;
        }

        public bool isValueNull()
        {
            return value == null;
        }
    }
}