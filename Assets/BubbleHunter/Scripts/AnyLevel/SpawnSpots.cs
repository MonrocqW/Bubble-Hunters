using System;
using System.Collections.Generic;
using System.Linq;
using BubHun.Lobby;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubHun.Level
{
    public class SpawnSpots : MonoBehaviour
    {
        [SerializeField] private Transform[] m_spots;

        private List<int> m_freeSpots = new List<int>();

        private void Awake()
        {
            m_freeSpots = Enumerable.Range(0, m_spots.Length).ToList();
        }

        private void Start()
        {
            
            PlayersManager.Instance.SetPlayersSpotsWith(this);
        }

        public Transform GetRandomSpot()
        {
            int l_spot = m_freeSpots[Random.Range(0,m_freeSpots.Count)];
            m_freeSpots.Remove(l_spot);
            return m_spots[l_spot];
        }
    }
}