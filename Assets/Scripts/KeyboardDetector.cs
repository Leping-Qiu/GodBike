using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardDetector : MonoBehaviour
{
    private HMD HMD;

    // Start is called before the first frame update
    void Start()
    {
        HMD = GameObject.FindGameObjectWithTag("HMD").GetComponent<HMD>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HMD.ChangeHMDPageDown();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HMD.ChangeHMDPageUp();
        }
    }
}
