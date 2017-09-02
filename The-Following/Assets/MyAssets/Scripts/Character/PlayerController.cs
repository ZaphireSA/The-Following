using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController controller;
    [SerializeField]
    float rollSpeed = 5f;
    Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Vector3.zero;
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontal, 0, vertical);
        controller.SimpleMove(moveDirection * rollSpeed * Time.deltaTime);
        velocity = Vector3.zero;
    }
}
