using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioStation : MonoBehaviour
{
    private GameObject radioZoneClone;
    private GameObject radioZone;
    private bool isRadioActive = false;
    public float cooldown = 5;

    private void Awake()
    {
        radioZone = Resources.Load<GameObject>("Prefabs/RadioDetection");
    }

    public void TurnRadioOn()
    {
        if(!isRadioActive)
        {
            radioZoneClone = Instantiate(radioZone, transform.position, Quaternion.identity);
            isRadioActive = true;
        }
    }

    private IEnumerator TurnRadioOff()
    {
        radioZoneClone.GetComponent<RadioZone>().isActive = false;
           
        yield return new WaitForSeconds(cooldown);
        radioZoneClone.GetComponent<RadioZone>().isActive = true;
    
        yield return new WaitForSeconds(1f);
        Destroy(radioZoneClone);
        isRadioActive = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BasicEnemy" && isRadioActive)
            StartCoroutine(TurnRadioOff());
    }
}


