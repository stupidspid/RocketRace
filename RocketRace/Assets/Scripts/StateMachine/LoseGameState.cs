using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class LoseGameState : AbstractGameState
    {
        public LoseGameState(GameStateMachine gameStateMachine, SignalBus signalBus) : base(gameStateMachine, signalBus)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void OnChangeStateRequest(RequestChangeStateSignal signal)
        {
            if (signal.newStateType == StateType.LoadLevel)
            {
                gameStateMachine.ChangeState(signal.newStateType);
            }
        }
    }
}