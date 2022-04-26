using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioStation : Station
{
    private GameObject radioZoneClone;
    private GameObject radioZone;
    public bool isRadioActive = false;
    [HideInInspector] public  Animator animator;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        var t = gameObject.name; tagToSearch = t;
        radioZone = Resources.Load<GameObject>("Prefabs/Setup/RadioDetection");
    }

    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(special = false);
    }
    public void TurnRadioOn()
    {
        if(!isRadioActive)
        {
            animator.SetBool("isOn", true);
            radioZoneClone = Instantiate(radioZone, transform.position, Quaternion.identity);
            isRadioActive = true;
        }
    }
    public IEnumerator TurnRadioOff()
    {
        radioZoneClone.GetComponent<RadioZone>().isActive = false;
           
        yield return new WaitForSeconds(5);
        radioZoneClone.GetComponent<RadioZone>().isActive = true;
        animator.SetBool("isOn", false);
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
            if (doEvent)
            {
                TurnRadioOn();
                cooldown += 5;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") ExitMachine();
    }
}


