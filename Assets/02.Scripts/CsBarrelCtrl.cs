using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    Transform tr;

    int hitCount = 0;

    //무작위 선택할 텍스처 배열
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
            //충돌한 총알 제거
            Destroy(collision.gameObject);


            if(++hitCount>=3)
            {
                ExpBarrel();
            }
            
        }
    }

    void ExpBarrel()
    {
        //이펙트를 동적 생성
        GameObject exEff = Instantiate(expEffect, tr.position + Vector3.up, Quaternion.identity);
        //생성된 이펙트를 이펙트 작동후 1초 후 제거
        Destroy(exEff, 1f);

        //지정한 원점을 중심으로 10f 반경 내에 들어와 있는 Collider 객체 추출
        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);
        //추출한 Collider 객체에 폭발력 전달
        foreach(Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if(rbody!=null)
            {
                rbody.mass = 1.0f;
                //Rigidbody.AddExplosionForce(폭발력, 원점, 반경, 위로 솟구치는 힘)
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
