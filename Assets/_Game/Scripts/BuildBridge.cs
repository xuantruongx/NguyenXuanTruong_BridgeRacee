using System.Collections.Generic;
using UnityEngine;


public class BuildBridge : MonoBehaviour
{
    public GameObject brickPrefab;
    public Transform origin;
    public Vector3 offset = new Vector3(0, 0.2f, 0.5f);
    public List<GameObject> test;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject pBrick = Instantiate(brickPrefab, origin);
            //pBrick.GetComponent<BrickBehaviour>().MaterialColor = MaterialColor;
            test.Add(pBrick);
            pBrick.GetComponent<BrickBehaviour>().MeshRenderer.enabled = false;
            pBrick.transform.position = origin.transform.position + (test.Count - 1) * offset;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
