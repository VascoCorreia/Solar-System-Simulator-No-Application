using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] UIController _uiController;
    private void Start()
    {
        _uiController.OnTimeScaleChangedEventHandler += OnTimeScaleChanged;
    }

    private void Update()
    {

    }

    //maximum value in editor is capped at 100
    private void OnTimeScaleChanged(object sender, UIController.OnTimeScaleChangedEventArgs e)
    {
        UnitScaling.fixedGameTime = e.sliderValue;

        e.currTimeStep.text = e.sliderValue + " seconds / second";

        if (e.sliderValue == 1)
            e.currTimeStep.text = e.sliderValue + " second / second";
    }
}