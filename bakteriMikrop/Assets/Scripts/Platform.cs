using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform platform;
    public Transform[] points;

    public float speed;
    Vector3 current;
    // Start is called before the first frame update
    void Start()
    {
        current = points[0].position;
    }
    int index = 0;

    void Update()
    {
        if (Vector3.Distance(platform.position,current)>0.1f)
        {
            platform.position = Vector3.MoveTowards(platform.position,current,Time.deltaTime*speed);
        }
        else
        {
            index++;
            current = points[index % points.Length].position;
        }
    }
}
