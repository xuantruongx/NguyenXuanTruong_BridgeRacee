using UnityEngine;

public class LastStair : MonoBehaviour
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
            Character character = other.GetComponent<Character>();
            character.currentStage++;
            if (other.GetComponent<BotTest>() != null)
                LevelManager.instance.NewStageBrickList(character);
            LevelManager.instance.SpawnBrick(character.currentStage);
            character.ClearBrick();
            door.SetActive(true);
        }
    }
}
