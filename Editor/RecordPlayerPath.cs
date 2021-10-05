using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionObject
{
    private float x;
    private float y;
    public PositionObject(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

public class RecordPlayerPath : MonoBehaviour
{
    public GameObject objectToCapture;
    List<PositionObject> listPath = new List<PositionObject>();
    private int nextUpdate = 1;
    private bool startRecord = false;

    public void StartRecord()
    {
        startRecord = true;
    }

    public List<PositionObject> GetPositionsAndFinalRecord()
    {
        startRecord = false;
        return this.listPath;
    }

    void Update()
    {
        if (Time.time >= nextUpdate && startRecord)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            UpdateEverySecond();
        }
    }
    void UpdateEverySecond()
    {
        Vector3 position = objectToCapture.transform.position;
        listPath.Add(new PositionObject(position.x, position.y));
    }
}
