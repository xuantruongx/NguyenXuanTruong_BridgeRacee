using UnityEngine;
using UnityEngine.AI;
public class StateManager : MonoBehaviour
{
    BotBaseState currentState;
    public IdleState BotIdleState = new IdleState();
    public FindBrickState BotFindBrickState = new FindBrickState();
    public BuildBridgeState BotBuildBridgeState = new BuildBridgeState();
    public int BrickListIndex;
    public BotTest bot;
    public NavMeshAgent botAgent;
    public int BrickThreshhold;
    public int RandomPos;
    public Transform BridgeDestination;

    private void Start()
    {
        BrickThreshhold = Random.Range(8, 15);
        BrickListIndex = LevelManager.instance.GetBotBricksListIndex(bot);
        Debug.Log(BrickListIndex);
        currentState = BotIdleState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(BotBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
