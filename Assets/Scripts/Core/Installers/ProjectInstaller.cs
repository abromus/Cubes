namespace Cubes.Core.Installers
{
    internal sealed class ProjectInstaller : Zenject.MonoInstaller
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void InstallBindings()
        {
            Container.Bind<Services.UpdaterService>().AsSingle();
        }
    }
}
