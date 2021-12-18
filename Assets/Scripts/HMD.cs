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

    [Header("HMD Objects")]
    public List<GameObject> widgets;
    public TMP_Text debugText;
    public GameObject arrow;
    public GameObject arrowComponent1;
    public GameObject arrowComponent2;

    private String debugContent;
    private int prevHMDPage;

    private int minPageIndex;
    private int maxPageIndex;

    private float arrowMovePos = 0;
    private bool arrowMoveForward = true;

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
        Debug.Log(arrowMovePos);
    }

    private void ChangeWidget(int newPageNumber)
    {
        Debug.Log("new " + newPageNumber);
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
