using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public float speed = 5f;
    
    void Update () {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        float multiplier = speed * Time.deltaTime;

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x + multiplier * h, transform.position.y, transform.position.z + multiplier * v),
            Time.deltaTime * 10
        );

    }

}
