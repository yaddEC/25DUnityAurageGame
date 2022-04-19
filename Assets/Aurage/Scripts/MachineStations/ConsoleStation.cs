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
        CooldownHandler(false);
        ClampInMachine();

        if(TrapAnimIsPlaying()) isOpen = true;
        else isOpen = false;

        if (isOpen) { objectBC.isTrigger = true; }
        else { objectBC.isTrigger = false; }
        }
    //-------------------------------------------------------------
    private void OpenTrap()
    {
        objectAnim.SetTrigger("open");
        doEvent = false;
    }
    private bool TrapAnimIsPlaying() { return objectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1; }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0) EnterMachine();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            StayMachine(false);
            if (isUsable && InputManager.performB) doEvent = true;
            if (doEvent) OpenTrap();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") ExitMachine();
    }
}
