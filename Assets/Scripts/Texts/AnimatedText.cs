using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Texts
{
    [RequireComponent(typeof(Text))]
    public class AnimatedText : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private float animationTime;

        [SerializeField] private string targetText;
        
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        void Start()
        {
            _text.DOText(targetText, animationTime, false, ScrambleMode.All)
                .SetEase(Ease.Flash)
                .SetDelay(delay);
        }
        
    }
}
