using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public PlayersData playersData;
    public PhotonView pv;
    public Transform[] spawnPoints;
    public GameObject myPlayer;
    public List<Door> doors;
    public float timeToGameOver = 10f;
    private bool changeScene = false;
    private bool winGame = false;

    void Start()
    {
        this.pv = GetComponent<PhotonView>();
        
        int spawnPicker = 0;
        if(PhotonNetwork.IsMasterClient){
            spawnPicker = 1;
        }
        this.playersData = GameObject.Find("PlayersData").GetComponent<PlayersData>();
        PlayerConfiguration player = (PlayerConfiguration)this.playersData.listPlayer[PhotonNetwork.LocalPlayer.NickName];
        string path = "Player" + player.playerRace + player.playerGender;
        this.myPlayer = PhotonNetwork.Instantiate(path, this.spawnPoints[spawnPicker].position, this.spawnPoints[spawnPicker].rotation);
        this.myPlayer.name = player.playerName;
        this.myPlayer.GetComponent<PlayerMovement>().enabled = true;
        this.myPlayer.GetComponent<PlayerStats>().enabled = true;
        foreach (Door d in this.doors)
        {
            d.Jogador = this.myPlayer;
            d.keysList = this.myPlayer.GetComponent<Keys>();
        }
        GameObject.Find("Camera").SetActive(false);
        this.myPlayer.GetComponent<PlayerMovement>().camera.tag = "MainCamera";
        this.myPlayer.GetComponent<PlayerMovement>().camera.GetComponent<MouseLook>().enabled = true;
        this.myPlayer.GetComponent<PlayerMovement>().camera.GetComponent<AudioListener>().enabled = true;
        this.myPlayer.GetComponent<PlayerMovement>().camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        int layer1 = LayerMask.NameToLayer("Default");
        int layer2 = LayerMask.NameToLayer("TransparentFX");
        int layer3 = LayerMask.NameToLayer("Ignore Raycast");
        int layer4 = LayerMask.NameToLayer("Water");
        int layer5 = LayerMask.NameToLayer("UI");
        int layer6 = LayerMask.NameToLayer("Ground");
        this.myPlayer.GetComponent<PlayerMovement>().camera.GetComponent<Camera>().cullingMask = (1 << layer1) | (1 << layer2) | (1 << layer3) | (1 << layer4) | (1 << layer5) | (1 << layer6);
    }

    void Update()
    {
        timeToGameOver -= Time.deltaTime * 1;
        if(timeToGameOver <= 0 && PhotonNetwork.IsMasterClient && !this.changeScene){
            PhotonNetwork.LoadLevel(3);
            this.changeScene = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        if(timeToGameOver > 0 && winGame && PhotonNetwork.IsMasterClient && !this.changeScene){
            PhotonNetwork.LoadLevel(2);
            this.changeScene = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (this.changeScene)
            return;
    }

    [PunRPC]
    public void WinGame()
    {
        this.winGame = true;
    }

}
