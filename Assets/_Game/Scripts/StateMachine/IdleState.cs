public class IdleState : BotBaseState
{
    public override void EnterState(StateManager bot)
    {
        bot.botAgent.speed = 10f;
    }
    public override void UpdateState(StateManager bot)
    {
        if (bot.bot.brickInUse.Count < bot.BrickThreshhold)
            bot.SwitchState(bot.BotFindBrickState);
        else
            bot.SwitchState(bot.BotBuildBridgeState);
    }

}
