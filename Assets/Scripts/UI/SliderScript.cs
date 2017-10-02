using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

    public Text SliderText;
    public Slider mySlider;
    public MapGeneratorTypes WhichParameter;

    public void SliderChanged_ValueWithPercent(float value)
    {
        SliderText.text = value.ToString() + "%";
    }

    public void SliderChanged_OnlyValue(float value)
    {
        SliderText.text = value.ToString();
    }

    private void Start()
    {
        if (WhichParameter == MapGeneratorTypes.MapWidth)
        {
            int tmp = Engine.Instance.MapWidth.Value;

            mySlider.minValue = Engine.Instance.MapWidth.MinValue;
            mySlider.maxValue = Engine.Instance.MapWidth.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.MapHeight)
        {
            int tmp = Engine.Instance.MapHeight.Value;

            mySlider.minValue = Engine.Instance.MapHeight.MinValue;
            mySlider.maxValue = Engine.Instance.MapHeight.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.MountainPercent)
        {
            int tmp = Engine.Instance.MountainPercent.Value;

            mySlider.minValue = Engine.Instance.MountainPercent.MinValue;
            mySlider.maxValue = Engine.Instance.MountainPercent.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.RiverPercent)
        {
            int tmp = Engine.Instance.RiverPercent.Value;

            mySlider.minValue = Engine.Instance.RiverPercent.MinValue;
            mySlider.maxValue = Engine.Instance.RiverPercent.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.StaticTemp)
        {
            int tmp = Engine.Instance.StaticTemp.Value;

            mySlider.minValue = Engine.Instance.StaticTemp.MinValue;
            mySlider.maxValue = Engine.Instance.StaticTemp.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.DynamicTemp)
        {
            int tmp = Engine.Instance.DynamicTemp.Value;

            mySlider.minValue = Engine.Instance.DynamicTemp.MinValue;
            mySlider.maxValue = Engine.Instance.DynamicTemp.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.TempDifference)
        {
            int tmp = Engine.Instance.TempDifference.Value;

            mySlider.minValue = Engine.Instance.TempDifference.MinValue;
            mySlider.maxValue = Engine.Instance.TempDifference.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.BiomePercent)
        {
            int tmp = Engine.Instance.BiomePercent.Value;

            mySlider.minValue = Engine.Instance.BiomePercent.MinValue;
            mySlider.maxValue = Engine.Instance.BiomePercent.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }

        if (WhichParameter == MapGeneratorTypes.CityPercent)
        {
            int tmp = Engine.Instance.CityPercent.Value;

            mySlider.minValue = Engine.Instance.CityPercent.MinValue;
            mySlider.maxValue = Engine.Instance.CityPercent.MaxValue;
            mySlider.wholeNumbers = true;
            mySlider.value = tmp;
        }
    }

    public void SliderValueChanged(float value)
    {
        if (WhichParameter == MapGeneratorTypes.MapWidth)
        {
            Engine.Instance.MapWidth.Value = (int)value;
            SliderChanged_OnlyValue(value);
        }

        if (WhichParameter == MapGeneratorTypes.MapHeight)
        {
            Engine.Instance.MapHeight.Value = (int)value;
            SliderChanged_OnlyValue(value);
        }

        if (WhichParameter == MapGeneratorTypes.MountainPercent)
        {
            Engine.Instance.MountainPercent.Value = (int)value;
            SliderChanged_ValueWithPercent(value);
        }

        if (WhichParameter == MapGeneratorTypes.RiverPercent)
        {
            Engine.Instance.RiverPercent.Value = (int)value;
            SliderChanged_ValueWithPercent(value);
        }

        if (WhichParameter == MapGeneratorTypes.StaticTemp)
        {
            Engine.Instance.StaticTemp.Value = (int)value;
            SliderChanged_OnlyValue(value);
        }

        if (WhichParameter == MapGeneratorTypes.DynamicTemp)
        {
            Engine.Instance.DynamicTemp.Value = (int)value;
            SliderChanged_OnlyValue(value);
        }

        if (WhichParameter == MapGeneratorTypes.TempDifference)
        {
            Engine.Instance.TempDifference.Value = (int)value;
            SliderChanged_OnlyValue(value);
        }

        if (WhichParameter == MapGeneratorTypes.BiomePercent)
        {
            if (Engine.Instance.CityPercent.Value + value > 100)
            {
                GameObject.Find("CitySlider").GetComponent<Slider>().value = 100 - value;
            }

            Engine.Instance.BiomePercent.Value = (int)value;
            SliderChanged_ValueWithPercent(value);
        }

        if (WhichParameter == MapGeneratorTypes.CityPercent)
        {
            if (Engine.Instance.BiomePercent.Value + value > 100)
            {
                GameObject.Find("BiomeSlider").GetComponent<Slider>().value = 100 - value;
            }

            Engine.Instance.CityPercent.Value = (int)value;
            SliderChanged_ValueWithPercent(value);
        }
    }

    public void onButtonClick(float value)
    {
        mySlider.value += value;
    }
}

