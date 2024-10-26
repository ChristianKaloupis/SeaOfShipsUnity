using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomButtonPrefab; // Prefab do bot�o de sala
    [SerializeField] private Transform roomListContainer; // Container para a lista de salas
    [SerializeField] private Button joinRoomButton; // Bot�o para entrar na sala

    private Dictionary<string, GameObject> roomButtons = new Dictionary<string, GameObject>();
    private string selectedRoomName; // Nome da sala selecionada
    // Start is called before the first frame update
 

    public void CriarLobby()
    {
        RoomOptions salaOptions = new RoomOptions();
        salaOptions.MaxPlayers = 2;
        salaOptions.IsVisible = true;
        salaOptions.IsOpen = true;
        PhotonNetwork.CreateRoom("Room", salaOptions);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Limpa a lista para que seja atualizada com as salas dispon�veis atuais
        foreach (var roomButton in roomButtons.Values)
        {
            Destroy(roomButton);
        }
        roomButtons.Clear();

        // Para cada sala dispon�vel, cria um bot�o na interface
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                // Remove a sala da lista se n�o for mais exibida
                if (roomButtons.ContainsKey(roomInfo.Name))
                {
                    Destroy(roomButtons[roomInfo.Name]);
                    roomButtons.Remove(roomInfo.Name);
                }
                continue;
            }

            // Cria o bot�o para a sala e define o nome
            GameObject newRoomButton = Instantiate(roomButtonPrefab, roomListContainer);
            newRoomButton.GetComponentInChildren<TextMeshProUGUI>().text = roomInfo.Name;

            // Salva a refer�ncia para poder excluir ou atualizar se necess�rio
            roomButtons[roomInfo.Name] = newRoomButton;

            // Adiciona uma fun��o de sele��o para o bot�o
            newRoomButton.GetComponent<Button>().onClick.AddListener(() => SelectRoom(roomInfo.Name));
        }
    }

    private void SelectRoom(string roomName)
    {
        selectedRoomName = roomName;
        Debug.Log("Sala selecionada: " + selectedRoomName);
    }

    public void JoinSelectedRoom()
    {
    
        if (!string.IsNullOrEmpty(selectedRoomName))
        {
            Debug.Log(selectedRoomName);
            PhotonNetwork.JoinRoom(selectedRoomName);
        }
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


}
