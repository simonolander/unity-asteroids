using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour, IAsteroidCollider {
	public float speed;

	void Start () {
		GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().transform.forward * speed;
	}

	public void Collide() {

	}
}
