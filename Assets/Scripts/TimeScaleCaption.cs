using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleCaption : MonoBehaviour
{

    [SerializeField] TMP_Text _captionText;
    Slider _sliderComponent;

    private void Awake()
    {
        _sliderComponent = GetComponent<Slider>();
    }

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
