using UnityEngine;

public class BrickBridge : MonoBehaviour
{
    public MaterialColor MaterialColor;
    public MeshRenderer MeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer.enabled = false;
    }

    public void ChangeColor(MaterialColor color)
    {
        if (!MeshRenderer.enabled)
        {
            MeshRenderer.enabled = true;
        }

        MeshRenderer.material = ColorTypeManager.Instance.MaterialChange.GetColor(color);
        MaterialColor = color;
    }
}
