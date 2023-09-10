using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform QRreferenceOrientation;

    private void Update()
    {
        transform.rotation = QRreferenceOrientation.rotation;
    }
}
