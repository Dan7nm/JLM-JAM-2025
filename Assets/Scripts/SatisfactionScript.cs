using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SatisfactionScript : MonoBehaviour
{
    [SerializeField] float maxSatisfaction = 5.0f;
    [SerializeField] float curSatisfaction = 4.0f;

    [SerializeField] private TextMeshProUGUI sliderText = null;
    //[SerializeField] float maxSliderAmount = 100.0f;
    //[SerializeField] float curSliderAmount = 0.0f;
    [SerializeField] GameObject slider;

    private Image fillImage; // Image component of the slider's fill area

    private void Start()
    {
        slider.GetComponent<Slider>().value = curSatisfaction;

        slider.GetComponent<Slider>().maxValue = maxSatisfaction;
        //slider.GetComponent<Slider>().value = curSatisfaction;

        // Get the fill image
        fillImage = slider.GetComponent<Slider>().fillRect.GetComponent<Image>();

        // Update the color initially
        UpdateSliderColor();
    }
    void updateSatisfaction(int newMaxSatisfaction, int newCurSatisfaction)
    {
        maxSatisfaction += newMaxSatisfaction;
        curSatisfaction += newCurSatisfaction;
    }

    public void changeSatAfterTenSec()
    {
        curSatisfaction -= 0.5f;
        if (curSatisfaction < 0) curSatisfaction = 0;
        slider.GetComponent<Slider>().value = curSatisfaction;
        UpdateSliderColor();
    }

    private void UpdateSliderColor()
    {
        if (fillImage == null) return;

        float percentage = (float)curSatisfaction / maxSatisfaction;

        if (percentage > 0.8f)
        {
            fillImage.color = Color.green;
        }
        else if (percentage > 0.4f)
        {
            fillImage.color = new Color(1f, 0.65f, 0f); // Orange color
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

    // Update is called once per frame
    //public void sliderChange(float value)
    //{
    //    float localValue = value * maxSliderAmount;
    //    sliderText.text = localValue.ToString("0.0");
    //}
}
