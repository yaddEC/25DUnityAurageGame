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

        if (GUILayout.Button("Destroy Last NodeHolder"))
            builder.DestroyNodeHolder();

        if (GUILayout.Button("Destroy All"))
            builder.DestroyAll();
    }
}