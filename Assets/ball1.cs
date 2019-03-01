using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 1f;
        float height = 1.2f;

        Vector3 pos = transform.position;
        float newY = (Mathf.Sin(Time.time * speed) + 1) * height;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
