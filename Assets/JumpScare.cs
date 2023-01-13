using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject ghost;
    public GameObject body;
    float time = 1;
    public float velocity = 7;
    public float speed = 7;
    public float timeToLive = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime * velocity;
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0)
            Destroy(ghost);
        this.transform.position += transform.forward * Time.deltaTime * speed;
        if(time <= 0){
            if(this.body.activeSelf){
                this.body.SetActive(false);
                time = 1;
            }
            else{
                this.body.SetActive(true);
                time = 1;
            }
        }
        
    }
}
