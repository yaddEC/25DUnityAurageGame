using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocketStation : MonoBehaviour
{
    [Header("Reference")]
    private PowerManager refPowerManager;
    public Socket refSocket;

    [Header("Lamp UI/UX")]
    private MeshRenderer meshRenderer;

    [Header("Lamp Stats")]
    public bool levelFinished = false;

    private void Awake()
    {
        refPowerManager = GameObject.FindObjectOfType<PowerManager>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = refSocket.machineMaterials[1];
    }

    private void Update()
    {
        if (levelFinished)
            FinishHandler();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            levelFinished = true;
        }
    }

    private void FinishHandler()
    {
        Debug.Log("go To next level");
        //SceneManager.LoadScene("LevelTwo");
    }
}
