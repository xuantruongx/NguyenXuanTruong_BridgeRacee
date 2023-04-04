using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MaterialColor MaterialColor;
    public MeshRenderer MeshRenderer;
    [SerializeField] private List<GameObject> brickContains, brickInUse;
    [SerializeField] private Transform brickContainer, backBrickHolder;
    [SerializeField] private GameObject playerBrick;
    [SerializeField] private Vector3 offset;
    public bool canMoveUp /*{ get; set; } */= true;

    public PlayerTouchMovement PlayerTouchMovement;
    void Start()
    {
        MeshRenderer.material = ColorTypeManager.Instance.MaterialChange.GetColor(MaterialColor);
        OnInit();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            AddBrick();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            RemoveBrick();
        }
        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up + PlayerTouchMovement.MoveDirection.normalized, Vector3.down * 10, Color.red);

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
    //[SerializeField] private Transform raycastPoint;
    [SerializeField] private LayerMask bridgeLayer, groundLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BRICK_GROUND))
        {
            if (other.GetComponent<BrickBehaviour>().MaterialColor != MaterialColor)
            {
                return;
            }
            else
            {
                other.GetComponent<BrickBehaviour>().BrickTaken();
                AddBrick();
            }
        }

        if (other.CompareTag(Constant.TAGE_BRICK_BRIDGE) && brickInUse.Count > 0)
        {
            if (!other.GetComponent<BrickBridge>().MeshRenderer.enabled)
            {
                other.GetComponent<BrickBridge>().ChangeColor(MaterialColor);
                RemoveBrick();
            }
            else
            {
                if (other.GetComponent<BrickBridge>().MaterialColor != MaterialColor)
                {
                    other.GetComponent<BrickBridge>().ChangeColor(MaterialColor);
                    RemoveBrick();
                }
            }
        }

        //{
        //    if (brickInUse.Count > 0)
        //        canMoveUp = true;
        //    else canMoveUp = false;
        //}
    }

    private void AddBrick()
    {
        if (brickInUse.Count <= 0)
        {
            GameObject brick = brickContains[brickContains.Count - 1];
            brick.SetActive(true);
            brick.transform.position = backBrickHolder.transform.position;
            brickContains.RemoveAt(brickContains.Count - 1);
            brickInUse.Add(brick);
        }
        else
        {
            GameObject brick = brickContains[brickContains.Count - 1];
            brick.SetActive(true);
            foreach (GameObject t in brickInUse)
            {
                t.transform.position += offset;
            }
            brick.transform.position = backBrickHolder.transform.position;
            brickContains.RemoveAt(brickContains.Count - 1);
            brickInUse.Add(brick);
        }
    }

    private void RemoveBrick()
    {
        if (brickInUse.Count > 0)
        {
            GameObject brick = brickInUse[brickInUse.Count - 1];
            brick.SetActive(false);
            brickInUse.RemoveAt(brickInUse.Count - 1);
            foreach (GameObject t in brickInUse)
            {
                t.transform.position -= offset;
            }
            brickContains.Add(brick);
        }
    }

    private void OnInit()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject pBrick = Instantiate(playerBrick, brickContainer);
            pBrick.GetComponent<BrickBehaviour>().MaterialColor = MaterialColor;
            brickContains.Add(pBrick);
            pBrick.SetActive(false);
        }
    }

}
