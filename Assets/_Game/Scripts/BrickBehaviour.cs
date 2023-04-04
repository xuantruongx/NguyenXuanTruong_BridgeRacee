using System.Collections;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{

    public MaterialColor MaterialColor;
    public MeshRenderer MeshRenderer;
    void Start()
    {
        MeshRenderer.material = ColorTypeManager.Instance.MaterialChange.GetColor(MaterialColor);
    }

    private void SpawnNewBrick()
    {
        int playerColor = LevelManager.instance.botColors[Random.Range(0, LevelManager.instance.botColors.Count)];
        MaterialColor = (MaterialColor)playerColor;
        MeshRenderer.material = ColorTypeManager.Instance.MaterialChange.GetColor(MaterialColor);
        MeshRenderer.enabled = true;
    }

    public void BrickTaken()
    {
        MeshRenderer.enabled = false;
        StartCoroutine(SpawnBrick());
    }
    IEnumerator SpawnBrick()
    {
        yield return new WaitForSeconds(2);
        SpawnNewBrick();
    }
}
