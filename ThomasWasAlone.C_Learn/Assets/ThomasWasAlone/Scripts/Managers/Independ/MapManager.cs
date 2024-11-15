using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public GameObject objectToSpawn;
    public GameObject MapObject;
    public float spawnDistance = 1f; // 생성 거리
    private void Awake()
    {
        Instance = this;
    }
    public void OnCreate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.collider.bounds.center + hit.normal * spawnDistance;

            GameObject Ins = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            Ins.transform.SetParent(MapObject.transform);
        }
    }
    public void OnDelete()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
