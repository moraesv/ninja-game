using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour {
    private Rigidbody2D rig;
    public float speed;
    public float jumpForce;
	private bool pulando = false;
	public bool atacando = false;
	private Animator animator;
	public Transform camera;

	private AudioSource somPulo;
	private AudioSource somMoeda;
	
	public GameObject shuriken;

	private int pontos = 0;
	public TextMeshProUGUI  txtMoeda;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		//definir a posição inicial da câmera
		camera.position =  new Vector3(-1.0f, -1.0f, -10.0f);
		//instancia o som
		// somPulo = GetComponents<AudioSource>()[1];
		// somMoeda = GetComponents<AudioSource>()[2];
    }
    
    void Update()
    {
        float mov = Input.GetAxisRaw("Horizontal");
		if (mov == 1) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (mov == -1) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}

        rig.velocity = new Vector2(mov * speed, rig.velocity.y);
		animator.SetFloat ("Velocidade", Mathf.Abs (mov));

        if (Input.GetKeyDown(KeyCode.UpArrow) && pulando == false) {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			pulando = true;
			animator.SetBool ("Pulando", true);
			// somPulo.Play();
		}
        
        if (Input.GetKeyDown(KeyCode.Space) && atacando == false) {
			atacando = true;
			animator.SetBool ("Atacando", true);

            Invoke("PararAtaque", 1F);
			// somPulo.Play();
		}

        

		// shuriken
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
            float fx;
            float movShuriken;
			if (GetComponent<SpriteRenderer>().flipX) {
                movShuriken = -8F;
                fx = rig.transform.position.x - 1.0F; 
            } else {
                movShuriken = 8F;
                fx = rig.transform.position.x + 1.0F; 
            }    

            float fy = rig.transform.position.y-0.3F;
            float fz = rig.transform.position.z;

            GameObject novo = Instantiate(shuriken, new Vector3(fx, fy, fz), Quaternion.identity);
            novo.GetComponent<Shuriken>().mov = movShuriken;
        }

		float camx = rig.transform.position.x + 3;
		if (camx < -1.0f) {
			camx = -1.0f;
		}
		if (camx > 37.29f) {
			camx = 37.29f;
		}
		camera.position = new Vector3 (camx, -1.0f, -10.0f);
    }

    void PararAtaque() {
        atacando = false;
        animator.SetBool ("Atacando", false);
    }

	void OnCollisionEnter2D(Collision2D coll) {
		pulando = false;
		animator.SetBool ("Pulando", false);
	}

	void OnTriggerEnter2D(Collider2D coll) {

		// if (coll.gameObject.tag == "Finish") {
		// 	//muda de fase
		// 	 SceneManager.LoadScene("jogo2d 2", LoadSceneMode.Single);
		// }

		// somMoeda.Play ();
		// Destroy (coll.gameObject);
		// pontos++;
		// //exibir os pontos na HUD
		// txtMoeda.text = ""+pontos;
	}
}