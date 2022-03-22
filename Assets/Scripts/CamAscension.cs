using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/Camera Ascension")]
public class CameraAscension : Randomizer
{
    public Camera mainCamera;
    private int count = 0;

    private float initHeight = 2f;

    //private Vector3 initialCameraPosition = new Vector3(-14, 6, 0f);
    private Vector3 initialCameraPosition = new Vector3(8f, 2f, 0f);
    //private Quaternion initialCameraRotation = Quaternion.Euler(12, 90, 0f);
    private Quaternion initialCameraRotation = Quaternion.Euler(30f, -90f, 0f);

    private Vector3 focusPoint = new Vector3(-9f, 0f, 0f);

    protected override void OnUpdate()
    {
        float tilt; float pan;
        Vector3 offset = new Vector3(-1f * count, initHeight + (float)count, 0f);

        Vector3 pos = initialCameraPosition + offset;

        Vector3 cam2fp = focusPoint - pos;

        tilt = Mathf.Atan(cam2fp[1]/cam2fp[0]) * 180 / Mathf.PI;

        Quaternion rot = Quaternion.Euler(tilt, -90f, 0f);

        Debug.Log(tilt);

        mainCamera.transform.position = pos;
        mainCamera.transform.rotation = rot;
        count++;
    }

    protected override void OnIterationEnd()
    {
        var s = 0;
    }
}