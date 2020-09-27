using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDeRede : MonoBehaviourPunCallbacks
{
    public static GestorDeRede Instancia { get; private set; }
    private void Awake()
    {
        if(Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexão bem Sucedida.");
    }
}
