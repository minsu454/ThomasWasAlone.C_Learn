using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public GameObject MapObject;
    public MapInput Input;
    private void Awake()
    {
        Instance = this;
    }
}
