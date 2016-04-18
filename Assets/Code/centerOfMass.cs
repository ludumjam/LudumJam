using UnityEngine;
using System.Collections;

public class centerOfMass : MonoBehaviour {
	public Vector2 offset;
	public Rigidbody2D rb;
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		rb.centerOfMass = offset;
	}
}