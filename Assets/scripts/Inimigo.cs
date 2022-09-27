using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {
	private Rigidbody2D rig;
	public float mov = 1F;
	public bool atacando = false;
	private Animator animator;

	private AudioSource somEspada;

	void Start () {
		rig = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		somEspada = GetComponents<AudioSource>()[0];
	}
	
	void Update () {
		if (mov > 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else {
			GetComponent<SpriteRenderer> ().flipX = true;
		}
		rig.velocity = new Vector2 (mov, rig.velocity.y);
		animator.SetFloat ("Velocidade", Mathf.Abs (mov));
	}
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<Jogador>().atacando == true) {
				Destroy (gameObject); //NPC morre
			} else {
				atacando = true;
        		animator.SetBool ("Atacando", true);
				somEspada.Play();
				Invoke("PararAtaque", 0.2F);
				Destroy (col.gameObject); //NPC mata
			}	
		} else if (col.gameObject.tag == "Shuriken") {
			Destroy (gameObject); //NPC morre
		} else {
			mov = mov * -1; //NPC muda de direção
		}
	}
	void PararAtaque() {
        atacando = false;
        animator.SetBool ("Atacando", false);
    }
}