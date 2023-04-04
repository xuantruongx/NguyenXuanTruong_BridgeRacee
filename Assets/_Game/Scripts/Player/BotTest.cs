using UnityEngine;

public class BotTest : Character
{
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void AddBrick()
    {
        base.AddBrick();
    }
    public override void RemoveBrick()
    {
        base.RemoveBrick();
    }
    public override void OnInit()
    {
        max = 80;
        base.OnInit();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
