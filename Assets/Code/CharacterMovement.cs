using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

	public float forceModifier = 0.5f;
	private new Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.AddForce (Vector2.right * Input.GetAxis ("Horizontal") * forceModifier, ForceMode2D.Impulse);
	}
}
