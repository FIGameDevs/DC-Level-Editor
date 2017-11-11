using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Round
{

    public static float ToFour(float num)
    {/*
        //var mod = num % 4f;
        var mod = nfmod(num, 4f);
        if (Mathf.Abs(mod) > 2)
            return num - mod + 4;
        return num - mod;*/
        if (num > 0)
            return 4f * (int)((num + 2f) / 4f);
        else
            return 4f * (int)((num - 2f) / 4f);

    }

    public static float ToThreePointSix(float num)
    {/*
        var mod = num % 3.6f;
        //var mod = nfmod(num, 3.6f);
        if (Mathf.Abs(mod) > 1.8f)
            return num - mod + 4;
        return num - mod;*/
        if (num > 0)
            return 3.6f * (int)((num + 1.8f) / 3.6f);
        else
            return 3.6f * (int)((num - 1.8f) / 3.6f);

    }



    static float nfmod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }
}
