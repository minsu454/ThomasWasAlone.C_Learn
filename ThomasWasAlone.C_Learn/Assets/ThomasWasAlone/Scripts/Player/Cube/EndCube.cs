using Common.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EndCube : MonoBehaviour
{
    [SerializeField] private CubeType cubeType;

    [SerializeField] private Material outCube;
    [SerializeField] private Material inCube;

    [SerializeField] private MeshRenderer myRenderer;

    private void Awake()
    {
        myRenderer.materials[0] = outCube;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BaseCube cube = other.GetComponent<BaseCube>();

            if (cubeType == cube.CubeType)
            {
                myRenderer.material = inCube;
                EventManager.Dispatch(GameEventType.InEndCube, 1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BaseCube cube = other.GetComponent<BaseCube>();

            if (cubeType == cube.CubeType)
            {
                myRenderer.material = outCube;
                EventManager.Dispatch(GameEventType.InEndCube, -1);
            }
        }
    }

    public void ResetMaterial()
    {
        myRenderer.material = outCube;
    }
}
