using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {

    public GameObject LoadingPanel;
    public Slider[] Sliders;
    public Button[] Buttons;

    #region SingletonPattern
    public static LoadingScript _instance;
    public static LoadingScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LoadingScript>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("LoadingScript");
                    _instance = container.AddComponent<LoadingScript>();
                }
            }
            return _instance;
        }
    }
    #endregion

    public void ShowLoadingPanel()
    {
        LoadingPanel.gameObject.SetActive(true);
    }

    public void HideLoadingPanel()
    {
        LoadingPanel.gameObject.SetActive(false);
    }

    public void EnableControls()
    {
        foreach (var item in Sliders)
        {
            item.interactable = true;
        }
        foreach (var item in Buttons)
        {
            item.interactable = true;
        }
    }

    public void DisableControls()
    {
        foreach (var item in Sliders)
        {
            item.interactable = false;
        }
        foreach (var item in Buttons)
        {
            item.interactable = false;
        }
    }
}
