using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    [Header("Gesture Parameters")]
    public float detectionThreshold = 0.05f;
    public bool addGestureMode = true;

    [Header("Gesture Objects")]
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;

    private List<OVRBone> fingerBones;
    private Gesture previousGesture;

    // Start is called before the first frame update
    void Start()
    {
        previousGesture = new Gesture();
        StartCoroutine(DelayRoutine(Initialize));
    }

    public IEnumerator DelayRoutine(Action actionToDo)
    {
        while (!skeleton.IsInitialized)
        {
            yield return null;
        }
        actionToDo.Invoke();
    }

    public void Initialize()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
    }

    // Update is called once per frame
    void Update()
    {
        if (addGestureMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        // Check if new gesture is used
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            // New Gesture
            Debug.Log("Gesture Found: " + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }
    }

    void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        int size = fingerBones.Count;
        foreach (var bone in fingerBones)
        {
            // Compare position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        Gesture currectGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);

                if (distance > detectionThreshold)
                {
                    isDiscarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currectGesture = gesture;
            }
        }

        return currectGesture;
    }
}
