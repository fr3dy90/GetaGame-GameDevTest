using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckPointCreator))]
public class ChecPointCreatorEditor : Editor
{
   public override void OnInspectorGUI()
   {
      DrawDefaultInspector();
      CheckPointCreator myScript = (CheckPointCreator) target;
      if (GUILayout.Button("Crear CheckPoints"))
      {
         myScript.CreateCheckPoints();
      }
   }
}
