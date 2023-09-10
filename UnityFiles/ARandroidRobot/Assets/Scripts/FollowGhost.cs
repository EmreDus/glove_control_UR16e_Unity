using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WiFiReceiver;

public class FollowGhost : MonoBehaviour
{
    public float moveSpeed = 0.3f; // Adjust this to control rotation speed
    private bool isMoving = false;

    public Transform ghostTarget;
    public Transform mainTarget;

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 52;

        if (GUI.Button(new Rect(40, 135, 300, 100), isMoving ? "Stop" : "Go", buttonStyle))
        {
            isMoving = !isMoving;
        }
    }

    private void Update()
    {
        if (fng1>45 & fng2>45 & fng3>45 & fng4>45 & fng5>45)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        
        
        if (isMoving)
        {
            Vector3 targetPosition = ghostTarget.position;
            Quaternion targetRotation = ghostTarget.rotation;

            // Calculate the interpolation factor based on your moveSpeed and Time.deltaTime
            float t = Mathf.Clamp01(moveSpeed * Time.deltaTime);

            // Interpolate the position and rotation
            mainTarget.transform.position = Vector3.Lerp(mainTarget.position, targetPosition, t);
            mainTarget.transform.rotation = Quaternion.Slerp(mainTarget.rotation, targetRotation, t);

        }  
    }
}
