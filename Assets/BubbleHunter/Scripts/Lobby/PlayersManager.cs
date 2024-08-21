using System;
using System.Collections.Generic;
using BubHun.Players;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

namespace BubHun.Lobby
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_lobbyUiParent;
        [SerializeField] private CharacterSelection[] m_lobbyMenus = Array.Empty<CharacterSelection>();
        
        private static PlayersManager s_instance;
        public static PlayersManager Instance => s_instance;

        private bool m_inLobby = true;
        private PlayerInputManager m_playerInputManager;
        private static Dictionary<int,PlayerConfig> s_players = new Dictionary<int,PlayerConfig>();
        //private static List<PlayerConfig> s_players = new List<PlayerConfig>();

        private void Awake()
        {
            m_playerInputManager = this.GetComponent<PlayerInputManager>();
            s_instance = this;
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene p_scene, LoadSceneMode p_loadMode)
        {
            m_inLobby = p_scene.name == "Lobby";
            m_lobbyUiParent.SetActive(m_inLobby);
            switch (p_scene.name)
            {
                default:
                    this.OnAnyLevel();
                    break;
                case "Lobby":
                    this.OnLobby();
                    break;
            }
        }

        private void OnAnyLevel()
        {
            //throw new NotImplementedException();
        }

        private void OnLobby()
        {
            foreach (int l_player in s_players.Keys)
            {
                this.SetLobbySelection(l_player);
            }
        }

        private void SetLobbySelection(int p_playerId)
        {
            m_lobbyMenus[p_playerId].gameObject.SetActive(true);
            m_lobbyMenus[p_playerId].SetPlayerIndex(p_playerId);
            s_players[p_playerId].playerInput.uiInputModule =
                m_lobbyMenus[p_playerId].GetComponent<InputSystemUIInputModule>();
        }

        private void RemoveLobbySelection(int p_playerId)
        {
            m_lobbyMenus[p_playerId].gameObject.SetActive(false);
        }

        public void OnPlayerJoined(PlayerInput p_player)
        {
            Debug.Log($"Player joined {p_player.playerIndex}");
            s_players[p_player.playerIndex] = new PlayerConfig(p_player);
            //s_players.Add(new PlayerConfig(p_player));
            if(m_inLobby)
                this.SetLobbySelection(p_player.playerIndex);
        }

        public void OnPlayerLeft(PlayerInput p_player)
        {
            Debug.Log($"Player left {p_player.playerIndex}");
            s_players.Remove(p_player.playerIndex);
            //s_players.RemoveAll(p_x => p_x.playerIndex == p_player.playerIndex);
            if(m_inLobby)
                this.RemoveLobbySelection(p_player.playerIndex);
        }

        public void SelectCharacter(int p_playerIndex, CharacterData p_charData)
        {
            if (s_players.ContainsKey(p_playerIndex))
                s_players[p_playerIndex].SetCharacter(p_charData);
        }
    }

    public class PlayerConfig
    {
        public int playerIndex;
        public PlayerInput playerInput;
        public CharacterData characterSelected;

        public PlayerConfig(PlayerInput p_input)
        {
            playerIndex = p_input.playerIndex;
            playerInput = p_input;
        }

        public void SetCharacter(CharacterData p_charData)
        {
            characterSelected = p_charData;
            playerInput.GetComponent<CharacterHolder>()?.SetCharacter(p_charData);
        }
    }
}