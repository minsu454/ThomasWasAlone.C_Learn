using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();

    private void Awake()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        //GameObject prefab = Resources.Load<GameObject>($"Prefabs/Map/SaveMap/SaveMap/{Managers.Data.MapName}");
        //GameObject mapGo = Instantiate(prefab);

        //Map map = mapGo.GetComponent<Map>();

        //foreach (var obj in map.startVecs)
        //{
        //    for (int i = 0; i < playerObj.Count; i++)
        //    {
        //        if (playerObj[i].name == obj.name)
        //        {
        //            Instantiate(obj);
        //            break;
        //        }
        //    }

        //    obj.GetComponent<MeshCollider>().enabled = false;
        //}
    }
}
