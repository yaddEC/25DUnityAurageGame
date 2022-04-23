using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioStation : Station
{
    private GameObject radioZoneClone;
    private GameObject radioZone;
    public bool isRadioActive = false;

    private void Awake()
    {
        var t = gameObject.name; tagToSearch = t;
        radioZone = Resources.Load<GameObject>("Prefabs/RadioDetection");
    }
    private void Update()
    {
        CooldownHandler();
    }

    public void TurnRadioOn()
    {
        if(!isRadioActive)
        {
            radioZoneClone = Instantiate(radioZone, transform.position, Quaternion.identity);
            isRadioActive = true;
        }
    }
    public IEnumerator TurnRadioOff()
    {
        radioZoneClone.GetComponent<RadioZone>().isActive = false;
           
        yield return new WaitForSeconds(5);
        radioZoneClone.GetComponent<RadioZone>().isActive = true;
    
        yield return new WaitForSeconds(1f);
        Destroy(radioZoneClone);
        isRadioActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) EnterMachine(this);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            StayMachine(autoExec = false);
            if (isUsable && InputManager.performB) doEvent = true;
            else doEvent = false;
            if (doEvent) TurnRadioOn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") ExitMachine();
    }
}


