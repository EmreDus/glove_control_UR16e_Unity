using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetControl : MonoBehaviour
{
    public Transform gloveTarget; //Reference to the hand GameObject

    private float rotationSpeed = 3f;
    private float moveSpeed = 0.05f;

    private Vector3 homePosition = new Vector3(0, 3f, 2f);
    private Vector3 homeRotation = new Vector3(0, 0, 0);

    private bool isGloveControl = false; // For toggle between glove and touch control
    private bool isXZmove = true; // For toggle between X-Z and X-Y touch control

    private void Start()
    {
        transform.position = homePosition;
        transform.eulerAngles = homeRotation;
    }

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 52;
        
        if (GUI.Button(new Rect(40, 35, 300, 100), isGloveControl ? "GLOVE" : "TOUCH", buttonStyle))
        {
            isGloveControl = !isGloveControl;
        }
    }

    private void Update()
    {
        if(isGloveControl)
        {
            gloveControl();
        }
        else
        {
            touchControl();
        }
    }

    private void touchControl()
    {
        //Debug.Log("touch control");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // Single touch operation
            if (Input.touchCount == 1) // Transform position target object X-Z and X-Y axis
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if(isXZmove)
                    {
                        float moveX = touch.deltaPosition.x * moveSpeed * Time.deltaTime;
                        float moveY = touch.deltaPosition.y * moveSpeed * Time.deltaTime;

                        transform.Translate(moveX, 0f, moveY);
                    }
                    else
                    {
                        float moveX = touch.deltaPosition.x * moveSpeed * Time.deltaTime;
                        float moveY = touch.deltaPosition.y * moveSpeed * Time.deltaTime;

                        transform.Translate(moveX, moveY, 0f);
                    }  
                }
            }

            // Double touch operation
            else if (Input.touchCount == 2) // Rotating target object
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

            // Triple touch operation
            else if (Input.touchCount == 3) 
            {
                isXZmove = !isXZmove; // Changing move control axis
            }
        }
    }

    private void gloveControl()
    {
        if (gloveTarget != null) // Check if the virtual hand reference is assigned
        {
            //Debug.Log("glove control");
            // Get the position of the virtual hand
            Vector3 targetPosition = gloveTarget.position;
            Quaternion targetRotation = gloveTarget.rotation;

            // Move the cube to the position of the virtual hand
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
            
        else
        {
            Debug.LogError("gloveTarget reference is not assigned! Please assign the gloveTarget in the Inspector.");
        }
    }
}
