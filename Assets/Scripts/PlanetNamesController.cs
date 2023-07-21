using TMPro;
using UnityEngine;

//Controlls the Names of each planet in the GUI
public class PlanetNamesController : MonoBehaviour
{
    [SerializeField] Transform _parentPlanet;
    [SerializeField] Transform _mainCamera;
    [SerializeField] GameObject solarSystem;
    [SerializeField] float zAxisOffset;
    [SerializeField] float distanceFromCameraForNameToFade, _maxTextSize, _minTextSize, _maxDistanceFromCameraToPlanetNameToGrow;
    [SerializeField] TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    //Moves the gameobject so that it follows the correct planet
    void LateUpdate()
    {
        transform.position = new Vector3(_parentPlanet.position.x, _parentPlanet.position.y, _parentPlanet.position.z + zAxisOffset);
    }

    private void Update()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, _mainCamera.transform.rotation * Vector3.up);

        DeactivateTextIfCameraTooClose();
        ChangeTextSizeWithCameraDistance();
    }

    //If the camera is too close to the planet we hide its name text so that it does not interfere with the user experience
    private void DeactivateTextIfCameraTooClose()
    {
        if (Vector3.Distance(_mainCamera.transform.position, transform.position) < distanceFromCameraForNameToFade)
        {
            text.alpha = 0f;
        }
        else if (Vector3.Distance(_mainCamera.transform.position, transform.position) >= distanceFromCameraForNameToFade)
        {
            text.alpha = 1f;
        }
    }

    //The further the camera is to the planets the bigger its text names must be so that they are visible to the player
    private void ChangeTextSizeWithCameraDistance()
    {
        float currentPercetangeBetweenMinAndMaxDistance = Mathf.InverseLerp(distanceFromCameraForNameToFade, _maxDistanceFromCameraToPlanetNameToGrow, Vector3.Distance(_mainCamera.transform.position, transform.position));

        text.fontSize = Mathf.Lerp(_minTextSize, _maxTextSize, currentPercetangeBetweenMinAndMaxDistance);

        if (text.fontSize >= _maxTextSize)
        {
            text.fontSize = _maxTextSize;
        }

        if (text.fontSize <= _minTextSize)
        {
            text.fontSize = _minTextSize;
        }
    }
}
