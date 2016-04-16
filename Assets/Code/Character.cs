using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public GameObject[] children;
    private int childIndex = 0;
    private GameObject currentChild;

    void Start()
    {
        currentChild = children[0];
        currentChild.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            GameObject previousChild = children[childIndex];
            Vector3 newPosition = previousChild.transform.position;
            previousChild.SetActive(false);
            childIndex = ++childIndex % children.Length;
            currentChild = children[childIndex];
            currentChild.transform.position = newPosition;
            currentChild.GetComponent<Rigidbody>().velocity = previousChild.GetComponent<Rigidbody>().velocity;
            currentChild.SetActive(true);
        }
    }
}
