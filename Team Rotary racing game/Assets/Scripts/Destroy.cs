using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Destroy the gameobject after 3s
        Destroy(gameObject, 3f);
    }
}
