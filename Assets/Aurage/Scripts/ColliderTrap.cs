using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTrap : Station
{
    [Header("Property Info")]
    private GameObject trap;
    private Animator trapAnim;
    private BoxCollider trapBC;
    //-------------------------------------------------------------
    private void Awake() 
    {
        var t = transform.parent.gameObject.name; tagToSearch = t;
        trap = UnityFinder.FindGameObjectInChildWithTag(transform.parent.gameObject, "Trap");
        trapAnim = trap.GetComponent<Animator>();
        trapBC = trap.GetComponent<BoxCollider>();
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        CooldownHandler(false);
        ClampInMachine();

        if(TrapAnimIsPlaying()) trapBC.isTrigger = true;
        else trapBC.isTrigger = false;

    }
    //-------------------------------------------------------------
    private void OpenTrap()
    {
        trapAnim.SetTrigger("open");
        doEvent = false;
    }
    private bool TrapAnimIsPlaying() { return trapAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1; }
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
