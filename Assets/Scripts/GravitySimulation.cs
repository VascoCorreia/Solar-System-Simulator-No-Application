using UnityEngine;

//This class haas a single instance (not singleton though) and is used to call the functions that calculate the gravity forces between all bodies 
//and also calls the function that moves all celestial bodies objects using the previously calculated gravity

public class GravitySimulation : MonoBehaviour
{
    public double gravitationalConstant { get; } = 1.5941358e-18d;

    CelestialBody[] celestialBodies;
    void Awake()
    {
        celestialBodies = FindObjectsOfType<CelestialBody>();
    }

    //
    void FixedUpdate()
    {
        for (int i = 0; i < celestialBodies.Length; i++)
        {
            celestialBodies[i].NewtonianGravitation(celestialBodies);
        }


        for (int i = 0; i < celestialBodies.Length; i++)
        {
            celestialBodies[i].applyGravity();
        }
    }
}
