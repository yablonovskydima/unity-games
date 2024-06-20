using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorModel;
    public GameObject colObject;
    public float openSpeed = 4;
    public bool isOpen;
    Vector3 doorPos;

    void Start()
    {
        doorPos = doorModel.position;
    }

    void Update()
    {
        if (isOpen && doorModel.position.y != -1f) 
        {
            
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, -1f, doorModel.position.z), openSpeed*Time.deltaTime);
            if(doorModel.position.y == -1f) 
            {
                colObject.SetActive(false);
            }
        }
        else if (!isOpen)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, doorPos, openSpeed * Time.deltaTime);
            if (doorModel.position.y == doorPos.y)
            {
                colObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOpen = false;
        }
    }
}
