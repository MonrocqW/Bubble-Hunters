using System;
using BubHun.Lobby;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubHun.Players.Movement
{
    public class BubbleMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterData m_characterData;
        [SerializeField]
        private Transform m_dashTrailsParent;
        [SerializeField]
        private float m_maxSpeed = 30f;

        private Rigidbody2D m_rb;
        private Vector2 m_moveDirection;
        private bool m_isDashing = false;
        private float m_dashTime;
        private float m_dashCooldownStartTime = 0;
        private int m_storedDashes = 1;
        private TrailRenderer[] m_dashTrails = Array.Empty<TrailRenderer>();

        private bool m_movementLocked = false;
        private bool CanMove => !m_movementLocked && PlayersManager.AllPlayersMovementAuthorized;

        #region Unity

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_dashTrails = m_dashTrailsParent.GetComponentsInChildren<TrailRenderer>();
            this.UpdateMovementAuthorized();
            PlayersManager.OnMovementAuthorizationChanged += this.UpdateMovementAuthorized;
        }

        private void OnDestroy()
        {
            PlayersManager.OnMovementAuthorizationChanged -= this.UpdateMovementAuthorized;
        }

        private void UpdateMovementAuthorized()
        {
            m_rb.isKinematic = !PlayersManager.AllPlayersMovementAuthorized;
            m_rb.velocity = Vector2.zero;
        }

        void Update()
        {
            if (!CanMove)
                return;
            if (m_characterData == null)
                return;
            RechargeDash();
            HandleDash();
        }

        void FixedUpdate()
        {
            if (!CanMove)
                return;
            if (m_characterData == null)
                return;
            if (!m_isDashing)
            {
                Move();
            }
        }

        #endregion

        #region Inputs

        public void OnMovement(InputValue p_value)
        {
            m_moveDirection = p_value.Get<Vector2>().normalized;
        }

        public void OnDash()
        {
            if (!CanMove)
                return;
            if (m_characterData == null)
                return;
            if (m_isDashing || m_storedDashes < 1)
                return;

            this.StartDash();
        }

        #endregion
        
        #region Set Player

        public void SetPlayer(CharacterData p_data)
        {
            if (p_data == null)
                return;
            m_characterData = p_data;
        }
        
        #endregion

        #region Base Movement

        void Move()
        {
            if(m_rb.velocity.magnitude>m_maxSpeed)
                m_rb.velocity = m_rb.velocity.normalized * m_maxSpeed;
            if(m_moveDirection != Vector2.zero)
                m_rb.AddForce(m_moveDirection * m_characterData.Stats.MoveSpeed);// = Vector2.Lerp(m_rb.velocity, m_moveDirection * m_moveSpeed, m_redirectionSpeed);
        }

        #endregion

        #region Dash

        void RechargeDash()
        {
            if(m_storedDashes > m_characterData.Stats.DashNumber)
                m_storedDashes = m_characterData.Stats.DashNumber;
            if(m_storedDashes == m_characterData.Stats.DashNumber)
                return;
            
            if(Time.time > m_dashCooldownStartTime + m_characterData.Stats.DashCooldownTime)
            {
                m_storedDashes++;
                m_dashCooldownStartTime = Time.time;
            }
        }

        void StartDash()
        {
            m_isDashing = true;
            if(m_storedDashes == m_characterData.Stats.DashNumber)
                m_dashCooldownStartTime = Time.time;
            m_storedDashes--;
            m_dashTime = Time.time + m_characterData.Stats.DashDurationTime;
            Vector2 l_dashDirection = m_moveDirection != Vector2.zero ? m_moveDirection : m_rb.velocity.normalized;
            m_rb.velocity = l_dashDirection * m_characterData.Stats.DashSpeed;
            foreach (TrailRenderer l_trail in m_dashTrails)
                l_trail.emitting = true;
            if(m_characterData.Stats.PhantomDash)
                this.PhantomMode(true);
        }

        void HandleDash()
        {
            if (m_isDashing && Time.time >= m_dashTime)
            {
                m_isDashing = false;
                m_rb.velocity = m_rb.velocity.normalized * m_characterData.Stats.MoveSpeed;
                foreach (TrailRenderer l_trail in m_dashTrails)
                    l_trail.emitting = false;
                this.PhantomMode(false);
            }
        }

        #endregion

        #region Player special state

        void PhantomMode(bool p_active)
        {
            this.gameObject.layer = LayerMask.NameToLayer(p_active ? "ThroughObstacles" : "Default"); 
        }

        #endregion
    }
}