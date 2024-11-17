using UnityEngine;

public class MapInput : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnDistance = 1f; // 생성 거리
    public void OnCreate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.collider.bounds.center + hit.normal * spawnDistance;

            GameObject Ins = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            Ins.transform.SetParent(MapManager.Instance.MapObject.transform);
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