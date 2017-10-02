using UnityEngine;
using System.Threading;
using UnityToolbag;

public class Engine : MonoBehaviour {

    #region Fields

    public MapGeneratorParameter MapWidth;
    public MapGeneratorParameter MapHeight;
    public MapGeneratorParameter MountainPercent;
    public MapGeneratorParameter RiverPercent;
    public MapGeneratorParameter StaticTemp;
    public MapGeneratorParameter DynamicTemp;
    public MapGeneratorParameter TempDifference;
    public MapGeneratorParameter BiomePercent;
    public MapGeneratorParameter CityPercent;
    public Seasons ActualSeason;
    public int ColdTemp = 40;
    public int HotTemp = 85;
    public int SeasonTempModifier = 20;

    public float CameraLimitX1 = 0;
    public float CameraLimitX2;
    public float CameraLimitY1 = 0;
    public float CameraLimitY2;

    #endregion

    #region SingletonPattern
    public static Engine _instance;
    public static Engine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Engine>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("Engine");
                    _instance = container.AddComponent<Engine>();
                    //_instance.GetComponent<Engine>().Initializer();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region UnityMethods

    private void Start()
    {
        //Initializer();
    }

    private void Awake()
    {
        MapWidth = new MapGeneratorParameter(10, 50, 20);
        MapHeight = new MapGeneratorParameter(10, 50, 20);
        MountainPercent = new MapGeneratorParameter(0, 30, 15);
        RiverPercent = new MapGeneratorParameter(0, 30, 15);
        StaticTemp = new MapGeneratorParameter(0, 100, 50);
        DynamicTemp = new MapGeneratorParameter(-200, 200, 0);
        TempDifference = new MapGeneratorParameter(-100, 100, 10);
        BiomePercent = new MapGeneratorParameter(0, 100, 50);
        CityPercent = new MapGeneratorParameter(0, 100, 50);
        ActualSeason = Seasons.Spring;
    }

    #endregion

    #region Methods

    private void Initializer()
    {
        MapWidth = new MapGeneratorParameter(10, 20, 20);
        MapHeight = new MapGeneratorParameter(10, 20, 20);
        MountainPercent = new MapGeneratorParameter(0, 30, 15);
        RiverPercent = new MapGeneratorParameter(0, 30, 15);
        StaticTemp = new MapGeneratorParameter(0, 100, 50);
        DynamicTemp = new MapGeneratorParameter(-200, 200, 0);
        TempDifference = new MapGeneratorParameter(-100, 100, 10);
        BiomePercent = new MapGeneratorParameter(0, 100, 50);
        CityPercent = new MapGeneratorParameter(0, 100, 50);
        ActualSeason = Seasons.Spring;
    }

    #endregion

    #region API

    public void CreateMap()
    {
        Thread myThread2 = new Thread(() =>
        {
            Dispatcher.Invoke(() =>
            {
                MapGenerator.Instance.CreateMap();
                LoadingScript.Instance.HideLoadingPanel();
            });
        });

        Thread myThread = new Thread(() =>
        {
            Dispatcher.Invoke(() =>
            {
                LoadingScript.Instance.ShowLoadingPanel();
                LoadingScript.Instance.DisableControls();
                myThread2.Start();
            });   
        });
        myThread.Start();       
    }

    public void ClearMap()
    {
        Thread myThread2 = new Thread(() =>
        {
            Dispatcher.Invoke(() =>
            {
                MapGenerator.Instance.ClearMap();
                LoadingScript.Instance.HideLoadingPanel();
            });
        });

        Thread myThread = new Thread(() =>
        {
            Dispatcher.Invoke(() =>
            {
                LoadingScript.Instance.ShowLoadingPanel();
                LoadingScript.Instance.EnableControls();
                myThread2.Start();
            });
        });
        myThread.Start();         
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}