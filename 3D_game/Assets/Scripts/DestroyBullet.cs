using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    public float lifetime;
    void Start()
    {
        
    }

    
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
