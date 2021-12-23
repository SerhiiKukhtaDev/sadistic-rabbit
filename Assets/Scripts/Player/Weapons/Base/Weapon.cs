using Interfaces;
using UnityEngine;

namespace Player.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour, IRenderView
    {
        [SerializeField] private new string name;
        [SerializeField] private int price;
        [SerializeField] private bool isSingle;
        [SerializeField] protected MouseAngleChecker angleChecker;
        [SerializeField] private Sprite sprite;
        [SerializeField] protected PooledObject bullet;
        [SerializeField] protected int minBulletCapacity;
        [SerializeField] protected int maxBulletCapacity;
        [SerializeField] protected bool autoExpand;
        [SerializeField] private int id;
        
        protected Pool Pool;

        public int ID => id;

        public bool IsBought => _isBought;

        public string Name => name;

        public bool IsSingle => isSingle;

        public int Price => price;

        public Sprite Sprite => sprite;

        private Camera _camera;
        private bool _isBought = false;

        protected void Start()
        {
            angleChecker.Init(_camera);
        }

        public abstract void Shoot();

        public void Buy()
        {
            _isBought = true;
        }

        public void Init(Camera camera, Transform poolContainer)
        {
            _camera = camera;
            Pool = PoolCreator.Create($"{Name}Pool", bullet, poolContainer, minBulletCapacity, maxBulletCapacity,
                autoExpand);
        }
    }
}
