using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelsChooser : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Transform container;
        [SerializeField] private LevelView template;

        private IItemRenderer _renderer;
        private List<LevelView> _levelsViews;

        private void Awake()
        {
            _levelsViews = new List<LevelView>();
            _renderer = GetComponent<IItemRenderer>();

            levels.ForEach(AddToLevelsList);
        }

        private void OnEnable()
        {
            _levelsViews.ForEach(view => view.GoToLevelButtonClick += GoToLevel);
        }
        
        private void OnDisable()
        {
            _levelsViews.ForEach(view => view.GoToLevelButtonClick -= GoToLevel);
        }

        void AddToLevelsList(Level level)
        {
            var levelView = _renderer.Render(template, level, container);

            _levelsViews.Add(levelView);
        }

        void GoToLevel(int levelId, string sceneName)
        {
            var playerData = PlayerData.Instance;

            if (playerData.UnlockedLevels.Contains(levelId))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
