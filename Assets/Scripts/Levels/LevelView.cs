using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Levels
{
    public class LevelView : MonoBehaviour, IRenderedDynamic
    {
        [SerializeField] private new Text name;
        [SerializeField] private Image image;
        [SerializeField] private Button goToLevelButton;

        public UnityAction<int, string> GoToLevelButtonClick { get; set; }

        private IRenderLevel _level;

        void TryLock()
        {
            if (!PlayerData.Instance.UnlockedLevels.Contains(_level.ID))
                goToLevelButton.interactable = false;
        }

        private void OnEnable()
        {
            goToLevelButton.onClick.AddListener(OnGoToLevelButtonClick);
        }

        public void Render(IRenderView template)
        {
            var level = (IRenderLevel) template;
            _level = level;
            
            name.text = level.Name;
            image.sprite = level.Icon;
            TryLock();
        }

        void OnGoToLevelButtonClick()
        {
            GoToLevelButtonClick?.Invoke(_level.ID, _level.SceneName);
        }
    }
}
