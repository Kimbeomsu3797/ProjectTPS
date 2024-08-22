using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    Transform tr;

    int hitCount = 0;

    //������ ������ �ؽ�ó �迭
    public Texture[] textures;

    void Start()
    {
        tr = GetComponent<Transform>();

        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BULLET")
        {
            //�浹�� �Ѿ� ����
            Destroy(collision.gameObject);


            if(++hitCount>=3)
            {
                ExpBarrel();
            }
            
        }
    }

    void ExpBarrel()
    {
        //����Ʈ�� ���� ����
        GameObject exEff = Instantiate(expEffect, tr.position + Vector3.up, Quaternion.identity);
        //������ ����Ʈ�� ����Ʈ �۵��� 1�� �� ����
        Destroy(exEff, 1f);

        //������ ������ �߽����� 10f �ݰ� ���� ���� �ִ� Collider ��ü ����
        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);
        //������ Collider ��ü�� ���߷� ����
        foreach(Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if(rbody!=null)
            {
                rbody.mass = 1.0f;
                //Rigidbody.AddExplosionForce(���߷�, ����, �ݰ�, ���� �ڱ�ġ�� ��)
                rbody.AddExplosionForce(1000.0f, tr.position, 10.0f, 300f);
            }
            
        }
        Destroy(gameObject, 5f);
    }
    void OnDamage(object[] _params)
    {
        Vector3 firePos = (Vector3)_params[0];
        Vector3 hitPos = (Vector3)_params[1];
        Vector3 incomVector = hitPos - firePos;
        incomVector = incomVector.normalized;
        GetComponent<Rigidbody>().AddForceAtPosition(incomVector * 1000f, hitPos);
        if(++hitCount >= 3)
        {
            ExpBarrel();
        }
    }
}
