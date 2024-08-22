using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSpawn : MonoBehaviour
{
    public GameObject barrel;
    //�ڽ� �ݶ��̴��� ����� �������� ���� ����
    private BoxCollider area;

    public int count;

    void Start()
    {
        area = GetComponent<BoxCollider>();
        for (int i = 0; i < count; i++)
        {
            //���� + ������ġ�� �����ϴ� �Լ�
            Spawn();
        }
        //�ڽ��ݶ��̴� ���۳�Ʈ ��Ȱ��ȭ
        area.enabled = false;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position; // 0,0,0
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, 0, posZ);
        return spawnPos;
    }

    private void Spawn()
    {
        //int selection = Random.Range(0, prefabs.Length);

        Vector3 spawnPos = GetRandomPosition(); // ������ġ�Լ�

        GameObject instance = Instantiate(barrel, spawnPos, Quaternion.identity);


    }
}
