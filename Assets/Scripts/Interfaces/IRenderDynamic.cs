namespace Interfaces
{
    public interface IRenderedDynamic<in T> where T : IRenderView
    {
        void Render(T from);
    }
}
