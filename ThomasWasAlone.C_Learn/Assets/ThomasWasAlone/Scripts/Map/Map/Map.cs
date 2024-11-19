using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public Dictionary<CubeType, Vector3> startVecs = new Dictionary<CubeType, Vector3>();
    public Dictionary<CubeType, Vector3> endVecs = new Dictionary<CubeType, Vector3>();
    public Vector2 mapSize;
    public readonly HashSet<GameObject> groundObjs = new HashSet<GameObject>();
}
