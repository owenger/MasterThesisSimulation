using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float xMin = 0f;
    [SerializeField] private float zMin = -12f;
    [SerializeField] private float xMax = 20f;
    [SerializeField] private float zMax = 12f;

    Animator animator;
    Vector3 startPos;
    Vector3 curPos;
    Vector3 targetPos;
    // state: 0: idle, 1: walking, 2: running
    int state = 1;

    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        animator = GetComponent<Animator>();

        var startX = Random.Range(xMin, xMax);
        var startZ = Random.Range(zMin, zMax);

        Vector3 positionStart = new Vector3(startX, 0, startZ);
        startPos = positionStart;
        curPos = positionStart;

        transform.position = positionStart;
        Debug.Log("Position0");

        setPose();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("CurrentState", state);

        curPos = transform.position;

        if(Time.time - startTime > 3 && state == 0)
        {
            transform.LookAt(targetPos);
            state = 1;
        }
        else if(state == 1)
        {
            //transform.Translate(Vector3.forward * Time.deltaTime);

            if((targetPos-curPos).magnitude < 2)
            {
                state = 0;
                startPos = curPos;
                setPose();
                startTime = Time.time;
            }
            else
            {
                transform.LookAt(targetPos);
            }
        }
    }

    void setPose()
    {
        startTime = Time.time;
        var targetX = Random.Range(xMin, xMax);
        var targetZ = Random.Range(zMin, zMax);

        Vector3 positionTarget = new Vector3(targetX, 0, targetZ);
        targetPos = positionTarget;
    }
}
