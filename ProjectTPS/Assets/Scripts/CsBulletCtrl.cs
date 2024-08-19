using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBulletCtrl : MonoBehaviour
{
    public float bulletSpeed = 1000f;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
    }
    
}
