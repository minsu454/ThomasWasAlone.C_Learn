using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public Vector2 mapSize;
    public int Count { get { return startList.Count; } }

    public List<SpawnData> startList = new List<SpawnData>();
    public List<SpawnData> endList = new List<SpawnData>();
    public Dictionary<CubeType, CubeItem> itemDic = new Dictionary<CubeType, CubeItem>();
}
