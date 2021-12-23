using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelsChooser : MonoBehaviour
    {
        [SerializeField] private Level startLevel;
        [SerializeField] private List<Level> levels;
        [SerializeField] private Transform container;
        [SerializeField] private LevelView template;
        
        private List<LevelView> _levelsViews;

        private void Awake()
        {
            _levelsViews = new List<LevelView>();

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
            LevelView levelView = Instantiate(template, container);
            levelView.Render(level);
            
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
