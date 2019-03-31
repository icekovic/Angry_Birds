using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.Experimental.XR;
using System;

public class ARContol : Control
{
    ARSessionOrigin arOrigin;
    Pose placementPose;
    bool placementPoseIsValid;

    void Start()
    {
        XRSettings.LoadDeviceByName("");
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        updatePlacementPose();
        updatePlacementIndicator();
    }

    void updatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            indicator.SetActive(true);
            indicator.transform.position = placementPose.position;
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    void updatePlacementPose()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        bool success = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000);
        placementPoseIsValid = success;
        if (success)
        {
            placementPose.position = hit.point + hit.normal * 0.05f;
            indicator.transform.up = hit.normal;
            placementPose.rotation = Math.getBearing(Camera.main.transform.forward);
        }
    }

    public override PlacementPose getPlacementPose()
    {
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return new PlacementPose()
            {
                pose = placementPose
            };
        }

        return null;
    }
}
