using System;
using UnityEngine;

public class SinglBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    public bool SingletoneCheck(T instance, bool destroyIt = true)
    {
        bool isSingle = true;

        if (SinglBehaviour<T>.instance != null)
        {
            if (SinglBehaviour<T>.instance.gameObject != null)
            {
                if (SinglBehaviour<T>.instance != instance)
                {
                    isSingle = false;
                    //Debug.Log($"{SinglBehaviour<T>.instance.GetType()}");
                    //Debug.Log($"{SinglBehaviour<T>.instance.GetHashCode()}");
                    //Debug.Log($"{instance.GetHashCode()}");
                    if (destroyIt)
                    {
                        Destroy(gameObject);
                    }
                    return isSingle;
                }
            }
        }

        SinglBehaviour<T>.instance = instance;

        return isSingle;
    }
}