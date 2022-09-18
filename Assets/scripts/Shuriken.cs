using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {
	public float mov = 8F;
	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	void Update () {
		float x = transform.position.x + (Time.deltaTime * mov);
		float y = transform.position.y;
		float z = transform.position.z;
		transform.position = new Vector3 (x, y, z);
	}
	void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag != "Player") {
		    Destroy (gameObject);
        }
	}
}
