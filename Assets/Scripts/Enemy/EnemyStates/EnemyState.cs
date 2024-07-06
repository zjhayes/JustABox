using HierarchicalStateMachine;

public class EnemyState : BaseState<EnemyState>
{
    protected EnemyController controller;
    protected IGameManager gameManager;

    protected EnemyState(EnemyController controller, IGameManager gameManager): base()
    {
        this.controller = controller;
        this.gameManager = gameManager;
    }

    protected override void InitializeSubState(){}
}
