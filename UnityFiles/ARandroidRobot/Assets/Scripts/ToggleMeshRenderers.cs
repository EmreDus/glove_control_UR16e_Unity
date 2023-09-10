using UnityEngine;

public class ToggleMeshRenderers : MonoBehaviour
{
    public bool enableMeshRenderers = true;

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 36;
        
        if (GUI.Button(new Rect(900, 40, 150, 70), enableMeshRenderers ? "Hide" : "Show", buttonStyle))
        {
            enableMeshRenderers = !enableMeshRenderers;
        }
    }

    void Update()
    {
        // Toggle mesh renderers on or off based on the enableMeshRenderers flag
        ToggleMeshRenderersRecursively(transform, enableMeshRenderers);
    }

    void ToggleMeshRenderersRecursively(Transform parent, bool enable)
    {
        foreach (Transform child in parent)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = enable;
            }

            // Recursively process child objects
            ToggleMeshRenderersRecursively(child, enable);
        }
    }
}
