using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectorMultiple : MonoBehaviour
{
    [Header("Head Parameters")]
    public float detectionThreshold = 20f;
    public float autoChangeTime = 0.5f;

    [Header("Head Objects")]
    public OVRCameraRig cameraRig;

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
        Quaternion headsetRotation = cameraRig.centerEyeAnchor.rotation;
        float turnAngle = headsetRotation.eulerAngles.z;
        Debug.Log(turnAngle);

        // Reset position
        if (turnAngle >= 0 && turnAngle <= 10 || turnAngle <= 360 && turnAngle >= 350)
        {
            timer = 0;
            firstChange = true;
        }

        if (turnAngle >= detectionThreshold && turnAngle <= 180)
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

        if (turnAngle <= (360 - detectionThreshold) && turnAngle >= 180)
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
