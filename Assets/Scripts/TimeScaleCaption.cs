using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This class is responsible for correctly setting the text above the TimeScale Slider, in a way that is comprehensive for the user.
public class TimeScaleCaption : MonoBehaviour
{

    [SerializeField] TMP_Text _captionText;
    Slider _sliderComponent;

    private void Awake()
    {
        _sliderComponent = GetComponent<Slider>();
    }

    //If timescale is bigger than 1 day in seconds it changed to days and hours until its maximum that is 1 year/second
    private void Update()
    {
        if(_sliderComponent.value == _sliderComponent.maxValue)
        {
            _captionText.text = "1 year / second";
        }

        else if (_sliderComponent.value > 86400)
        {
            int days = (int)_sliderComponent.value / 86400;
            float seconds = _sliderComponent.value % 86400;
            int hours = (int)seconds / 3600;
            _captionText.text = days + " days and " + hours + " hours " + " / second";
        }
    }
}
