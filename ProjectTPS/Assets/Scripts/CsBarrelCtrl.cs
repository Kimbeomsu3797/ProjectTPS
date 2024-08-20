using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    Transform tr;
    int hitcount = 0;

    public Texture[] textures;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BULLET")
        {
            Destroy(collision.gameObject);
            if(++hitcount >= 3)
            {
                ExpBarrel();
            }

        }
    }
    void ExpBarrel()
    {
        GameObject boom = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(boom, 1f);

        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);

        foreach(Collider collision in colls)
        {
            Rigidbody rbody = collision.GetComponent<Rigidbody>();
            if(rbody != null)
            {
                rbody.mass = 1.0f;
                rbody.AddExplosionForce(1000.0f, tr.position, 10.0f, 300.0f);
            }
        }
        Destroy(gameObject, 3f);
    }
}
