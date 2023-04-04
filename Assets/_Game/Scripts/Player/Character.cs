using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public MaterialColor MaterialColor;
    public MeshRenderer MeshRenderer;
    public List<GameObject> brickContains, brickInUse;
    public Transform brickContainer, backBrickHolder;
    public GameObject playerBrick;
    public int currentStage;
    public Vector3 offset;
    public bool canMoveUp /*{ get; set; } */= true;
    public virtual void Start()
    {
        MeshRenderer.material = ColorTypeManager.Instance.MaterialChange.GetColor(MaterialColor);
        OnInit();
    }
    public virtual void Update()
    {

    }
    public virtual void OnTriggerEnter(Collider other)
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
    }

    public virtual void AddBrick()
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

    public virtual void RemoveBrick()
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

    public virtual void ClearBrick()
    {
        for (int i = 0; i < brickInUse.Count; i++)
        {
            GameObject brick = brickInUse[i];
            brick.SetActive(false);
            brickContains.Add(brick);
        }
        brickInUse.Clear();
    }

    public virtual void OnInit()
    {
        for (int i = 0; i < max; i++)
        {
            GameObject pBrick = Instantiate(playerBrick, brickContainer);
            pBrick.GetComponent<BrickBehaviour>().MaterialColor = MaterialColor;
            brickContains.Add(pBrick);
            pBrick.SetActive(false);
        }
    }
    public int max = 50;

}
