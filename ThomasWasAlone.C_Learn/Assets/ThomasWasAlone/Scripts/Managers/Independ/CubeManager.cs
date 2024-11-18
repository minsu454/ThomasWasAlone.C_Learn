using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public BaseCube[] cubes;
    private int currentCubeIndex = 0;
    public Camera mainCamera;
    
    private PlayerMovement currentMovement;

    private void Start()
    {
        foreach (var cube in cubes)
        {
            if (cube.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
            {
                movement.enabled = false;
            }
        }
        
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
        if (currentMovement != null)
        {
            currentMovement.enabled = false;
        }

        if (cubes[index].TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            currentMovement = movement;
            currentMovement.enabled = true;
        }
        
        CameraController.Instance.SetTarget(cubes[index].transform);
    }
} 