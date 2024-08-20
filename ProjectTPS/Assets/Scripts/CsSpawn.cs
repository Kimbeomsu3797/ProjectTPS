using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSpawn : MonoBehaviour
{
    public GameObject _barrel;
    private BoxCollider area;
    public int count = 15;
    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();

        for(int i = 0; i < count; ++i)
        {
            Spawn();
        }
        area.enabled = false;
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, 0, posZ);
        return spawnPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(_barrel, spawnPos, Quaternion.identity);
    }
}
