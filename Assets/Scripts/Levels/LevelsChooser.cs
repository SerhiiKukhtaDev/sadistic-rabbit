using System.Collections.Generic;
using Interfaces;
using Render;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelsChooser : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Transform container;
        [SerializeField] private LevelView template;

        private void Awake()
        {
            levels.ForEach(AddToLevels);
        }

        private void OnEnable()
        {
            LevelView.GoToLevelButtonClick += GoToLevel;
        }
        
        private void OnDisable()
        {
            LevelView.GoToLevelButtonClick -= GoToLevel;
        }

        void AddToLevels(Level level)
        {
            ItemRenderer.Render(template, level, container);
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
