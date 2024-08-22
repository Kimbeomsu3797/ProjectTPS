using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//Ŭ������ [Sysytem.Serializable]�� ����� �ν����� �信 ����
[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}


public class CsPlayerCtrl : MonoBehaviour
{
    float h = 0f;
    float v = 0f;

    Transform tr;
    //�̵� �ӵ�
    public float moveSpeed = 10f;
    //ȸ�� �ӵ�
    public float rotSpeed = 100f;

    //�ν����ͺ信 ǥ���� �ִϸ��̼� Ŭ���� ����
    public Anim anim;
    //�ڽ������ִ� 3D���� Animation ������Ʈ�� �����ϱ� ���� ����
    public Animation _animation;

    public int hp = 100;

    public Image imgHpbar;
    //Player ���� �ʱⰪ
    private int initHp;

    void Start()
    {
        initHp = hp;
        tr = GetComponent<Transform>();
        //�ڽ��� �ڽ����� �ִ� Animation ������Ʈ�� ã�ƿ� ������ �Ҵ�
        _animation = GetComponentInChildren<Animation>();

        //Animation ������Ʈ�� �ִϸ��̼� Ŭ���� ����
        _animation.clip = anim.idle;
        //�ִϸ��̼� ����
        _animation.Play();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //�����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //(���� ��ǥ�� Space.World : ������ǥ�� / Space.Slef : ������ǥ��)
        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);

        //Vector3.up ���� �������� rotSpeed��ŭ�� �ӵ��� ȸ��
        //���콺 �¿� �̵� ���� ���� ó��
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        //Ű���� �Է°��� �������� ������ �ִϸ��̼� ����
        if (v >= 0.1f)
        {
            //���� �ִϸ��̼�
            //CrossFade : �ִϸ��̼��� ������ �ε巴�� ���ִ� ���� �Լ�
            //(�ִϸ��̼� Ŭ�� ��Ī, ���̵�ƿ��Ǵ� �ð�)
            _animation.CrossFade(anim.runForward.name, 0.4f);
        }
        else if (v <= -0.1f)
        {
            //���� �̵� �ִϸ��̼�
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            //������ �ִϸ��̼�
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            //���� �ִϸ��̼�
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            //��� �ִϸ��̼�
            _animation.CrossFade(anim.idle.name, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PUNCH")
        {
            hp -= 10;
            Debug.Log("Player HP = " + hp.ToString());

            imgHpbar.fillAmount = (float)hp / (float)initHp;

            if(hp <= 0)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("�÷��̾� ���");
        //MONSTER��� �±׸� ���� ��� ���ӿ�����Ʈ�� ã�ƿ�
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        //��� ������ OnPlayerDie �Լ��� ���������� ȣ��
        foreach(GameObject monster in monsters)
        {
            //private �Լ� ȣ�� ���
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }
    }
}
