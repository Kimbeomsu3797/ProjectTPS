using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float moveSpeed = 10f;
    public float rotSpeed = 100f;
    public Anim anim;
    public Animation _animation;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        _animation = GetComponentInChildren<Animation>();
        _animation.clip = anim.idle;
        _animation.Play();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        if(h >= 0.1f)
        {
            
            _animation.CrossFade(anim.runRight.name, 0.4f);
        }
        else if(h <= -0.1f)
        {
           
            _animation.CrossFade(anim.runLeft.name, 0.4f);
        }
        else if(v <= -0.1f)
        {
          
            _animation.CrossFade(anim.runBackward.name, 0.4f);
        }
        else if (v >= 0.1f)
        {
            
            _animation.CrossFade(anim.runForward.name, 0.4f);
        }
        else
        {
            
            _animation.CrossFade(anim.idle.name, 0.4f);
        }
    }
}
