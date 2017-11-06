using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speedMultipier = 2f;
    public float maxSpeed = 25f;
    public float rotationMultiplier = 0.5f;
    public float speed = 4f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool speedUp = Input.GetKey(KeyCode.W);
        bool speedDown = Input.GetKey(KeyCode.S);
        float rotation = 0f;
        float moveHorizontal = Input.GetAxis("Mouse X");
        float moveVertical = Input.GetAxis("Mouse Y");

        if (speedUp)
            speed += speedMultipier;
        else if (speedDown)
            speed -= speedMultipier;

        if (speed < 0)
            speed = 0;
        else if (speed >= maxSpeed)
            speed = maxSpeed;

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, speed);

        rb.AddForce(movement);

        if (Input.GetKey(KeyCode.E))
            rotation += rotationMultiplier;
        else if (Input.GetKey(KeyCode.Q))
            rotation -= rotationMultiplier;

        transform.Rotate(new Vector3(moveVertical, moveHorizontal, rotation) * Time.deltaTime * speed);
    }
}
