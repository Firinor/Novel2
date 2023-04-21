using System;
using UnityEngine;

namespace FirUnityEditor
{
#if UNITY_EDITOR
    public class NullCheck : PropertyAttribute
    {
        public Color NullFieldColor = Color.red;
    }
#else
    public class NullCheck : Attribute
    {
    }
#endif
}
