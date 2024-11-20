using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public Vector2 mapSize;
    public int Count { get { return startDic.Count; } }

    public List<SpawnData> startDic = new List<SpawnData>();
    public List<SpawnData> endDic = new List<SpawnData>();
    public Dictionary<CubeType, CubeItem> itemDic = new Dictionary<CubeType, CubeItem>();
}
