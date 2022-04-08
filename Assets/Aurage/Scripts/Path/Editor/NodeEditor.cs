using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeBuilder))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NodeBuilder builder = (NodeBuilder)target;

        if(GUILayout.Button("Build NodeHolder"))
            builder.BuildNodeHolder();

        if (GUILayout.Button("Remove NodeHolder"))
            builder.RemoveNodeHolder();

        if (GUILayout.Button("Reset Editor"))
            builder.ResetAllNodeHolders();
    }
}