using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFollowCam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 10f;
    public float height = 3f;
    public float damTrace = 20f;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * damTrace);
        //Lerp(카메라의 위치,목표 위치, 목표 위치로 이동하는시간)
        tr.LookAt(targetTr.position);
    }
}
