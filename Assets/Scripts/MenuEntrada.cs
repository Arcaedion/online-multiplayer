using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrada : MonoBehaviour
{
    [SerializeField] private Text _nomeDoJogador;
    [SerializeField] private Text _nomeDaSala;

    public void CriaSala()
    {
        GestorDeRede.Instancia.MudaNick(_nomeDoJogador.text);
        GestorDeRede.Instancia.CriaSala(_nomeDaSala.text);
    }    
    public void EntraSala()
    {
        GestorDeRede.Instancia.MudaNick(_nomeDoJogador.text);
        GestorDeRede.Instancia.EntraSala(_nomeDaSala.text);
    }
}
