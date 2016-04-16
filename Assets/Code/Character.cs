using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public GameObject[] children;
    private int childIndex = 0;
    public static GameObject currentChild;
    public static float greatestDistanceAchieved = 0f;

    private enum Shift {Next, Previous};

    void Start()
    {
        currentChild = children[0];
        currentChild.SetActive(true);
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        greatestDistanceAchieved = 0f;
    }

    void HandleOnDeathEvent ()
    {
        enabled = false;
    }

    void OnDestroy()
    {
        CameraFollow.OnPlayerWentOutsideScreen -= HandleOnDeathEvent;
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

    void FixedUpdate()
    {
        greatestDistanceAchieved = Mathf.Max(Mathf.Abs(currentChild.transform.position.y), greatestDistanceAchieved);
    }

    private void ShapeShift(Shift direction)
    {
        GameObject previousChild = currentChild;
        Vector3 newPosition = previousChild.transform.position;
        Vector2 newVelocity = previousChild.GetComponent<Rigidbody2D>().velocity;
        previousChild.SetActive(false);
        currentChild = (direction == Shift.Previous) ? GetPreviousChild() : GetNextChild();
        currentChild.transform.position = newPosition;
        currentChild.SetActive(true);
        currentChild.GetComponent<Rigidbody2D>().velocity = newVelocity;
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
