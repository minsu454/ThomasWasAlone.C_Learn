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

        if (!int.TryParse(inputFieldx.text, out int x) || !int.TryParse(inputFieldy.text, out int y)) return;
        
        if (MapObject == null)
        {
            MapObject = new GameObject("Map");
            MapManager.Instance.MapObject = MapObject;
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject Ins = Instantiate(block, new Vector3(i, j, 0), Quaternion.identity);
                Ins.transform.SetParent(MapObject.transform);
            }
        }
        Close();
    }
}