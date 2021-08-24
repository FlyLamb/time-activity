
/*
    A quick script that just draws a mesh gizmo for a gameobject, cool for level design, useful for showcasing spawners in the editor
    use it however you want, it's too short to have a license, no credit required just yoink it and place it in your codebase
    made by bajtixone (https://bajtix.xyz)
*/

using UnityEngine;

public class QGizmo : MonoBehaviour {
    [SerializeField] private Mesh mesh;
    
    [SerializeField] private Color gizmoColor = Color.green;

    void OnDrawGizmos() {
        if(mesh == null) return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawMesh(mesh, 0, transform.position, transform.rotation, transform.localScale);
    }
}