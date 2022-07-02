using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> defaultLayerSpriteRenders = new List<SpriteRenderer>();

    bool isDrivingOnOverpass = false;
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.sortingLayerName == "Default")
                defaultLayerSpriteRenders.Add(spriteRenderer);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnOverpass)
        {
            SetSortingLayer("RaceTrackOverpass");
        }
        else
        {
            SetSortingLayer("Default");
        }
    }

    void SetSortingLayer(string layerName)
    {
        foreach(SpriteRenderer spriteRenderer in defaultLayerSpriteRenders)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("UnderpassTrigger"))
        {
            isDrivingOnOverpass = false;

            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverpass = true;

            UpdateSortingAndCollisionLayers();
        }
    }
}
