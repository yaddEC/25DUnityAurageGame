using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeBuilder))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NodeBuilder builder = (NodeBuilder)target;

        if(GUILayout.Button("Create NodeHolder"))
            builder.BuildNodeHolder();

        if (GUILayout.Button("Destroy Last NodeHolder"))
            builder.DestroyLastNodeHolder();

        if (GUILayout.Button("Destroy All NodeHolders"))
            builder.DestroyAllNodeHolders();
    }
}