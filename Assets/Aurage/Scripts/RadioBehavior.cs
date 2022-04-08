using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioBehavior : MonoBehaviour
{
    public GameObject radioZone;
    GameObject radioZoneClone;
    public bool isZoneActive;
    public float distractionDuration=5;
    // Start is called before the first frame update
    void Start()
    {
        isZoneActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRadio()
    {
        if(!isZoneActive)
        {

            radioZoneClone = Instantiate(radioZone, transform.position, Quaternion.identity);
            isZoneActive = true;
        }
    }

    public IEnumerator EndRadio(float distractionDuration)
    {
        if (isZoneActive)
        {
            radioZoneClone.GetComponent<RadioZone>().isDeactivate = true;
           
            yield return new WaitForSeconds(distractionDuration);
           
            radioZoneClone.GetComponent<RadioZone>().wasDeactivate = true;
           
            yield return new WaitForSeconds(1f);
            
            Destroy(radioZoneClone);
            isZoneActive = false ;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BasicEnemy"))
        {
            StartCoroutine(EndRadio(distractionDuration));
        }
            


    }
}


