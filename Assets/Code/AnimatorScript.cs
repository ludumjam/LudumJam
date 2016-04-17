using UnityEngine;
using System.Collections;

public class AnimatorScript : MonoBehaviour {

	public AudioClip collision;
	Animator animator;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		//float h = Input.GetAxis("Horizontal");
		//float v = Input.GetAxis("speed");
		float speed = Character.currentChild.GetComponent<Rigidbody2D>().velocity.y;
		bool fire = Input.GetButtonDown("Jump");

		//animator.SetFloat("speed",v);
		//animator.SetFloat("Strafe",h);

		animator.SetFloat("speed",speed);
		animator.SetBool("ability", fire);
	}

	void OnCollisionEnter2D(Collision2D col) {
		
		animator.SetTrigger("collision");
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = collision;
		audio.PlayOneShot(collision, 0.4F);




	}
}
