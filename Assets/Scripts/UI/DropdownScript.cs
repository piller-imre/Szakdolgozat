using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{

    public Dropdown myDropdown;

    private Engine engine;

    public void onValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                engine.ActualSeason = Seasons.Spring;
                break;
            case 1:
                engine.ActualSeason = Seasons.Summer;
                break;
            case 2:
                engine.ActualSeason = Seasons.Fall;
                break;
            case 3:
                engine.ActualSeason = Seasons.Winter;
                break;
        }
    }

    private void Start()
    {
        engine = Engine.Instance;

        myDropdown.value = (int)engine.ActualSeason;
    }

}
