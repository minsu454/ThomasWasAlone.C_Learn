using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockInitPopup : BasePopupUI
{
    [SerializeField] private TMP_InputField inputFieldx;
    [SerializeField] private TMP_InputField inputFieldy;
    [SerializeField] private GameObject block;
    private GameObject MapObject;
    public void MapCreateButton()
    {
        int x = int.Parse(inputFieldx.text);
        int y = int.Parse(inputFieldy.text);
        MapManager.Instance.map.mapSize = new Vector2(x, y);
        MapObject = MapManager.Instance.MapObject;
        
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject Ins = Instantiate(block, new Vector3(i, 0, j), Quaternion.identity);
                Ins.transform.SetParent(MapObject.transform);
                MapManager.Instance.map.groundObjs.Add(Ins);


            }
        }
        Close();
    }
}