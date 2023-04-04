using UnityEngine;
using UnityEngine.AI;

public class FindBrickState : BotBaseState
{
    public override void EnterState(StateManager bot)
    {
        bot.RandomPos = Random.Range(0, LevelManager.instance.botBricks[bot.BrickListIndex].Count);
        FindBrick(bot);
    }
    public override void UpdateState(StateManager bot)
    {
        float dist = bot.botAgent.remainingDistance;
        if (dist != Mathf.Infinity && bot.botAgent.pathStatus == NavMeshPathStatus.PathComplete && bot.botAgent.remainingDistance == 0)
        {
            bot.SwitchState(bot.BotIdleState);
        }
    }
    public void FindBrick(StateManager bot)
    {
        bot.botAgent.SetDestination(LevelManager.instance.botBricks[bot.BrickListIndex][bot.RandomPos]);
    }

}
