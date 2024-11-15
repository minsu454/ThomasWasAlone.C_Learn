using System;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public event Action<GameObject> ReleaseEvent;

    public void OnDestroy()
    {
        ReleaseEvent?.Invoke(gameObject);
    }
}