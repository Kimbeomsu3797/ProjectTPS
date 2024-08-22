using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFollowCam : MonoBehaviour
{
    //������ Ÿ�� ���ӿ�����Ʈ�� Transform ����
    public Transform targetTr;
    //ī�޶���� ���� �Ÿ�
    public float dist = 10f;
    //ī�޶� ���� ����
    public float height = 3f;
    //�ε巯�� ������ ���� ����
    public float damTrace = 20f;

    //ī�޶� �ڽ��� Transform ����
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        CursorLock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Update �Լ� ȣ�� ���� �ѹ��� ȣ��Ǵ� �Լ�
    //������ Ÿ���� �̵��� ����� ���Ŀ� ī�޶� �����ϱ� ���� ���
    private void LateUpdate()
    {
        //Vector3.Lerp(Vector3 ������ġ, Vector3 ������ġ, float �ð�)
        tr.position = Vector3.Lerp(tr.position, 
            targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), 
            Time.deltaTime * damTrace);

        //ī�޶� Ÿ�� ���ӿ�����Ʈ�� �ٶ󺸰� ����
        tr.LookAt(targetTr.position);
    }

    public void CursorLock()
    {
        // ���콺 Ŀ�� ȭ�� �߾ӿ� ����
        Cursor.lockState = CursorLockMode.Locked;
        // ���콺 Ŀ�� �Ⱥ��̰�
        Cursor.visible = false;
    }
}
