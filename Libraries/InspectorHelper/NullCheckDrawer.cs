using UnityEngine;
using UnityEditor;

namespace FirUnityEditor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(NullCheck))]
    public class NullCheckDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            NullCheck red = attribute as NullCheck;

            if (property.objectReferenceValue == null)
            {
                Color color = GUI.color;
                GUI.color = red.NullFieldColor;
                EditorGUI.PropertyField(position, property, label);
                GUI.color = color;
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
    #endif
}
