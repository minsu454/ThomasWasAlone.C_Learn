using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] public bool[] Clear;
    private void Awake()
    {
       // MapManager.Instance.map = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
