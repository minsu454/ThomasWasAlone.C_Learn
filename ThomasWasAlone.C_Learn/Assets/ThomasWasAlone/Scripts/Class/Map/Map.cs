using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] public bool[] Clear;
    [SerializeField] public List<GameObject> startObj = new List<GameObject>();
    [SerializeField] public List<GameObject> endObj = new List<GameObject>();
    [SerializeField] public List<GameObject> playerObj = new List<GameObject>();

    public int index = 0;
    public void ClearCheck()
    {
        int check = 0;
        for (int i = 0; i < Clear.Length; i++) 
        {
            if(Clear[i] == true)
            {
                check++;
            }
        }
        if(Clear.Length == check)
        {
            Debug.Log("클리어");
        }
    }
}
