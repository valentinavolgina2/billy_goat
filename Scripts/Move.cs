using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform[] targets;
    public float speed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targets[index].position) < 0.01f)
        {
            index++;
            if (index == targets.Length)
            {
                index = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targets[index].position, speed * Time.deltaTime);
    }
}
