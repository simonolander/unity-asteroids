using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public float tumble;
	public Vector3 velocity;

	public GameObject explosion;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		rb.velocity = velocity;
	}
	
	void OnTriggerEnter(Collider other) {
		IAsteroidCollider collider = other.GetComponent<IAsteroidCollider> ();
		if (collider != null) {
			Destroy (gameObject);
			Destroy (other.gameObject);
			collider.Collide ();
			Instantiate (explosion, rb.transform.position, rb.transform.rotation);
		} else if (other.CompareTag ("Asteroid")) {
			Destroy (gameObject);
			Instantiate (explosion, rb.transform.position, rb.transform.rotation);
		}
	}
}
