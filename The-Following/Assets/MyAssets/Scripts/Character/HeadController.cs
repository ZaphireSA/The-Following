using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

    Rigidbody rb;
    [SerializeField] float rollSpeed = 5f;
    Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            velocity.z = 1;
        else if (Input.GetKey(KeyCode.S))
            velocity.z = -1;
        if (Input.GetKey(KeyCode.A))
            velocity.x = -1;
        if (Input.GetKey(KeyCode.D))
            velocity.x = 1;

        rb.AddForce(velocity * rollSpeed * Time.deltaTime);
        Debug.Log(velocity);
        velocity = Vector3.zero;
    }
}
