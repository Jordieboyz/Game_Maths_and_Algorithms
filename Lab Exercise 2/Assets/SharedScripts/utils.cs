using UnityEngine;
using System;

public class Utils {
    static public Func<Vector3, Vector3, float> calcAngle = (a, b) => -MathF.Atan2(a.z-b.z, a.x-b.x) * Mathf.Rad2Deg;
    static public Func<Vector3, Vector3, float> dist2DXZ= (a, b) => Vector2.Distance(stripY(a), stripY(b));
    static public Func<Vector3, Vector2> stripY = (vec) => new Vector2(vec.x, vec.z);
    static public Func<Vector3, Vector2> stripX = (vec) => new Vector2(vec.y, vec.z);
    static public Func<Vector3, Vector2> stripZ = (vec) => vec;
}





