using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float minX, maxX, minZ, maxZ;
}

public class Player : MonoBehaviour, IAsteroidCollider {
	public float speed;
	public float tilt;
	public Boundary boundary;

	public Bolt bolt;
	public Transform shotSpawn;
	public float fireRate;

	public GameObject explosion;

	public GameController gameController;

	private Rigidbody rb;
	private AudioSource audioSource;
	private float nextFireTime;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 velocity = new Vector3 (moveHorizontal, 0, moveVertical) * speed;

		rb.velocity = velocity;
		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.minX, boundary.maxX),
			0,
			Mathf.Clamp(rb.position.z, boundary.minZ, boundary.maxZ)
		);

		rb.rotation = Quaternion.Euler (
			0,
			0,
			-velocity.x / speed * tilt
		);
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFireTime) {
			Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
			nextFireTime = Time.time + fireRate;
			audioSource.Play ();
		}
	}

	public void Collide() {
		Instantiate (explosion, rb.transform.position, rb.transform.rotation);
		gameController.OnGameOver ();
	}
}
