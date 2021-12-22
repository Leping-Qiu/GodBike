using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class HMD : MonoBehaviour
{
    [Header("HMD Parameters")]
    public float arrowMoveRange = 0.4f;
    public float arrowMoveSpeed = 0.002f;

    [Header("HMD Widgets")]
    public List<GameObject> widgets;

    [Header("HMD Dashboard")]
    public TMP_Text textTime;
    public TMP_Text textSpeed;
    public TMP_Text textHeading;
    public TMP_Text textActivity;
    public TMP_Text textHeartRate;

    [Header("HMD Arrow")]
    public GameObject arrow;
    public GameObject arrowComponent1;
    public GameObject arrowComponent2;

    private int prevHMDPage;

    private int minPageIndex;
    private int maxPageIndex;

    private float arrowMovePos = 0;
    private bool arrowMoveForward = true;

    private float activity = 0;
    private float heartRate = 79;
    private float heartRateTimer = 15;

    // Start is called before the first frame update
    void Start()
    {
        prevHMDPage = 0;
        minPageIndex = 0;
        maxPageIndex = widgets.Count - 1;

        foreach (var widget in widgets)
        {
            widget.SetActive(false);
        }

        widgets[minPageIndex].SetActive(true);
        arrowComponent1.GetComponent<MeshRenderer>().material.color = new Color(0.008f, 0.348f, 0.813f, 0.5f);
        arrowComponent2.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update() 
    {
        // Move Arrow
        if (arrowMoveForward)
        {
            arrow.transform.Translate(new Vector3(0, arrowMoveSpeed, 0));
            arrowMovePos += arrowMoveSpeed;
            if (arrowMovePos > arrowMoveRange) arrowMoveForward = false;
        }
        else
        {
            arrow.transform.Translate(new Vector3(0, -arrowMoveSpeed * 1.5f, 0));
            arrowMovePos -= arrowMoveSpeed * 1.5f;
            if (arrowMovePos < 0) arrowMoveForward = true;
        }

        // Update DashBoard
        textTime.SetText(DateTime.UtcNow.AddHours(8).ToString("HH:mm"));
        textHeading.SetText("175");
        textActivity.SetText((activity += 0.1f * Time.deltaTime).ToString("0.0"));

        // Random Heart Rate
        heartRateTimer += Time.deltaTime;
        if (heartRateTimer > 15 && heartRate < 130)
        {
            heartRate += 1;
            heartRateTimer = 0;
            textHeartRate.SetText(heartRate.ToString("0"));
        }
    }

    public void ChangeSpeedometer(float speed)
    {
        textSpeed.SetText(speed.ToString("0.0"));
    }

    private void ChangeWidget(int newPageNumber)
    {
        widgets[prevHMDPage].SetActive(false);
        widgets[newPageNumber].SetActive(true);
        prevHMDPage = newPageNumber;
    }

    
    public void ChangeHMDPageNumber(int newPageNumber)
    {
        if (newPageNumber >= minPageIndex && newPageNumber <= maxPageIndex) 
            ChangeWidget(newPageNumber);
    }

    public void ChangeHMDPageUp()
    {
        int newHMDPage = prevHMDPage + 1;
        if (newHMDPage > maxPageIndex) newHMDPage = minPageIndex;
        ChangeWidget(newHMDPage);

    }

    public void ChangeHMDPageDown()
    {
        int newHMDPage = prevHMDPage - 1;
        if (newHMDPage < minPageIndex) newHMDPage = maxPageIndex;
        ChangeWidget(newHMDPage);
    }
}
