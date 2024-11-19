using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField] public readonly List<Vector3> startVecs = new List<Vector3>();
    [SerializeField] public readonly List<Vector3> endVecs = new List<Vector3>();
    public readonly HashSet<GameObject> groundObjs = new HashSet<GameObject>();


}
