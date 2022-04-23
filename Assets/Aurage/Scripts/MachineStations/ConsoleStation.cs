using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleStation : Station
{
    [Header("Property Info")]
    public bool isOpen = false;
    public GameObject objectToInteractWith;
    public Animator objectAnim;
    public BoxCollider objectBC;
    //-------------------------------------------------------------
    private void Awake() 
    {
        var t = transform.parent.gameObject.name; tagToSearch = t;
        objectToInteractWith = UnityFinder.FindGameObjectInChildWithTag(transform.parent.gameObject, transform.parent.GetComponentInChildren<Animator>().gameObject.tag);
        objectAnim = objectToInteractWith.GetComponent<Animator>();
        objectBC = objectToInteractWith.GetComponent<BoxCollider>();
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler();

        if(doEvent) OpenTrap();

        if (!TrapAnimIsPlaying()) isOpen = false;

        if (!isOpen) CloseTrap();
        }
    //-------------------------------------------------------------
    private void OpenTrap()
    {
        objectAnim.SetTrigger("open");
        objectBC.isTrigger = true;
        isOpen = true;
        doEvent = false;
    }
    private void CloseTrap()
    {
        objectAnim.SetTrigger("close");
        objectBC.isTrigger = false;
        isOpen = false;
    }
    private bool TrapAnimIsPlaying() { return objectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1; }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) EnterMachine(this.GetComponent<ConsoleStation>());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            StayMachine(autoExec = false);
            if (isUsable && InputManager.performB) doEvent = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") ExitMachine();
    }
}
