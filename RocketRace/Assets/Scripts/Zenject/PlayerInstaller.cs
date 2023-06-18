namespace Zenject
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InputMoveSignal>();
            Container.DeclareSignal<InputFingerSignal>();
        }
    }
}