using Common.SceneEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    public static UIManager UI { get { return instance.uiManager; } }
    public static SoundManager Sound { get { return instance.soundManager; } }
    public static DataManager Data { get { return instance.dataManager; } }
    public static CubeContainer Cube { get { return instance.cubeContainer; } }
    public static MapContainer Map { get { return instance.mapContainer; } }

    private UIManager uiManager;
    private SoundManager soundManager;
    private DataManager dataManager = new DataManager();
    private CubeContainer cubeContainer = new CubeContainer();
    private MapContainer mapContainer = new MapContainer();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject go = new GameObject("Managers");
        instance = go.AddComponent<Managers>();

        DontDestroyOnLoad(go);

        instance.dataManager.Init();
        instance.cubeContainer.Init();
        instance.mapContainer.Init();

        instance.uiManager = CreateManager<UIManager>(go.transform);
        instance.soundManager = CreateManager<SoundManager>(go.transform);
    }

    private static T CreateManager<T>(Transform parent) where T : Component, IInit
    {
        GameObject go = new GameObject(typeof(T).Name);
        T t = go.AddComponent<T>();
        go.transform.parent = parent;

        t.Init();

        return t;
    }
}