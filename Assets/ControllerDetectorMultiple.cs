using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerDetectorMultiple: MonoBehaviour
{
    [Header("Controller Parameters")]
    public float autoChangeTime = 0.5f;

    private HMD HMD;
    private bool firstChange = true;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        HMD = GameObject.FindGameObjectWithTag("HMD").GetComponent<HMD>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        // Right Hand Thunmb Stick in Horizontal Direction
        float thumbStickX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        // Reset position
        if (thumbStickX == 0)
        {
            timer = 0;
            firstChange = true;
        }

        if (thumbStickX < 0)
        {
            if (firstChange)
            {
                HMD.ChangeHMDPageDown();
                firstChange = false;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > autoChangeTime)
                {
                    HMD.ChangeHMDPageDown();
                    timer = 0;
                }
            }

        }

        if (thumbStickX > 0)
        {
            if (firstChange)
            {
                HMD.ChangeHMDPageUp();
                firstChange = false;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > autoChangeTime)
                {
                    HMD.ChangeHMDPageUp();
                    timer = 0;
                }
            }
        }
    }
}
