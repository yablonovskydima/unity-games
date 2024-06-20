using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private float bulletSpeed = 13f;
    public Rigidbody rb;
    private Vector3 dir;
    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) != 0)
        {
            Vector3 playerDir = PlayerMovement.instance.transform.position - transform.position;
            rb.velocity = playerDir.normalized * bulletSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            PlayerMovement.instance.TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
 