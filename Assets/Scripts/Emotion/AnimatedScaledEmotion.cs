using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Emotion
{
    public class AnimatedScaledEmotion : AnimatedEmotion
    {
        [SerializeField] private float startDelay;
        [SerializeField] private float animationTime;
        [SerializeField] private bool hideOnEnd;
        [SerializeField] private float hideDelay;
        
        private Coroutine _coroutine;
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.fillAmount = 0;
            
            gameObject.SetActive(false);
        }

        public override void Animate()
        {
            StartCoroutine(Action.DelayedAction(startDelay, DoScale));
        }

        public override void Exit()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            
            _image.fillAmount = 0;
            gameObject.SetActive(false);
        }

        private void DoScale()
        {
            _coroutine = StartCoroutine(ScaleCoroutine(0, 1, animationTime, ScaleEnd));
        }

        private void ScaleEnd(float endValue)
        {
            GetComponent<Image>().fillAmount = endValue;
            
            if (hideOnEnd)
            {
                StartCoroutine(Action.DelayedAction(hideDelay, () => gameObject.SetActive(false)));
            }
        }
    
        IEnumerator ScaleCoroutine(float startValue, float endValue, float time, UnityAction<float> scaleEnd = null)
        {
            float elapsedTime = 0;

            while (elapsedTime <= time)
            {
                float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / time );
                _image.fillAmount = newValue;
                
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            scaleEnd?.Invoke(endValue);
        }
    }
}
