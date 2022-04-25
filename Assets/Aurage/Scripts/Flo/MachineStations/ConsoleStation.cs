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
    public bool animationPlaying = false;
    //-------------------------------------------------------------
    private void Awake() 
    {
        var t = transform.parent.gameObject.name; tagToSearch = t;
        objectToInteractWith = UnityFinder.FindGameObjectInChildWithTag(transform.parent.gameObject, transform.parent.GetComponentInChildren<Animator>().gameObject.tag);
        objectAnim = objectToInteractWith.GetComponent<Animator>();
        objectBC = objectToInteractWith.GetComponent<BoxCollider>();
        objectAnim.SetTrigger("close");
    }
    private void Start() { RegisterReferences(); }
    private void Update()
    {
        TrapAnimIsPlaying();

        CooldownHandler(special = false);

        if (doEvent && !animationPlaying) 
            StartCoroutine(TrapSwitcher());
        }
    //-------------------------------------------------------------
    private IEnumerator TrapSwitcher()
    {
        Debug.Log("started animation Open");
        doEvent = false;

        objectBC.gameObject.layer = 0;
        objectBC.isTrigger = true;
        objectAnim.SetTrigger("open");
        yield return new WaitForSeconds(5f);
        objectAnim.SetTrigger("close");
        objectBC.gameObject.layer = 10;
        objectBC.isTrigger = false;

        Debug.Log("finished animation Open");
    }
    private void TrapAnimIsPlaying() { animationPlaying = objectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1; }
    //-------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
            EnterMachine(this.GetComponent<ConsoleStation>());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && cooldown <= 0)
        {
            StayMachine(autoExec = false);
            if (isUsable && InputManager.performB) 
                doEvent = true;
        }
    }
}
