using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 5f;
    [SerializeField]
    private float m_maxSpeed = 30f;
    [SerializeField]
    private float m_dashSpeed = 15f;
    [SerializeField]
    private float m_dashDuration = 0.2f;
    [SerializeField]
    private float m_dashCooldown = 1f;
    [SerializeField]
    private float m_redirectionSpeed = 0.05f;
    [SerializeField]
    private bool m_useKinematicDash = false;

    private Rigidbody2D m_rb;
    private Vector2 m_moveDirection;
    private bool m_isDashing = false;
    private float m_dashTime;
    private float m_dashCooldownTime;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

    void HandleInput()
    {
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");
        float moveX = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        float moveY = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        m_moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= m_dashCooldownTime)
        {
            StartDash();
        }
    }

    void Move()
    {
        if(m_rb.velocity.magnitude>m_maxSpeed)
            m_rb.velocity = m_rb.velocity.normalized * m_maxSpeed;
        if(m_moveDirection != Vector2.zero)
            m_rb.AddForce(m_moveDirection * m_moveSpeed);// = Vector2.Lerp(m_rb.velocity, m_moveDirection * m_moveSpeed, m_redirectionSpeed);
    }

    void StartDash()
    {
        m_isDashing = true;
        m_dashTime = Time.time + m_dashDuration;
        m_dashCooldownTime = Time.time + m_dashCooldown;
        Vector2 l_dashDirection = m_moveDirection != Vector2.zero ? m_moveDirection : m_rb.velocity.normalized;
        m_rb.velocity = l_dashDirection * m_dashSpeed;
        if(m_useKinematicDash)
            m_rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void HandleDash()
    {
        if (m_isDashing && Time.time >= m_dashTime)
        {
            m_isDashing = false;
            m_rb.velocity = m_rb.velocity.normalized * m_moveSpeed;
            m_rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}