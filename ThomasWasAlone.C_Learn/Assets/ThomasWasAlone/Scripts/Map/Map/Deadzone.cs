using Common.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            EventManager.Dispatch(GameEventType.KillAllCubes, null);
        }
    }
}
