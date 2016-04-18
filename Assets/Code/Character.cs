using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public GameObject[] children;
    private int childIndex = 0;
    public static GameObject currentChild;
    public static int greatestDistanceAchieved = 0;

    private enum Shift {Next, Previous};

    public delegate void ShapeShiftEvent(int index, int previousIndex);
    public static ShapeShiftEvent OnPlayerShapeShift;

    void Start()
    {
        currentChild = children[0];
        ToggleCharacter(currentChild, true);
        CameraFollow.OnPlayerWentOutsideScreen += HandleOnDeathEvent;
        greatestDistanceAchieved = 0;
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

        if (Input.GetKey(KeyCode.Space))
        {
            // Trigger ability
            currentChild.GetComponent<ISpecialAbility>().TriggerAbility();
        }
    }

    void FixedUpdate()
    {
        greatestDistanceAchieved = (int) Mathf.Max(Mathf.Abs(currentChild.transform.position.y), greatestDistanceAchieved);
    }

    private void ShapeShift(Shift direction)
    {
        GameObject previousChild = currentChild;
        Vector3 newPosition = previousChild.transform.position;
        Vector2 newVelocity = previousChild.GetComponent<Rigidbody2D>().velocity;
        ToggleCharacter(previousChild, false);
        int previousIndex = childIndex;
        currentChild = (direction == Shift.Previous) ? GetPreviousChild() : GetNextChild();
        currentChild.transform.position = newPosition;
        ToggleCharacter(currentChild, true);
        currentChild.GetComponent<Rigidbody2D>().velocity = newVelocity;

        if (OnPlayerShapeShift != null)
        {
            OnPlayerShapeShift(childIndex, previousIndex);
        }
    }

    private void ToggleCollider(GameObject target, bool newState)
    {
        BoxCollider2D boxCollider = target.GetComponent<BoxCollider2D>();
        CircleCollider2D circleCollider = target.GetComponent<CircleCollider2D>();
        PolygonCollider2D polygonCollider = target.GetComponent<PolygonCollider2D>();

        if (boxCollider != null)
        {
            boxCollider.enabled = newState;
        }
        if (circleCollider != null)
        {
            circleCollider.enabled = newState;
        }
        if (polygonCollider != null)
        {
            polygonCollider.enabled = newState;
        }
    }

    private void ToggleCharacter(GameObject target, bool newState)
    {
        target.GetComponent<SpriteRenderer>().enabled = newState;
        ToggleCollider(target, newState);
        target.transform.FindChild("shapeBG").GetComponent<SpriteRenderer>().enabled = newState;
        Transform laser = target.transform.FindChild("Laser");
        if (laser != null)
        {
            laser.GetComponent<SpriteRenderer>().enabled = newState;
        }
        if (newState)
        {
            target.transform.FindChild("Trail").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            target.transform.FindChild("Trail").GetComponent<ParticleSystem>().Stop();
        }
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
