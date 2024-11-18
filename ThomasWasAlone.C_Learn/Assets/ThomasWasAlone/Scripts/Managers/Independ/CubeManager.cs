using Common.Yield;
using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public BaseCube[] cubes;
    private int currentCubeIndex = 0;
    public Camera mainCamera;

    private void Start()
    {
        SwitchToCube(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentCubeIndex = (currentCubeIndex + 1) % cubes.Length;
            SwitchToCube(currentCubeIndex);
        }
    }

    private void SwitchToCube(int index)
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].enabled = (i == index);
        }
        
        CameraController.Instance.SetTarget(cubes[index].transform);
    }
} 