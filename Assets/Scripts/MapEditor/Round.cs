using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Round
{

    public static float ToFour(float num)
    {
        var mod = nfmod(num, 4f);
        if (mod > 2)
            return num - mod + 4;
        return num - mod;
    }

    public static float ToThreePointSix(float num)
    {
        var mod = nfmod(num, 3.6f);
        if (mod > 2)
            return num - mod + 4;
        return num - mod;
    }



    static float nfmod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }
}
