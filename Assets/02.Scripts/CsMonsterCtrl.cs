using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CsMonsterCtrl : MonoBehaviour
{
    public enum MonsterState
    {
        idle,
        trace,
        attack,
        die
    }

    public MonsterState mState;

    public float traceDist = 10f;
    public float attackDist = 2f;
    private bool isDie = false;

    //�ӵ� ����� ���� ���� ������Ʈ ���� �Ҵ�
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public GameObject bloodEffect;
    public GameObject bloodDecal;
    GameUI gameUI;
    public int hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        animator = GetComponent<Animator>();
        //���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����
        //�÷��̾��� �ʱ� ��ġ ���� �÷��̾��� ��ġ�� ���󰡷���
        //nvAgent.destination = playerTr.position;

        //������ �������� ������ �ൿ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ����
        StartCoroutine(CheckMonsterState());
        //������ ���¿� ���� �����ϴ� ��ƾ�� �����ϴ� �ڷ�ƾ �Լ� ����
        StartCoroutine(MonsterAction());

    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            //0.2�� ���� ��ٷȴٰ� �������� �Ѿ.
            yield return new WaitForSeconds(0.2f);
            //���Ϳ� �÷��̾� ������ �Ÿ� ����
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            //���ݰŸ� ���� �̳��� ���Դ��� Ȯ��
            if(dist <= attackDist)
            {
                //������ ���¸� ��������
                mState = MonsterState.attack;
            }
            else if(dist <= traceDist)
            {
                //������ ���¸� �������� ����
                mState = MonsterState.trace;
            }
            else
            {
                //������ ���¸� idle�� ����
                mState = MonsterState.idle;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch(mState)
            {
                case MonsterState.idle:
                    nvAgent.Stop();
                    animator.SetBool("IsTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;
                case MonsterState.attack:
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BULLET")
        {
            animator.SetTrigger("IsHit");
            Destroy(collision.gameObject);
            CreateBooldEffect(collision.transform.position);

            hp -= collision.gameObject.GetComponent<CsBulletCtrl>().damage;
            if(hp<=0)
            {
                MonsterDie();
            }
            
        }
    }

    void MonsterDie()
    {
        //����� ������ �±׸� ����
        gameObject.tag = "Untagged";

        StopAllCoroutines();

        isDie = true;
        //����ġ ���� �ش��ϴ� ��ƾ�� ����.
        mState = MonsterState.die;
        animator.SetTrigger("IsDie");
        //���Ϳ� �߰��� Collider ��Ȱ��ȭ
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        foreach(Collider coll in gameObject.GetComponentsInChildren<Collider>())
        {
            coll.enabled = false;
        }
        gameUI.DispScore(50);
    }

    void CreateBooldEffect(Vector3 pos)
    {
        GameObject blood1 = Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 2f);
        //��Į ���� ��ġ : �ٴڿ��� ���� ����
        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);
        //��Į�� ȸ������ �������� ����
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
        //��Į ������ ����
        GameObject blood2 = Instantiate(bloodDecal, decalPos, decalRot);
        //��Į ũ�⵵ �ұ�Ģ������ ��Ÿ���� ������ ����
        float scale = Random.Range(1.5f, 3.5f);
        blood2.transform.localScale = Vector3.one * scale;
        //5���Ŀ� ��Į ����
        Destroy(blood2, 5f);
    }

    void OnPlayerDie()
    {
        //������ ���¸� üũ�ϴ� �ڷ�ƾ�� ��� ������Ŵ
        StopAllCoroutines();
        //������ �����ϰ� �ִϸ��̼��� ����
        nvAgent.Stop();
        animator.SetTrigger("IsPlayerDie");
    }

    public void OnDamage(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));
        CreateBooldEffect((Vector3)_params[0]);
        hp -= (int)_params[1];
        if(hp <= 0)
        {
            MonsterDie();
        }
        animator.SetTrigger("IsHit");
    }
    public void Idle()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
