using UnityEngine;

public class GravitySimulation : MonoBehaviour
{
    public double gravitationalConstant { get; } = 1.5941358e-18d;

    CelestialBody[] celestialBodies;
    void Awake()
    {
        celestialBodies = FindObjectsOfType<CelestialBody>();
    }

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
