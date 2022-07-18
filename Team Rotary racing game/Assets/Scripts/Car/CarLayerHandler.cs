using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> defaultLayerSpriteRenders = new List<SpriteRenderer>();

    List<Collider2D> overpassColliderList = new List<Collider2D>();
    List<Collider2D> underpassColliderList = new List<Collider2D>();

    Collider2D carCollider;

    bool isDrivingOnOverpass = false;
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.sortingLayerName == "Default")
                defaultLayerSpriteRenders.Add(spriteRenderer);
        }

        foreach (GameObject overpassColliderGameObject in GameObject.FindGameObjectsWithTag("OverpassCollider"))
        {
            overpassColliderList.Add(overpassColliderGameObject.GetComponent<Collider2D>());
        }

        foreach (GameObject underpassColliderGameObject in GameObject.FindGameObjectsWithTag("UnderpassCollider"))
        {
            underpassColliderList.Add(underpassColliderGameObject.GetComponent<Collider2D>());
        }

        carCollider = GetComponentInChildren<Collider2D>();
    }
    void Start()
    {
        UpdateSortingAndCollisionLayers();
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

        SetCollisionWithOverpass();
    }

    void SetCollisionWithOverpass()
    {
        foreach (Collider2D collider2D in overpassColliderList)
        {
            Physics2D.IgnoreCollision(carCollider, collider2D, !isDrivingOnOverpass);
        }

        foreach (Collider2D collider2D in underpassColliderList)
        {
            if (isDrivingOnOverpass)
                Physics2D.IgnoreCollision(carCollider, collider2D, true);
            else Physics2D.IgnoreCollision(carCollider, collider2D, false);
        }
    }
    void SetSortingLayer(string layerName)
    {
        foreach(SpriteRenderer spriteRenderer in defaultLayerSpriteRenders)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    public bool IsDrivingOnOverpass()
    {
        return isDrivingOnOverpass;
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
