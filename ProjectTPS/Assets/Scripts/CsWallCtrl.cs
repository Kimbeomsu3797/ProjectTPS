using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsWallCtrl : MonoBehaviour
{
    public GameObject sparkEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BULLET")
        {
            GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            Destroy(spark, 0.2f);
            Destroy(collision.gameObject);
        }
    }
}
