using RayTracerChallenge.Core;

namespace RayTracerChallenge.ConsoleApplication.Scenes
{
    internal interface IScene
    {
        string Name { get; }
        void Render(ICanvasRenderer canvasRenderer);
    }
}
