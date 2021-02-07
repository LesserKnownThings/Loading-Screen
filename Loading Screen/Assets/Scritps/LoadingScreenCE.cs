using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace LesserKnown.LoadingScreen
{
    [CustomEditor(typeof(LoadingManager))]
    [CanEditMultipleObjects]
    public class LoadingScreenCE : Editor
    {
        
        public override void OnInspectorGUI()
        {
            LoadingManager _target = (LoadingManager)target;

            DrawDefaultInspector();
            

            if(GUILayout.Button("Create Loader"))
            {
               _target.ui_holder =  _target.CreateLoader();
            }
        }
    }
}
