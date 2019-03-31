using UnityEngine;

public static class Math
{
    public static Quaternion getBearing(Vector3 forward)
    {
        return Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z).normalized);
    }
}