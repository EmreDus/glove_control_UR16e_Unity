using UnityEngine;

public class gloveControl : MonoBehaviour
{
    public Transform gloveTarget; // Reference to the hand GameObject

    private void Update()
    {
        if (gloveTarget != null) // Check if the sphere reference is assigned
        {
            // Get the position of the sphere
            Vector3 targetPosition = gloveTarget.position;
            Quaternion targetRotation = gloveTarget.rotation;

            // Move the cube to the position of the sphere
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
            
        else
        {
            Debug.LogError("gloveTarget reference is not assigned! Please assign the gloveTarget in the Inspector.");
        }
    }
}
