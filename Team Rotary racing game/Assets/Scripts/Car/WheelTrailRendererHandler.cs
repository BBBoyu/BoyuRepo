using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    public bool isOverpassEmitter = false;

    TopDownCarController topDownCarController;
    TrailRenderer trailRenderer;
    CarLayerHandler carLayerHandler;

    void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        carLayerHandler = GetComponentInParent<CarLayerHandler>();

        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }


    void Update()
    {
        trailRenderer.emitting = false;

        if(topDownCarController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (carLayerHandler.IsDrivingOnOverpass() && isOverpassEmitter)
                trailRenderer.emitting = true;
            if (!carLayerHandler.IsDrivingOnOverpass() && !isOverpassEmitter)
                trailRenderer.emitting = true;
        }

    }
}
