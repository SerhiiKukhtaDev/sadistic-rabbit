using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Emotion
{
    public class EmotionAnimator : MonoBehaviour
    {
        private List<AnimatedEmotion> _emotions;

        private void Start()
        {
            _emotions = new List<AnimatedEmotion>();
        }

        public void Animate(AnimatedEmotion emotion)
        {
            if (emotion.gameObject.activeSelf) return;
        
            if (!_emotions.Contains(emotion))
            {
                _emotions.Add(emotion);
            }
        
            foreach (var animatedEmotion in _emotions.Where(e => e.gameObject.activeSelf && !e.gameObject.Equals(emotion)))
            {
                animatedEmotion.Exit();
            }

            emotion.gameObject.SetActive(true);
            
            emotion.Animate();
        }
    }
}
