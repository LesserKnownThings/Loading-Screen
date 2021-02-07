using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LesserKnown.LoadingScreen
{
    public class LoadingUIHolder : MonoBehaviour
    {
        protected LoadingScreenPattern pattern;

        [Space(10)]
        [Header("UI Settings")]
        public CanvasGroup loading_ui;
        public GameObject[] image_swappers;
        public TextMeshProUGUI tip_text;

        [Space(10)]
        [Header("Slider Options")]
        public Slider loading_slider;
        public TextMeshProUGUI slider_text;
        public Image loading_bar_image;
        public Image bg_image;

        private int scene_index;
        private string scene_name;
                    

        public void Set_Default(int _index, string _name, LoadingScreenPattern _pattern)
        {
            pattern = _pattern;

            loading_ui.gameObject.SetActive(false);
            loading_ui.alpha = 0f;
            tip_text.text = string.Empty;

            //Slider Options
            if (!pattern.show_loading_amount)
                slider_text.gameObject.SetActive(false);

            if (pattern.use_sprite)
                loading_bar_image.sprite = pattern.slider_loader_sprite;
            else
                loading_bar_image.color = pattern.slider_loader;

            bg_image.color = pattern.slider_bg;

            scene_index = _index;
            scene_name = _name;

            
        }

        /// <summary>
        /// Activate this when scene is loading
        /// </summary>
        public void LoadScene()
        {
            StartCoroutine(LoadSceneIE());
        }

        private IEnumerator LoadSceneIE()
        {
            loading_ui.gameObject.SetActive(true);
            slider_text.text = "0%";

            #region Image Config
            image_swappers[1].SetActive(false);

            Image _firstImg = image_swappers[0].GetComponent<Image>();
            Image _scndtImg = image_swappers[1].GetComponent<Image>();

            _firstImg.sprite = pattern.loading_images[Random_Index()];
            #endregion

            if(pattern.screen_tips.Length > 0)
            StartCoroutine(ChangeText_IE());

            //Turns on the loading screen
            while (loading_ui.alpha < 1)
            {
                loading_ui.alpha += Time.deltaTime * 2.5f;
                yield return null;
            }

            AsyncOperation _operation;
            if (scene_index == -1)
                _operation = SceneManager.LoadSceneAsync(scene_name);
            else
                _operation = SceneManager.LoadSceneAsync(scene_index);

            float _currentSwapDelay = 0f;

            while (!_operation.isDone)
            {
                if (pattern.show_loading_amount)
                    slider_text.text = $"{_operation.progress*100:0}%";

                loading_slider.value = _operation.progress;

                if (pattern.image_swap)
                {
                    
                    if(_currentSwapDelay >= pattern.swap_delay)
                    {
                        if (image_swappers[0].activeSelf)
                        {
                            image_swappers[0].SetActive(false);
                            image_swappers[1].SetActive(true);
                            _scndtImg.sprite = pattern.loading_images[Random_Index()];
                        }
                        else
                        {
                            image_swappers[1].SetActive(false);
                            image_swappers[0].SetActive(true);
                            _firstImg.sprite = pattern.loading_images[Random_Index()];
                        }
                        _currentSwapDelay = 0;
                    }
                    _currentSwapDelay += Time.deltaTime;                  
                }

                yield return null;
            }

            yield return null;
        }

        private IEnumerator ChangeText_IE()
        {
            tip_text.color = pattern.text_color;
            float _currentDelay = pattern.text_swap_delay;

            while (true)
            {
                if(_currentDelay >= pattern.text_swap_delay)
                {
                    tip_text.text = $"{pattern.screen_tips[Random_Text_Index()]}";
                    _currentDelay = 0f;
                }
                _currentDelay += Time.deltaTime;
                
                yield return null;
            }
        }

        /// <summary>
        /// Return a random index from the pattern list
        /// </summary>
        /// <returns></returns>
        private int Random_Index()
        {
            return Random.Range(0, pattern.loading_images.Length);
        }

        private int Random_Text_Index()
        {
            return Random.Range(0, pattern.screen_tips.Length);
        }
    }
}
