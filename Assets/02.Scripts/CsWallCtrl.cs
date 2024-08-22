using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsWallCtrl : MonoBehaviour
{
    //����ũ ��ƼŬ ������ ������ ����
    public GameObject sparkEffect;

    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BULLET")
        {
            //����ũ ��ƼŬ�� �������� ����
            GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            //�ð��� ���� �� ���� ó��
            Destroy(spark, 0.2f);

            //�浹�� ���ӿ�����Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
