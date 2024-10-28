﻿
using TMPro;
using UnityEngine;


public class MenuEntrada : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nomeDaSala;
    [SerializeField] private string _nomeDoJogador = "Rai";

    public void EntraSala()
    {
        GestorDeRede.Instancia.MudaNick(_nomeDoJogador);
        GestorDeRede.Instancia.EntraSala(_nomeDaSala.text);
    }
}
