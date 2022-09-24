using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public Text TitleTx;
    public InputField Nick_Input;
    public GameObject LoginPanel;
    public GameObject Before_Connect;
    public GameObject After_Connect;


    private void Awake()
    {
        Screen.SetResolution(960, 540, false); //창모드
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    private IEnumerator Start()
    {
        yield return PhotonNetwork.ConnectUsingSettings();
        if(PhotonNetwork.IsConnected)
        {
            Before_Connect.SetActive(false);
            After_Connect.SetActive(true);
        }
        else
        {
            Application.Quit();
        }
    }

    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("1대1뜨실분");
        else PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinedRoom()
    {
        LoginPanel.SetActive(false);
        PhotonNetwork.Instantiate("Mask_Dude", Vector2.zero, Quaternion.identity);
    }

    public void ClickBtn()
    {
        if (Nick_Input.text.Length > 1)
        {
            PhotonNetwork.NickName = Nick_Input.text;
            PhotonNetwork.JoinLobby();
        }
        else TitleTx.text = "닉네임은 2글자 이상!";
    }

    private void Update()
    {
        if(LoginPanel.activeSelf&&Input.GetKeyDown(KeyCode.Return))
        {
            ClickBtn();
        }

    }



}
