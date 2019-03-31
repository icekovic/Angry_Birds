using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Control : MonoBehaviour
{
    protected GameObject indicator;

    public void setIndicator(GameObject indicator)
    {
        this.indicator = indicator;
    }

    public abstract PlacementPose getPlacementPose();
}

public class PlacementPose
{
    public Pose pose;
}