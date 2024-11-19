using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    [SerializeField] public GameObject interactionPlayer;
    [SerializeField] public GameObject interactionMapObj;
    private IMapBlockLogic logic;
    public MapObjType mapObjType;
    // Start is called before the first frame update
    void Start()
    {
        logic = interactionMapObj.GetComponent<IMapBlockLogic>();
    }
    public void StartMapObj()
    {
        StartCoroutine(logic.MapLogicCoroutine());
    }
    
}
