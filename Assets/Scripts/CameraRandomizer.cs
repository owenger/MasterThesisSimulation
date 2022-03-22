using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/Camera Randomizer")]
public class CameraRandomizer : Randomizer
{
    public Camera mainCamera;
    public FloatParameter cameraXRotation;
    public FloatParameter cameraYRotation;
    public FloatParameter cameraDistance;
    public FloatParameter cameraTransformX;
    public FloatParameter cameraTransformZ;
    private int count = 0;

    //private Vector3 initialCameraPosition = new Vector3(-14, 6, 0f);
    private Vector3 initialCameraPosition = new Vector3(-8, 8, 0f);
    //private Quaternion initialCameraRotation = Quaternion.Euler(12, 90, 0f);
    private Quaternion initialCameraRotation = Quaternion.Euler(45, 90, 0f);

    protected override void OnUpdate()
    {
        float tilt; float pan;
        if (count == 0)
        {
            // first image for each iteration is from fixed camera position
            mainCamera.transform.position = initialCameraPosition;
            mainCamera.transform.rotation = initialCameraRotation;
            count++;
            return;
        }
        else
        {
            pan = cameraYRotation.Sample();
            tilt = cameraXRotation.Sample();
        }
        
        var distance = cameraDistance.Sample();

        var x = - distance * Mathf.Sin(pan * Mathf.PI / 180) * Mathf.Cos(tilt * Mathf.PI / 180);
        var y = distance * Mathf.Sin(tilt * Mathf.PI / 180);
        var z = - Mathf.Sqrt(distance * distance - x * x - y * y);

        //Debug.Log($"d: {distance}, x: {x}, y: {y}, z: {z}, pan: {pan}, tilt: {tilt}");

        mainCamera.transform.rotation = Quaternion.Euler(tilt, pan, 0f);

        // transform camera by random numbers x and y
        x = x + cameraTransformX.Sample();
        z = z + cameraTransformZ.Sample();

        mainCamera.transform.position = new Vector3(x, y, z);
        count++;
    }

    protected override void OnIterationEnd()
    {
        count = 0;
    }

    /*
    protected override void OnIterationStart()
    {
        float tilt; float pan;
        if (count == 0)
        {
            tilt = 20;
            pan = 0;
            count++;
        }
        else
        {
            pan = cameraYRotation.Sample();
            tilt = cameraXRotation.Sample();
        }
        
        var distance = cameraDistance.Sample();

        var x = - distance * Mathf.Sin(pan * Mathf.PI / 180) * Mathf.Cos(tilt * Mathf.PI / 180);
        var y = distance * Mathf.Sin(tilt * Mathf.PI / 180);
        var z = - Mathf.Sqrt(distance * distance - x * x - y * y);

        Debug.Log($"d: {distance}, x: {x}, y: {y}, z: {z}, pan: {pan}, tilt: {tilt}");
        

        mainCamera.transform.rotation = Quaternion.Euler(tilt, pan, 0f);
        mainCamera.transform.position = new Vector3(x, y, z);
        count++;
    }
    */
}