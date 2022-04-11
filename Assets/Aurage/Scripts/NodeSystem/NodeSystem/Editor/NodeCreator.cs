using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeReference))]
public class NodeCreator : Editor
{
    public override void OnInspectorGUI()
    {
        //NodeReference.node = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Aurage/Scripts/NodeSystem/Objects/Node.prefab", typeof(GameObject));

        base.OnInspectorGUI();
        NodeReference builder = (NodeReference)target;

        if (GUILayout.Button("Create Node"))
            builder.CreateNode();

        if (GUILayout.Button("Destroy Last Node"))
            builder.DestroyLastNode();

        if (GUILayout.Button("Destroy All Nodes"))
            builder.DestroyAllNodes();
    }
}