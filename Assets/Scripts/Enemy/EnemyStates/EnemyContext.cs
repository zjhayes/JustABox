using System.Collections.Generic;

public class EnemyContext : StateMachineContext<EnemyState, EnemyStates>
{
    public EnemyContext(EnemyController controller, IGameManager gameManager, EnemyStates defaultState = EnemyStates.PATROL)
        : base(new Dictionary<EnemyStates, EnemyState>
        {
            { EnemyStates.PATROL, new PatrolState(controller, gameManager) }
        }, defaultState)
    {
    }
}