using UnityEngine;

public class GameUnit : MonoBehaviour
{
    //public Transform Transform => m_transform ??= transform;

    public Vector3 WorldPosition { get => transform.position; set => transform.position = value; }

    RectTransform rectTransform;
    public Vector3 SizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }


    public Vector3 GetSizeDelta() => rectTransform.sizeDelta;

    public void SetSizeDelta(Vector3 delta) => rectTransform.sizeDelta = delta;
}


public class A
{
    //GameUnit unit;
    //public void AB()
    //{
    //    unit.SizeDelta = new(1, 1);
    //    unit.SetSizeDelta(new(1, 1));
    //}
}