using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace LesserKnown.LoadingScreen
{
    [ExecuteInEditMode]
    public class LoadingManager: MonoBehaviour
    {
        public LoadingScreenPattern pattern;       
        public GameObject loading_ui;
        public string scene_name;

        [Tooltip("If left to -1 the scene_name variable will be used instead")]
        public int scene_index = -1;

        [HideInInspector]
        public LoadingUIHolder ui_holder;
       

        public LoadingUIHolder CreateLoader()
        {
            if(ui_holder != null)
            {
                DestroyImmediate(ui_holder.gameObject);
                ui_holder = null;
            }


            LoadingUIHolder _uiHolder = null;

            if (scene_index == -1 && scene_name == string.Empty)
            {
                Debug.LogError("Scene Name is empty, loader will fail");
                return null;
            }

            if (ui_holder == null)
            {
                var _copy = Instantiate(loading_ui);
                _uiHolder = _copy.GetComponent<LoadingUIHolder>();
            }

            return _uiHolder;
        }

        private void Awake()
        {
            if(ui_holder == null)
            {
                Debug.LogError("The loading UI holder is not created yet.");
                return;
            }

            ui_holder.Set_Default(scene_index, scene_name, pattern);
        }

        public void LoadScene()
        {
            if(ui_holder == null)
            {
                Debug.LogError("The loading UI holder is not created yet.");
                return;
            }

            ui_holder.LoadScene();
        }
    }
}
