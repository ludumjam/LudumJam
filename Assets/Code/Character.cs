using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public GameObject[] children;
    private int childIndex = 0;
    private GameObject currentChild;

    private enum Shift {Next, Previous};

    void Start()
    {
        currentChild = children[0];
        currentChild.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ShapeShift(Shift.Previous);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ShapeShift(Shift.Next);
        }
    }

    private void ShapeShift(Shift direction)
    {
        GameObject previousChild = currentChild;
        Vector3 newPosition = previousChild.transform.position;
        previousChild.SetActive(false);
        currentChild = (direction == Shift.Previous) ? GetPreviousChild() : GetNextChild();
        currentChild.transform.position = newPosition;
        currentChild.GetComponent<Rigidbody>().velocity = previousChild.GetComponent<Rigidbody>().velocity;
        currentChild.SetActive(true);
    }

    private GameObject GetNextChild()
    {
        childIndex++;
        if (childIndex >= children.Length)
        {
            childIndex = 0;
        }
        return children[childIndex];
    }

    private GameObject GetPreviousChild()
    {
        childIndex--;
        if (childIndex < 0)
        {
            childIndex = children.Length - 1;
        }
        return children[childIndex];
    }
}
