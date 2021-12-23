using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private new string name;
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite icon;
        
        public string Name => name;

        public string SceneName => sceneName;

        public Sprite Icon => icon;

        public int ID => id;
    }
}
