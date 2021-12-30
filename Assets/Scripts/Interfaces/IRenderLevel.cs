using UnityEngine;

namespace Interfaces
{
    public interface IRenderLevel : IRenderView
    {
        string Name { get; }

        string SceneName { get; }

        Sprite Icon { get; }

        int ID { get; }
    }
}
