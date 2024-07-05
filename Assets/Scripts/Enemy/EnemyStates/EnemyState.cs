using HierarchicalStateMachine;

public class EnemyState : BaseState
{
    protected EnemyController controller;
    protected IGameManager gameManager;

    protected EnemyState(EnemyController _controller, IGameManager _gameManager): base(_controller.Context as IStateMachine)
    {
        controller = _controller;
        gameManager = _gameManager;
    }

    protected override void InitializeSubState(){}
}
