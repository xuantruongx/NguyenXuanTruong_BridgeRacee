using UnityEngine;
[CreateAssetMenu(fileName = "Material Changer")]
public class MaterialChange : ScriptableObject
{
    public Material[] colors;
    public Material GetColor(MaterialColor material)
    {
        return colors[(int)material];
    }
}

public enum MaterialColor
{
    Blue = 0,
    Green = 1,
    Red = 2,
    Yellow = 3,
    Pink = 4,
    Purple = 5
}