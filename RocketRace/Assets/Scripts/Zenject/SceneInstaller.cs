using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<RequestChangeStateSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
    }
}