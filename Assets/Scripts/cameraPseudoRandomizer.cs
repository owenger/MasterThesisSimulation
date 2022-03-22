using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/Camera Psuedo Randomizer")]
public class CameraPseudoRandomizer : Randomizer
{
    public Camera mainCamera;

    public Vector3 cam0Trans = new Vector3(-4f, 3f, 0f);
    public Vector3 cam0Rot = new Vector3(10f, 90f, 0f);
    public Vector3 cam1Trans = new Vector3(0f, 10f, 5f);
    public Vector3 cam1Rot = new Vector3(60f, 100f, 0);
    public Vector3 cam2Trans = new Vector3(-1, 9, -10);
    public Vector3 cam2Rot = new Vector3(50, 70, 0);

    private int count = 0;

    protected override void OnUpdate()
    {
        if (count == 0)
        {
            // first image for each iteration is from fixed camera position
            mainCamera.transform.position = cam0Trans;
            mainCamera.transform.rotation = Quaternion.Euler(cam0Rot);
            count++;
            return;
        }
        else if (count == 1)
        {
            mainCamera.transform.position = cam1Trans;
            mainCamera.transform.rotation = Quaternion.Euler(cam1Rot);
            count++;
            return;
        }
        else if (count == 2)
        {
            mainCamera.transform.position = cam2Trans;
            mainCamera.transform.rotation = Quaternion.Euler(cam2Rot);
            count++;
            return;
        }
        count++;
    }

    protected override void OnIterationEnd()
    {
        count = 0;
    }
}