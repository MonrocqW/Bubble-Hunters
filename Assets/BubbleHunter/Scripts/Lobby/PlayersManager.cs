using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BubHun.Level;
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
        [SerializeField] private Transform m_playersHolder;
        [SerializeField] private GameObject m_lobbyUiParent;
        [SerializeField] private CharacterSelection[] m_lobbyMenus = Array.Empty<CharacterSelection>();
        [SerializeField] private float m_roundStartDelay;
        
        private static PlayersManager s_instance;
        public static PlayersManager Instance => s_instance;

        private bool m_inLobby = true;
        private PlayerInputManager m_playerInputManager;
        private static Dictionary<int,PlayerConfig> s_players = new Dictionary<int,PlayerConfig>();

        private static bool s_movementAuthorized = false;
        public static bool AllPlayersMovementAuthorized => s_movementAuthorized;
        public static event Action OnMovementAuthorizationChanged;

        private void Awake()
        {
            m_playerInputManager = this.GetComponent<PlayerInputManager>();
            s_instance = this;
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }
        
        #region On Scene Loaded

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

        private async void OnAnyLevel()
        {
            SetMovementAuthorization(false);
            await Task.Delay((int)(m_roundStartDelay * 1000));
            SetMovementAuthorization(true);
        }

        public void SetPlayersSpotsWith(SpawnSpots p_spawnSpots)
        {
            foreach (PlayerConfig l_config in s_players.Values)
            {
                l_config.playerInput.transform.position = p_spawnSpots.GetRandomSpot().position;
            }
        }

        private void OnLobby()
        {
            SetMovementAuthorization(false);
            foreach (int l_player in s_players.Keys)
            {
                this.SetLobbySelection(l_player);
            }
        }

        private void SetMovementAuthorization(bool p_authorized)
        {
            s_movementAuthorized = p_authorized;
            OnMovementAuthorizationChanged?.Invoke();
        }
        
        #endregion

        #region Lobby Character Selection
        
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

        public void SelectCharacter(int p_playerIndex, CharacterData p_charData)
        {
            if (s_players.ContainsKey(p_playerIndex))
                s_players[p_playerIndex].SetCharacter(p_charData);
        }
        
        #endregion
        
        #region Player join events

        public void OnPlayerJoined(PlayerInput p_player)
        {
            Debug.Log($"Player joined {p_player.playerIndex}");
            s_players[p_player.playerIndex] = new PlayerConfig(p_player);
            p_player.transform.SetParent(m_playersHolder);
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
        
        #endregion
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