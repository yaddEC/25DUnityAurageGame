using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    private LineRenderer laser;
    private bool isStunned;
    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                        PowerManager.outOfPower = true;
                    else
                        laser.SetPosition(1, new Vector3(0, 0, hit.distance));
                }
                else
                {
                    laser.SetPosition(1, new Vector3(0, 0, 3000));
                }
            }
        }

    }

    public void Stun(float stunDuration)
    {
        StartCoroutine(Stunned(stunDuration));
    }

    public IEnumerator Stunned(float stunDuration)
    {

        isStunned = true;
        laser.SetPosition(1, new Vector3(0, 0, 0));

        yield return new WaitForSeconds(stunDuration);
        laser.SetPosition(1, new Vector3(0, 0, 3000));
        isStunned = false;

    }
}
