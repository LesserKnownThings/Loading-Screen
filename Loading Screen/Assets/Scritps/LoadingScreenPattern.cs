using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LesserKnown.LoadingScreen
{
    [CreateAssetMenu(fileName = "Settings", menuName = "LesserKnown/Scene Settings")]
    public class LoadingScreenPattern : ScriptableObject
    {
        public Sprite[] loading_images;
        public string[] screen_tips;
        public Sprite loading_bar;
        public bool image_swap;
        [Range(2f, 1000f)]
        public float swap_delay;
        [Range(2f, 1000f)]
        public float text_swap_delay;

        [Space(10)]
        [Header("Sider Options")]
        public Color slider_bg;
        public Color slider_loader;
        public Sprite slider_loader_sprite;
        public bool use_sprite;
        public bool show_loading_amount;

        [Space(10)]
        [Header("Text Options")]
        public Color text_color;
        
    }
}
