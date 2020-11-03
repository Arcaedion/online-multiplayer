using Arcaedion.Multiplayer;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameManager Instancia { get; private set; }

    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform[] _spawns;

    private int _jogadoresEmJogo = 0;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void AdicionaJogador()
    {
        _jogadoresEmJogo++;
        if(_jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
        {
            CriaJogador();
        }
    }

    private void CriaJogador()
    {
        var jogadorObj = PhotonNetwork.Instantiate(_localizacaoPrefab, _spawns[Random.Range(0, _spawns.Length)].position, Quaternion.identity);
        var jogador = jogadorObj.GetComponent<ControleJogador>();


    }
}
