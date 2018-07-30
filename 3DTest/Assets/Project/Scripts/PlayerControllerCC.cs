using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCC : MonoBehaviour {

	public Vector3 velocity;
	public float speed = 10f;
	public float jumpHeight= 1f;
	public float GroundDistance = 0.2f;
	public LayerMask Ground;
	public bool isGrounded;

	private CharacterController myController;
	private Transform groundChecker;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentspeed;
	float velocityY;


	void Start()
	{
		myController = GetComponent<CharacterController>();
		groundChecker = transform.GetChild(0);
	}

	void Update()
	{
		isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		currentspeed = Mathf.SmoothDamp (currentspeed, speed, ref speedSmoothVelocity, speedSmoothTime);

		if (move != Vector3.zero) {
			transform.forward = move;
		}

		velocityY += Time.deltaTime * Physics.gravity.y;

		velocity = move * currentspeed + Vector3.up * velocityY;

		myController.Move(velocity * Time.deltaTime);


		if (isGrounded && velocityY < 0) {
			velocityY = 0f;
		}

		if (isGrounded && Input.GetKeyDown (KeyCode.Space)) {
			velocityY = Mathf.Sqrt (-2 * Physics.gravity.y * jumpHeight);
		}
	}
}
