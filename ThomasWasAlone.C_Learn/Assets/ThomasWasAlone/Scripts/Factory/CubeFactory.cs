using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeFactory
{
    public static Color TypeByColor(CubeType type)
    {
        Color color = Color.black;

        switch (type)
        {
            case CubeType.BigCube:
                color = new Color32(101, 100, 255, 255);
                break;
            case CubeType.SmallCube:
                color = Color.white;
                break;
            case CubeType.JumpBoostCube:
                color = new Color32(255, 100, 100, 255);
                break;
            case CubeType.LightCube:
                color = new Color32(255, 237, 100, 255);
                break;
        }

        return color;
    }
}
