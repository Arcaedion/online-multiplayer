using Arcaedion.Multiplayer;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instancia { get; private set; }

    [SerializeField] private string _localizacaoPrefab;
    [SerializeField] private Transform[] _spawns;

    private int _jogadoresEmJogo = 0;
    private List<ControleJogadorNet> _jogadores;
    public List<ControleJogadorNet> Jogadores { get => _jogadores; private set => _jogadores = value; }



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
        _jogadores = new List<ControleJogadorNet>();
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
        var jogador = jogadorObj.GetComponent<ControleJogadorNet>();

        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
