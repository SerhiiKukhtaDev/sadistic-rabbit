using UnityEngine;

namespace Interfaces
{
    public interface IItemRenderer
    {
        T Render<T, T1>(T rendered, T1 viewRenderer, Transform container)
            where T : Object, IRenderedDynamic where T1 : IRenderView;
    }
}
