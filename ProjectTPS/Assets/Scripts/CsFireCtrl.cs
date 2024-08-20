using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BulletFire();
        }
    }

    void BulletFire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
