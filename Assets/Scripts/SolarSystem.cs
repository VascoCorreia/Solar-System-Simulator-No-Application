using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    Vector3 _initalSpeed = new Vector3(-3.008021e-06f, 0f, 0f);
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += _initalSpeed * (Time.deltaTime * UnitScaling.fixedGameTime);
    }
}
