using System.Collections.Generic;
using UnityEngine;

public class CelestialBodyManager : MonoBehaviour
{
    public List<CelestialBody> _allBodies = new List<CelestialBody>();

    void Start()
    {
        getAllCelestialBodiesInScene();
    }

    void getAllCelestialBodiesInScene()
    {
        CelestialBody[] getObject = FindObjectsOfType<CelestialBody>();

        for (int i = 0; i < getObject.Length; i++)
        {
            _allBodies.Add(getObject[i]);
        }
    }
}
