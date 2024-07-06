
public class EnemyController : GameBehaviour, IController
{
    private EnemyContext context;

    private void Awake()
    {
        context = new EnemyContext(this, gameManager);
    }

    private void Update()
    {
        context.CurrentState.Update();
    }

    public EnemyContext Context
    {
        get { return context; }
    }

}