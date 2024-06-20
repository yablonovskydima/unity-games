using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public static PlayerMovement instance;
    

    public static int Ammo = 10;
    public float speed = 12f;
    public float gravity = -9.8f;

    public int health;
    public int maxHealth = 100;
    public GameObject deadScreen;
    public GameObject UIShotgun;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isDead = false;

    public Text healthText;
    public Text ammoText;
    public Animator anim;

    public AudioSource ammoSound;
    public AudioSource Healing;
    public AudioSource Hurt;

    Vector3 velocity;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString() + "%";
        ammoText.text = Ammo.ToString();  
        deadScreen.SetActive(false);
    }

    void Update()
    {
        if (!isDead) 
        {
            healthText.text = health.ToString() + "%";
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -5f;
            }

            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * inputX + transform.forward * inputZ;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (move != Vector3.zero)
            {
                anim.SetBool("IsMoving", true);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ammo") 
        {
            Ammo += 50;
            UpdateAmmo();
            ammoSound.Play();
            Destroy(other.gameObject);
        }
        else if(other.tag == "Meds") 
        {
            AddHealth(25); 
            Healing.Play();
            Destroy(other.gameObject);
        }
       
    }

    public void TakeDamage(int damageAmount) 
    {
        health -= damageAmount;
        Hurt.Play();
        if(health <= 0) 
        {
            deadScreen.SetActive(true);
            UIShotgun.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            isDead = true;
            health = 0;
        }
        healthText.text = health.ToString() + "%";
    }

    public void AddHealth(int healthAmount) 
    {
        health += healthAmount;
        if(health > maxHealth) 
        {
            health = maxHealth;
            healthText.text = health.ToString() + "%";
        }
       
    }

    public void UpdateAmmo() 
    {
        ammoText.text = Ammo.ToString();
    }
}

