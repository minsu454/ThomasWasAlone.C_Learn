using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public MapData mapData;
    public HashSet<GameObject> groundObjs = new HashSet<GameObject>();
}
