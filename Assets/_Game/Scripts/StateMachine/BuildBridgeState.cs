using UnityEngine;
using UnityEngine.AI;

public class BuildBridgeState : BotBaseState
{
    public override void EnterState(StateManager bot)
    {
        int currentStage = bot.bot.currentStage;
        switch (currentStage)
        {
            case 0:
                bot.BridgeDestination = LevelManager.instance.doorStage0[Random.Range(0, LevelManager.instance.doorStage1.Count)];
                break;
            case 1:
                bot.BridgeDestination = LevelManager.instance.doorStage1[Random.Range(0, LevelManager.instance.doorStage1.Count)];

                break;
            case 2:
                bot.BridgeDestination = LevelManager.instance.doorStage2[Random.Range(0, LevelManager.instance.doorStage1.Count)];

                break;
            case 3:
                bot.BridgeDestination = LevelManager.instance.doorStage3[Random.Range(0, LevelManager.instance.doorStage1.Count)];
                break;
        }
        bot.botAgent.SetDestination(bot.BridgeDestination.position);
    }
    public override void UpdateState(StateManager bot)
    {
        if (bot.bot.brickInUse.Count == 0)
        {
            bot.botAgent.speed = 0;
            bot.SwitchState(bot.BotIdleState);
        }
        float dist = bot.botAgent.remainingDistance;
        if (dist != Mathf.Infinity && bot.botAgent.pathStatus == NavMeshPathStatus.PathComplete && bot.botAgent.remainingDistance == 0)
        {
            bot.SwitchState(bot.BotIdleState);
        }
    }

}
