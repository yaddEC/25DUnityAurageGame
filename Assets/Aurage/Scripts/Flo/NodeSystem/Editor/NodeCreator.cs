using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeHolder))]
public class NodeCreator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NodeHolder builder = (NodeHolder)target;

        if (GUILayout.Button("Create Node"))
            builder.CreateNode();

        if (GUILayout.Button("Destroy Last Node"))
            builder.DestroyLastNode();

        if (GUILayout.Button("Destroy All Nodes"))
            builder.DestroyAllNodes();
    }
}