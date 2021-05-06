using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    public string code;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            gameObject.SetActive(false);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master server");
        //base.OnConnectedToMaster();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
        code = roomName;
    }

    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("CoopGame");
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
        code = roomName;
    }
}
