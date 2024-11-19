using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class MapMeshCombiner : MonoBehaviour
{
    //private const int VerticesPerChunk = 65000;
    //private Park save;
    //private void Start()
    //{
    //    save = GetComponent<Park>();
    //}
    //public void CombineMeshes(List<GameObject> objects, Material material)
    //{
    //    var chunks = new List<List<GameObject>>();
    //    var currentChunk = new List<GameObject>();
    //    int vertexCount = 0;

    //    foreach (var obj in objects)
    //    {
    //        if (obj == null) continue;
    //        var mesh = obj.GetComponent<MeshFilter>().sharedMesh;
    //        if (vertexCount + mesh.vertexCount > VerticesPerChunk)
    //        {
    //            chunks.Add(currentChunk);
    //            currentChunk = new List<GameObject>();
    //            vertexCount = 0;
    //        }

    //        currentChunk.Add(obj);
    //        vertexCount += mesh.vertexCount;
    //    }

    //    if (currentChunk.Count > 0)
    //        chunks.Add(currentChunk);

    //    foreach (var chunk in chunks)
    //    {
    //        CombineChunk(chunk, material);
    //    }
    //}

    //private void CombineChunk(List<GameObject> objects, Material material)
    //{
    //    var combines = new CombineInstance[objects.Count];

    //    for (int i = 0; i < objects.Count; i++)
    //    {
    //        var obj = objects[i];
    //        combines[i].mesh = obj.GetComponent<MeshFilter>().sharedMesh;
    //        combines[i].transform = obj.transform.localToWorldMatrix;
    //    }

    //    var go = new GameObject("CombinedMesh");
    //    go.transform.SetParent(MapManager.Instance.MapObject.transform);

    //    var mesh = new Mesh();
    //    mesh.CombineMeshes(combines);

    //    var mf = go.AddComponent<MeshFilter>();
    //    mf.sharedMesh = mesh;

    //    var mr = go.AddComponent<MeshRenderer>();
    //    mr.sharedMaterial = material;
    //    // MeshCollider를 추가하여 결합된 메시의 콜라이더를 설정
    //    var meshCollider = go.AddComponent<MeshCollider>();
    //    meshCollider.sharedMesh = mesh;
    //    meshCollider.convex = true;
    //    // 메시를 에셋으로 저장

    //    foreach (var obj in objects)
    //    {
    //        Destroy(obj);
    //    }
    //    save.SaveMeshToAsset(mesh);
    //    Debug.Log("Mesh 저장 완료");
    //}

}
