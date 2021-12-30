using Interfaces;
using UnityEngine;

namespace Render
{
    public class ItemRenderer : MonoBehaviour, IItemRenderer
    {
        public T Render<T, T1>(T rendered, T1 viewRenderer, Transform container) 
            where T : Object, IRenderedDynamic where T1 : IRenderView
        {
            var itemView = Instantiate(rendered, container);
            itemView.Render(viewRenderer);

            return itemView;
        }
    }
}
