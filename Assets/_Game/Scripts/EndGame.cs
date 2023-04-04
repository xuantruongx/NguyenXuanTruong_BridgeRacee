using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            door.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //EndGame
            Debug.Log("END GAME");
            door.SetActive(true);
            other.GetComponent<Character>().ClearBrick();
        }
    }
}
