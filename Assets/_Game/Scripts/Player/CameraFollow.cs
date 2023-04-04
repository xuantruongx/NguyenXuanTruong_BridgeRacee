using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        transform.position = target.transform.position;
    }
}
