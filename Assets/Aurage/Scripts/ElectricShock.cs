using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShock : MonoBehaviour
{
    public Color color;
    public float fadeSpeed = 1f;
    public float stunDuration = 5;
    private LayerMask enemyMask;
    public int scale = 1;
    public Vector3 oneScale = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        color = this.GetComponent <Renderer> ().material.color;
        StartCoroutine(ShockBehavior());
    }

    // Update is called once per frame


    public IEnumerator ShockBehavior()
    {
   
        
        float time = 1f;
        float scale = 0f;
        while (time >0) 
        {
            
            time -= Time.deltaTime*fadeSpeed;
            scale += Time.deltaTime*fadeSpeed;
            color.a = time;
            transform.localScale = Vector3.one+oneScale * scale;

            this.GetComponent<Renderer>().material.color = color;
            yield return new WaitForSeconds(0.005f);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BasicEnemy")
        {
            other.gameObject.GetComponent<BasicEnemy>().Stun(stunDuration);
        }
    }
}
