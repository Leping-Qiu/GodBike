using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetectorSingle : MonoBehaviour
{
    [Header("Head Parameters")]
    public float detectionThreshold = 20f;

    [Header("Head Objects")]
    public OVRCameraRig cameraRig;
    
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
        Quaternion headsetRotation = cameraRig.centerEyeAnchor.rotation;

        //HMD.Debug("w " + headsetRotation.w + "\nx " + headsetRotation.x + "\ny " + headsetRotation.y + "\nz " + headsetRotation.z);
        float turnAngle = headsetRotation.eulerAngles.z;
        Debug.Log(turnAngle);

        // Reset position
        if (turnAngle >= 0 && turnAngle <= 10 || turnAngle <= 360 && turnAngle >= 350)
        {
            HMD.Debug("page neutral" + turnAngle);
            hasChanged = false;
        }

        if (!hasChanged && turnAngle >= detectionThreshold && turnAngle <= 180)
        {
            HMD.Debug("page down" + turnAngle);
            HMD.ChangeHMDPageDown();
            hasChanged = true;
        }

        if (!hasChanged && turnAngle <= (360 - detectionThreshold) && turnAngle >= 180)
        {
            HMD.Debug("page up" + turnAngle);
            HMD.ChangeHMDPageUp();
            hasChanged = true;
        }
    }
}
