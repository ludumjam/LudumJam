using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public float gravity = 10f;
    public float controlForce = 20f;

    void Start() {
        Physics2D.gravity = new Vector2(0, -gravity);
    }
	
	void Update () {

        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");

        float force = 0;

        if (left && !right) {
            force = -controlForce;
        }

        if (!left && right) {
            force = controlForce;
        }

        Physics2D.gravity = new Vector2(force, -gravity);

    }
}
