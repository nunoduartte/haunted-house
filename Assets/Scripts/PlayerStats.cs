using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using System.IO;


public class PlayerStats : MonoBehaviour
{
    public float sanity = 100;
    private PhotonView pv;
    public AIEnemy.EnemyState enemyState = AIEnemy.EnemyState.Sleep;

    public GameObject camera;

    private bool collisionGround1Floor;
    private bool collisionGround2Floor;
    public GameObject body;
    public GameObject content;
    public GameObject flashlight;

    private bool inHouse = false;
    public ManagerEnviromentSound enviromentSound;

    void Start()
    {
        this.pv = this.GetComponent<PhotonView>();
        this.enviromentSound = GameObject.FindGameObjectWithTag("EnviromentSound").GetComponent<ManagerEnviromentSound>();
    }

    void Update()
    {
        inputs();
        float timeToGameOver = GameObject.Find("GameManager").GetComponent<GameManager>().timeToGameOver;
        enviromentSound.UpdateEnemyStateLocal(enemyState);
        if(timeToGameOver <= 100){
            enemyState = AIEnemy.EnemyState.Attack;
        }else{
            enemyState = AIEnemy.EnemyState.Sleep;
        }
    }

    private void inputs()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray;
            RaycastHit hit;
            ray = new Ray(this.camera.transform.position, this.camera.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Flashlight"))
                {
                    this.flashlight.GetComponent<Flashlight>().hasFlashLight = true;
                    hit.collider.gameObject.SetActive(false);
                }

                if (hit.collider.gameObject.CompareTag("key"))
                {
                    print("pegou a chave");
                    print(hit.collider.GetComponent<Key>().id);
                    this.GetComponent<Keys>().playersKeys.Add(hit.collider.GetComponent<Key>().id);
                    hit.collider.GetComponent<Key>().DestroyKey();
                }
            }
        }
    }
}
