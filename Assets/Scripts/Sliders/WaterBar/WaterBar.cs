using UnityEngine;

namespace Sliders.WaterBar
{
    public class WaterBar : MonoBehaviour
    {
        private void Start()
        {
            ShowOrHide(false);
        }

        public void ShowOrHide(bool isEntered)
        {
            gameObject.SetActive(isEntered);
        }
    }
}
