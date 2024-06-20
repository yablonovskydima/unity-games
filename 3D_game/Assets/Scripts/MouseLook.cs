using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 100f;
    float xRotation = 0f;
    public Transform player;
    public Camera cam;
    public GameObject BulletImpact;
    public Animator gunAnim;
    public AudioSource gunshotSound;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!PlayerMovement.instance.isDead) 
        {
            float mouseX = Input.GetAxis("MouseX") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("MouseY") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);

            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerMovement.Ammo > 0)
                {
                    gunshotSound.Play();
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(BulletImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.GetComponent<EnemyController>().TakeDamage();
                        }
                    }
                    PlayerMovement.Ammo--;
                    gunAnim.SetTrigger("Shoot");
                    PlayerMovement.instance.UpdateAmmo();
                }
            }
        }

    }
}

    