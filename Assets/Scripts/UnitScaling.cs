using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitScaling
{
    //1 = 100000km
    public static int sizeScaling { get; } = 100000;

    public static double massScaling { get; } = 1.00e+25f;

    public static float fixedGameTime { get; set; } = 1f;
}
