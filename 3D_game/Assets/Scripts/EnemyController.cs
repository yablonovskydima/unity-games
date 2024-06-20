using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Health = 5;
    public GameObject explosion;

    public float playerRange = 10f;

    public Rigidbody body;
    public float moveSpeed;

    public bool Shoot = true;
    public float fireRate = 0.5f;
    private float counter;
    public GameObject bullet;
    public Transform firePoint;
    public AudioSource deathSound;
    public AudioSource shootSound;

    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < playerRange) 
        {
            Vector3 playerDir = PlayerMovement.instance.transform.position - transform.position;
            body.velocity = playerDir.normalized * moveSpeed;

            if (Shoot) 
            {
                
                counter -= Time.deltaTime;
                if(counter <= 0) 
                {
                    shootSound.Play();
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    counter = fireRate;
                }
            }

        }
        else 
        {
            body.velocity = Vector3.zero;
        }
    }

    public void TakeDamage() 
    {
        Health--;
        if(Health <= 0) 
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            deathSound.Play();
        }
    }
}
