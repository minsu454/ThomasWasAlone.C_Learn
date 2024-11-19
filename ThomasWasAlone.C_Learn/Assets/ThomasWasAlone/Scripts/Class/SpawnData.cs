using System;
using UnityEngine;

[Serializable]
public class SpawnData
{
    public CubeType Type;
    public Vector3 Pos;

    public SpawnData(CubeType type, Vector3 pos)
    {
        Type = type;
        Pos = pos;
    }
}