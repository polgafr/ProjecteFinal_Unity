using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Cube
{
    public float x;
    public float y;
    public float z;

    public float w;
    public float h;
    public float d;

    public Cube(float pX, float pY, float pZ, float pWidth, float pHeight, float pDepth)
    {
        x = pX;
        y = pY;
        z = pZ;

        w = pWidth;
        h = pHeight;
        d = pDepth;
    }
}