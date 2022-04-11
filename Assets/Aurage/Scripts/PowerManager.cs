using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerManager : MonoBehaviour
{
    private GameObject refPlayer;
    private GeneratorStation refGeneratorStation;

    [Header("Charging Stats")]
    public float unchargePowerDelta;
    public bool isCharging = false;

    public bool canLoosePower = true;

    [Header("Player Stats")]
    public bool outOfPower;
    public float maxPower;
    public float currentPower;
    public Transform waypoint;


    private void Awake()
    {
        refPlayer = GameObject.FindGameObjectWithTag("Player");
        refGeneratorStation = GameObject.FindObjectOfType<GeneratorStation>();

        currentPower = maxPower;
    }

    private void Update()
    {
        if (outOfPower || KillZone.isDead)
            /*StartCoroutine(*/
            OnOutOfPowerEvent()/*)*/;
    }

    private void OnOutOfPowerEvent()
    {
        /*refPlayer.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(2f);

        refPlayer.GetComponent<MeshRenderer>().enabled = true;
        currentPower = maxPower;
        outOfPower = false;

        refPlayer.transform.position = waypoint.position;
        refGeneratorStation.RestoreGeneratorStateEvent();*/

        SceneManager.LoadScene("GameOverScreen");
    }
}

