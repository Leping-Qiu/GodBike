using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerDetectorSingle : MonoBehaviour
{
    
    private HMD HMD;
    private bool hasChanged = false; // Change Lock

    // Start is called before the first frame update
    void Start()
    {
        HMD = GameObject.FindGameObjectWithTag("HMD").GetComponent<HMD>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        // Right Hand Thunmb Stick in Horizontal Direction
        float thumbStickX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        if (thumbStickX == 0)
        {
            hasChanged = false;
        }

        if (!hasChanged && thumbStickX < 0)
        {
            HMD.ChangeHMDPageDown();
            hasChanged = true;
        }

        if (!hasChanged && thumbStickX > 0)
        {
            HMD.ChangeHMDPageUp();
            hasChanged = true;
        }
    }
}
