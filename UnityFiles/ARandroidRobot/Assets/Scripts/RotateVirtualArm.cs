using UnityEngine;
using static WiFiReceiver;

public class RotateVirtualArm : MonoBehaviour
{
    public Transform upperArmCube;
    public Transform lowerArmCube;
    public Transform handCube;

    private Vector3 upperArmOffset = Vector3.zero;
    private Vector3 lowerArmOffset = Vector3.zero;
    private Vector3 handOffset = Vector3.zero;

    public float upperSensitivity = 1.0f;
    public float lowerSensitivity = 1.0f;
    public float handSensitivity = 1.0f;

    public Transform refOrientation;

    private bool isEnable = false;
    private void OnGUI() //WiFiReceiver On and OFF
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 36;
        
        if (GUI.Button(new Rect(750, 40, 150, 70), isEnable ? "ON" : "OFF", buttonStyle))
        {
            isEnable = !isEnable;

            WiFiReceiver script = GetComponent<WiFiReceiver>();
            script.enabled = isEnable;
        }
    }

    void Update()
    {
        upperArmOffset = new Vector3(90.0f+refOrientation.eulerAngles.x, refOrientation.eulerAngles.y, refOrientation.eulerAngles.z);
        lowerArmOffset = new Vector3(90.0f+refOrientation.eulerAngles.x, refOrientation.eulerAngles.y, refOrientation.eulerAngles.z);
        handOffset = new Vector3(90.0f+refOrientation.eulerAngles.x, refOrientation.eulerAngles.y, refOrientation.eulerAngles.z);

        Quaternion lowerArmRotation = Quaternion.Euler(lowerArmOffset) * new Quaternion(-qy1, qx1, qz1, qw1);
        Quaternion upperArmRotation = Quaternion.Euler(upperArmOffset) * new Quaternion(-qy2, qx2, qz2, qw2);
        Quaternion handRotation = Quaternion.Euler(handOffset) * new Quaternion(qy3, -qx3, qz3, qw3);

        // Apply the rotation sensitivity
        lowerArmRotation = Quaternion.Lerp(lowerArmCube.rotation, lowerArmRotation, Time.deltaTime * lowerSensitivity);
        upperArmRotation = Quaternion.Lerp(upperArmCube.rotation, upperArmRotation, Time.deltaTime * upperSensitivity);
        handRotation = Quaternion.Lerp(handCube.rotation, handRotation, Time.deltaTime * handSensitivity);

        // Apply the new rotation to the objects
        lowerArmCube.rotation = lowerArmRotation;
        upperArmCube.rotation = upperArmRotation;
        handCube.rotation = handRotation;
    }
}
