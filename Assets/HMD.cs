using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class HMD : MonoBehaviour
{
    [Header("HMD Parameters")]
    public int maxPageIndex = 5;
    public int minPageIndex = 0;

    [Header("HMD Objects")]
    public TMP_Text debugText;

    private String debugContent;
    private int HMDPage;

    // Start is called before the first frame update
    void Start()
    {
        HMDPage = 0;
    }

    // Update is called once per frame
    void Update() 
    {
        debugText.SetText(debugContent + "\n Page" + HMDPage);
    }

    public void ChangeHMDPage(int newPage)
    {
        HMDPage = newPage;
    }

    public void ChangeHMDPageUp()
    {
        HMDPage++;
        if (HMDPage > maxPageIndex) HMDPage = minPageIndex;
    }

    public void ChangeHMDPageDown()
    {
        HMDPage--;
        if (HMDPage < minPageIndex) HMDPage = maxPageIndex;
    }

    public void Debug(String text)
    {
        debugContent = text;
    }

}
