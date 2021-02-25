using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject placementIndicator;
    private ARSessionOrigin AROrig;
    private ARRaycastManager RaycastManager;
    private Pose placementpose;
    private bool placementIsValid = false;
    void Start()
    {
        AROrig = FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (placementIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementpose.position, placementpose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        RaycastManager = FindObjectOfType<ARRaycastManager>();
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        RaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        placementIsValid = hits.Count > 0;
        if (placementIsValid)
        {
            placementpose = hits[0].pose;
        }
    }
}
