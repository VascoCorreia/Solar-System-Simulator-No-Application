using UnityEngine;

//This class is responsible for changing the timeScale of the simulation according to the value that the slider is currently on

public class TimeController : MonoBehaviour
{
    [SerializeField] UIController _uiController;
    private void Start()
    {
        _uiController.OnTimeScaleChangedEventHandler += OnTimeScaleChanged;
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