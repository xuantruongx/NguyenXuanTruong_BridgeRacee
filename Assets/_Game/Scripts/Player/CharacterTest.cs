using UnityEngine;

public class CharacterTest : Character
{
    public PlayerTouchMovement PlayerTouchMovement;
    public LayerMask bridgeLayer, groundLayer;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        RaycastHit hit;

        //hit bridge
        if (Physics.Raycast(transform.position + Vector3.up + PlayerTouchMovement.MoveDirection.normalized, Vector3.down, out hit, 100f, bridgeLayer))
        {
            //Debug.Log(hit.collider);
            BrickBridge brickBridge = hit.collider.GetComponent<BrickBridge>();
            if (brickBridge != null)
            {
                //Debug.Log(brickBridge.MaterialColor);

                //dif
                if (brickBridge.MaterialColor != MaterialColor || !brickBridge.MeshRenderer.enabled)
                {
                    if (brickInUse.Count > 0)
                        canMoveUp = true;
                    else
                        canMoveUp = false;
                }
                else if (brickBridge.MaterialColor == MaterialColor)
                {
                    canMoveUp = true;
                }
            }
        }
        else if (Physics.Raycast(transform.position + Vector3.up + PlayerTouchMovement.MoveDirection.normalized, Vector3.down, out hit, 100f, groundLayer))
        {
            canMoveUp = true;
        }
        else
        {
            canMoveUp = false;
        }
    }
    public override void AddBrick()
    {
        base.AddBrick();
    }
    public override void RemoveBrick()
    {
        base.RemoveBrick();
    }
    public override void ClearBrick()
    {
        base.ClearBrick();
    }
    public override void OnInit()
    {
        max = 100;
        base.OnInit();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
