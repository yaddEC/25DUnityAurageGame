using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    RadioStation radio;
    // Start is called before the first frame update
    void Start()
    {
        radio = this.transform.parent.GetComponent<RadioStation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "BasicEnemy" )
        {
            Debug.Log("Halt");
            other.GetComponent<BasicEnemy>().Halt();
            if (radio.isRadioActive)
            {
                Debug.Log("Off");
                StartCoroutine(radio.TurnRadioOff());
            }
        }
           
   }
}
