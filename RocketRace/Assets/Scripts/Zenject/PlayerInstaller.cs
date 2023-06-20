namespace Zenject
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<InputMoveSignal>();
            Container.DeclareSignal<InputFingerSignal>();
            Container.DeclareSignal<UpdateObstacleSignal>();
            Container.DeclareSignal<MovePlayerSignal>();
        }
    }
}