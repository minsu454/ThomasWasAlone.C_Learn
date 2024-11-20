using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCube : MonoBehaviour
{
    [SerializeField] private CubeType cubeType;

    [SerializeField] private Material outCube;
    [SerializeField] private Material inCube;

    [SerializeField] private MeshRenderer myRenderer;

    public bool isInCube;

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
                myRenderer.materials[0] = inCube;
                isInCube = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isInCube)
            {
                BaseCube cube = other.GetComponent<BaseCube>();

                if (cubeType == cube.CubeType)
                    myRenderer.materials[0] = outCube;
            }
        }
    }
}
