using UnityEngine;

public class ColorTypeManager : MonoBehaviour
{
    public static ColorTypeManager Instance;
    public MaterialChange MaterialChange;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
