using UnityEngine;

public class touchControl : MonoBehaviour
{
    private float rotationSpeed = 3f;
    private float moveSpeed = 0.05f;

    private Vector3 homePosition = new Vector3(0, 3f, 2f);
    private Vector3 homeRotation = new Vector3(0, 0, 0);

    private bool controlMode = false; // False for Touch Control

    void Start()
    {
        transform.position = homePosition;
        transform.eulerAngles = homeRotation;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Toggle Control Mode
            if (Input.touchCount == 3)
            {
                controlMode = !controlMode;

                if (controlMode)
                {
                    homeReset();
                }
            }
            
            if (!controlMode)
            {
                TouchControl(touch);
            }
        }
    }

    void TouchControl(Touch touch)
    {
         // Single touch operation
        if (Input.touchCount == 1)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                float moveX = touch.deltaPosition.x * moveSpeed * Time.deltaTime;
                float moveY = touch.deltaPosition.y * moveSpeed * Time.deltaTime;

                transform.Translate(moveX, 0f, moveY);
            }
        }

        // Double touch operation
        else if (Input.touchCount == 2)
        {
            Vector2 touch0PrevPos = touch.position - touch.deltaPosition;
            Vector2 touch1PrevPos = touch0PrevPos - touch.deltaPosition;

            Vector2 prevTouchDelta = touch0PrevPos - touch1PrevPos;
            Vector2 touchDelta = touch.position - touch0PrevPos;

            float rotationAmount = (touchDelta.magnitude - prevTouchDelta.magnitude) * rotationSpeed * Time.deltaTime;

            float rotationX = -touchDelta.y * rotationSpeed * Time.deltaTime;
            float rotationY = touchDelta.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(rotationX, rotationY, rotationAmount);
        }
    }

    void homeReset()
    {
        transform.position = homePosition;
        transform.eulerAngles = homeRotation;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Input.touchCount == 3)
            {
                TouchControl(touch);
            }
        }
    }
}
