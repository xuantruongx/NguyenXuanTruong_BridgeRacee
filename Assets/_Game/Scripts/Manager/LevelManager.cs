using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private int playerCount;
    [SerializeField] private int width, height;
    public MaterialColor MaterialColor;
    public Character player;
    public List<int> botColors;
    public List<Vector3Int> brickSpawnPos;
    public Character bot;
    private int counter;
    public BrickBehaviour brick;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Transform[] stageSpawnPos;
    [SerializeField] private int currentStage = 0;
    public List<List<Vector3>> botBricks = new List<List<Vector3>>();
    [SerializeField] private List<Character> botList;
    public List<Transform> doorStage0;
    public List<Transform> doorStage1;
    public List<Transform> doorStage2;
    public List<Transform> doorStage3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnBot();
        SpawnBrick(0);
    }
    public void SpawnPlayer()
    {
        int playerColor = Random.Range(0, ColorTypeManager.Instance.MaterialChange.colors.Length);
        botColors.Add(playerColor);
        player.MaterialColor = (MaterialColor)playerColor;
        Character character = Instantiate(player, playerPos.position, Quaternion.identity);
        character.GetComponent<PlayerTouchMovement>().Joystick = joystick;
        character.GetComponent<PlayerTouchMovement>().OnInit();
    }

    [SerializeField] private Transform playerPos, botPos;
    public void SpawnBot()
    {
        while (counter < playerCount - 1)
        {
            int botColor = Random.Range(0, ColorTypeManager.Instance.MaterialChange.colors.Length);
            if (!botColors.Contains(botColor))
            {
                botColors.Add(botColor);
                Character enemy = Instantiate(bot, botPos.position, Quaternion.identity);
                enemy.MaterialColor = (MaterialColor)botColor;
                counter++;
                List<Vector3> sublist = new List<Vector3>();
                botBricks.Add(sublist);
                botList.Add(enemy);
            }
        }
    }

    public void SpawnBrick(int stage)
    {
        int offsetX = (int)stageSpawnPos[stage].position.x;
        int offsetZ = (int)stageSpawnPos[stage].position.z;
        int offsetY = (int)Mathf.Ceil(stageSpawnPos[stage].position.y / 2);

        for (int i = offsetX / 2; i < width + offsetX / 2; i++)
        {
            for (int j = offsetZ / 2; j < height + offsetZ / 2; j++)
            {
                Vector3Int position = new Vector3Int(i, offsetY, j) * 2;
                brickSpawnPos.Add(position);
            }
        }
        for (int i = 0; i < brickSpawnPos.Count; i++)
        {
            BrickBehaviour brickGround = Instantiate(brick, brickSpawnPos[i], Quaternion.identity);
            int randomColor = Random.Range(0, botColors.Count);
            int brickColor = botColors[randomColor];
            brickGround.MaterialColor = (MaterialColor)brickColor;

            switch (randomColor)
            {
                case 0:
                    break;
                case 1:
                    if (brickSpawnPos[i].y - stageSpawnPos[botList[randomColor - 1].currentStage].position.y <= 0.2)
                    {
                        botBricks[randomColor - 1].Add(brickSpawnPos[i]);
                    }
                    break;
                case 2:
                    if (brickSpawnPos[i].y - stageSpawnPos[botList[randomColor - 1].currentStage].position.y <= 0.2)
                        botBricks[randomColor - 1].Add(brickSpawnPos[i]);
                    break;
                case 3:
                    if (brickSpawnPos[i].y - stageSpawnPos[botList[randomColor - 1].currentStage].position.y <= 0.2)
                        botBricks[randomColor - 1].Add(brickSpawnPos[i]);
                    break;
            }
        }
    }
    public void NewStageBrickList(Character enemy)
    {
        for (int i = 0; i < botColors.Count; i++)
            if ((int)enemy.MaterialColor == botColors[i])
            {
                Debug.Log($"Color number {(int)enemy.MaterialColor} is {enemy.MaterialColor} and botColors index is {i}, which is {botColors[i]}");
                botBricks[i - 1].Clear();
                brickSpawnPos.Clear();
            }
    }

    public int GetBotBricksListIndex(Character enemy)
    {
        for (int i = 0; i < botColors.Count; i++)
            if ((int)enemy.MaterialColor == botColors[i])
            {
                return i - 1;
            }
        return -1;
    }
}
