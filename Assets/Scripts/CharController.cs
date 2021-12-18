using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public SpawnManager spawnManager;
    public float movementSpeed = 10f;
    private int triggerCounter = 1;
    void Start()
    {
        
    }

    void Update()
    {
        // Move with Up and Down keyboard arrow keys
        float movement = Input.GetAxis("Vertical") * movementSpeed;
        transform.Translate(new Vector3(0, 0, movement) * Time.deltaTime);
        if (transform.position.z > triggerCounter * 60)
        {
            Debug.Log(transform.position.z);
            spawnManager.SpawnTriggerEntered();
            triggerCounter++;
        }
    }
}
