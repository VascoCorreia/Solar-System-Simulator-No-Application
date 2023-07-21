using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This class is responsible for handling the planet dropdown and its behaviour.
//It also handles the planet Information GUI
public class UIController : MonoBehaviour
{
    CelestialBodyManager _bodiesManager;

    [SerializeField] TMP_Dropdown _dropdown;
    [SerializeField] TMP_Text _timeScale;
    [SerializeField] TMP_Text _currTimeStep;
    [SerializeField] TMP_Text _planetName, _planetMass, _planetDiameter, _planetTilt, _planetOrbitalPeriod, _planetSpeed;
    [SerializeField] TMP_Text[] _uiPlanetData;
    [SerializeField] Slider _timeScaleSlider;
    [SerializeField] RectTransform[] _planetDataUIText;
    [SerializeField] Button resetBtn;
    [SerializeField] CameraController _camera;
    [SerializeField] GameObject _planetDataUI;

    Dictionary<string, int> _bodies = new Dictionary<string, int>()
    {
        {"Solar System", 0 },
        {"Sun", 1 },
        {"Mercury", 2 },
        {"Venus", 3 },
        {"Terra", 4 },
        {"Marte", 5 },
        {"Jupiter", 6 },
        {"Saturn", 7},
        {"Uranus", 8 },
        {"Neptune", 9 }
    };

    //This delegate/"method" will be the event handler in the subscribers
    //this arguments are .NET convention. source is the class that that is publishing the event, and args is any data that we want to send in the event
    public event EventHandler<OnPlanetClickedEventArgs> OnDropdownPlanetClickedEventHandler;
    public event EventHandler<OnTimeScaleChangedEventArgs> OnTimeScaleChangedEventHandler;

    //data to be sent on event
    public class OnPlanetClickedEventArgs : EventArgs
    {
        public int planetIndex;
    }
    public class OnTimeScaleChangedEventArgs : EventArgs
    {
        public int sliderValue;
        public TMP_Text currTimeStep;
    }

    private void Awake()
    {
        if (_dropdown != null)
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_bodies.Keys.ToList());

            _dropdown?.onValueChanged.AddListener(delegate { OnPlanetClicked(_dropdown); });

        }

        _bodiesManager = FindAnyObjectByType<CelestialBodyManager>();
    }

    void Start()
    {
        //Everytime we click on a value different from the current one in the drop down an event is called
        //this event in turn calls another function OnPlanetClicked.

        //Everytime we chenge the value in the time slider an event is called, this event calls another function OnTimeScaleChanged
        _timeScaleSlider?.onValueChanged.AddListener(delegate { OnTimeScaleChanged(_timeScaleSlider); });
    }

    private void FixedUpdate()
    {
        deactivatePlanetDataUI();
    }

    //This function activates the UI and then updates its values according to the value that was clicked in the dropdown
    protected virtual void OnPlanetClicked(TMP_Dropdown dropdown)
    {
        activatePlanetDataUI();
        updatePlanetUIData(dropdown.value, _uiPlanetData);
        OnDropdownPlanetClickedEventHandler?.Invoke(this, new OnPlanetClickedEventArgs { planetIndex = dropdown.value });
    }

    protected virtual void OnTimeScaleChanged(Slider slider)
    {
        OnTimeScaleChangedEventHandler?.Invoke(this, new OnTimeScaleChangedEventArgs { sliderValue = (int)slider.value, currTimeStep = _currTimeStep });
    }

    void updatePlanetUIData(int id, TMP_Text[] data)
    {
        CelestialBody _currentSelectedPlanet = null;

        foreach (CelestialBody planet in _bodiesManager._allBodies)
        {
            if (planet.GetComponent<CelestialBody>().id == id)
            {
                _currentSelectedPlanet = planet;
            }
        }

        if (_currentSelectedPlanet != null)
        {
            ////name
            _planetName.text = _currentSelectedPlanet.name;
                
            ////mass
            _planetMass.text = "Mass: " + (_currentSelectedPlanet.mass * UnitScaling.massScaling).ToString("E2") + " Kg";
            ////diameter
            _planetDiameter.text = "Diameter: " + (_currentSelectedPlanet.diameter / 1000).ToString() + " Km";

            ////tilt
            _planetTilt.text = "Axial Tilt: " + _currentSelectedPlanet.tilt.ToString() + "º";

            ////Orbital period (s)
            _planetOrbitalPeriod.text = "Orbital Period: " + Math.Round(_currentSelectedPlanet.rotationTime / 3600).ToString() + " hours";

            ////velocity
            _planetSpeed.text = "Speed: " + _currentSelectedPlanet._speedNotScaled.ToString() + " Km/h";
        }

    }

    void deactivatePlanetDataUI()
    {
        if (!_camera.isLocked || _dropdown.value == 0)
        {
            LeanTween.alpha(_planetDataUI.GetComponent<RectTransform>(), 0, 1);
            LeanTween.moveLocalX(_planetDataUI, 1500, 1);
        }
    }

    void activatePlanetDataUI()
    {
        LeanTween.alpha(_planetDataUI.GetComponent<RectTransform>(), 0.211f, 1);
        LeanTween.moveLocal(_planetDataUI, new Vector3(940, -1.7f), 1);
    }

    public void resetScene()
    {
        UnitScaling.fixedGameTime = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
