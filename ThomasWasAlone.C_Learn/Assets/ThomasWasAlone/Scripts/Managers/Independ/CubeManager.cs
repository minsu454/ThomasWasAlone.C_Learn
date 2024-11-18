using Common.Yield;
using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public BaseCube[] cubes;
    private int currentCubeIndex = 0;
    public Camera mainCamera;

    public AudioClip clip;

    private void Start()
    {
        SwitchToCube(0);
        StartCoroutine(CoLoop());
    }

    
    private IEnumerator CoLoop()
    {
        while (true)
        {
            yield return YieldCache.WaitForSeconds(1);
            Managers.Sound.SFX3DPlay(clip);
        }
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
        
        //CameraController.Instance.SetTarget(cubes[index].transform);
    }
} 