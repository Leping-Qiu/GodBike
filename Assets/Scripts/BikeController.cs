using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    public enum RideModes
    {
        AutoPilotBreaks,
        AutoPilotNoBreaks,
        Controller,
        Keyboard
    }

    [Header("Bike Controls")]
    public float movementSpeed = 6f;
    public RideModes rideMode = RideModes.AutoPilotBreaks;

    [Header("Spawn Controls")]
    public SpawnManager spawnManager;
    public float blockSize = 60f;

    private int triggerCounter = 1;
    private float movement = 1;

    void Start()
    {
        
    }

    void Update()
    {
        OVRInput.Update();

        if (rideMode == RideModes.AutoPilotBreaks)
        {
            float indexTrigger = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
            float handTrigger = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
            movement = (1 - indexTrigger) * movementSpeed * (1 + handTrigger * 15);
        }
        else if (rideMode == RideModes.AutoPilotNoBreaks)
        {
            movement = 1 * movementSpeed;
        }
        else if (rideMode == RideModes.Controller)
        {
            float thumbStickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;
            movement = thumbStickY * movementSpeed;
        }
        else if (rideMode == RideModes.Keyboard)
        {
            // Move with Up and Down keyboard arrow keys
            movement = Input.GetAxis("Vertical") * movementSpeed;
        }

        transform.Translate(new Vector3(0, 0, movement) * Time.deltaTime);

        // Trigger Spawning
        if (transform.position.z > triggerCounter * blockSize)
        {
            spawnManager.SpawnTriggerEntered();
            triggerCounter++;
        }
    }
}
