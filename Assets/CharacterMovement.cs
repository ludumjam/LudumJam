using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour {

	public float forceModifier = 0.5f;
	private new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.AddForce (Vector3.right * Input.GetAxis ("Horizontal") * forceModifier, ForceMode.Impulse);
	}
}
