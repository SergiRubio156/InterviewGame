using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectManager))]
public class ObjectListControllerEditor : Editor
{
    /*public override void OnInspectorGUI()
    {
        ObjectManager controller = (ObjectManager)target;

        for (int i = 0; i < controller.objectList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            // Mostrar el objeto en la lista
            controller.objectList[i] = (GameObject)EditorGUILayout.ObjectField(controller.objectList[i], typeof(GameObject), true);

            // Mostrar el estado del objeto en la enum como una lista desplegable
            controller.objectStates[i] = (ObjectState)EditorGUILayout.EnumPopup(controller.objectStates[i]);

            EditorGUILayout.EndHorizontal();
        }
        
    }*/
}