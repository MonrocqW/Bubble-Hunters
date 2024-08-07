using UnityEngine;

namespace BubHun.Players.Movement
{
    public class BubbleMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerStats m_playerStats;
        [SerializeField]
        private float m_maxSpeed = 30f;

        private Rigidbody2D m_rb;
        private Vector2 m_moveDirection;
        private bool m_isDashing = false;
        private float m_dashTime;
        private float m_dashCooldownStartTime = 0;
        private int m_storedDashes = 1;

        #region Unity

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            RechargeDash();
            HandleInput();
            HandleDash();
        }

        void FixedUpdate()
        {
            if (!m_isDashing)
            {
                Move();
            }
        }

        #endregion

        #region Inputs

        void HandleInput()
        {
            //float moveX = Input.GetAxis("Horizontal");
            //float moveY = Input.GetAxis("Vertical");
            float moveX = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            float moveY = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
            m_moveDirection = new Vector2(moveX, moveY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) && m_storedDashes>0)
            {
                StartDash();
            }
        }

        #endregion

        #region Base Movement

        void Move()
        {
            if(m_rb.velocity.magnitude>m_maxSpeed)
                m_rb.velocity = m_rb.velocity.normalized * m_maxSpeed;
            if(m_moveDirection != Vector2.zero)
                m_rb.AddForce(m_moveDirection * m_playerStats.MoveSpeed);// = Vector2.Lerp(m_rb.velocity, m_moveDirection * m_moveSpeed, m_redirectionSpeed);
        }

        #endregion

        #region Dash

        void RechargeDash()
        {
            if(m_storedDashes == m_playerStats.DashNumber)
                return;
            
            if(Time.time > m_dashCooldownStartTime + m_playerStats.DashCooldownTime)
            {
                m_storedDashes++;
                m_dashCooldownStartTime = Time.time;
            }
        }

        void StartDash()
        {
            m_isDashing = true;
            if(m_storedDashes == m_playerStats.DashNumber)
                m_dashCooldownStartTime = Time.time;
            m_storedDashes--;
            m_dashTime = Time.time + m_playerStats.DashDurationTime;
            Vector2 l_dashDirection = m_moveDirection != Vector2.zero ? m_moveDirection : m_rb.velocity.normalized;
            m_rb.velocity = l_dashDirection * m_playerStats.DashSpeed;
            if(m_playerStats.PhantomDash)
                this.PhantomMode(true);
        }

        void HandleDash()
        {
            if (m_isDashing && Time.time >= m_dashTime)
            {
                m_isDashing = false;
                m_rb.velocity = m_rb.velocity.normalized * m_playerStats.MoveSpeed;
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