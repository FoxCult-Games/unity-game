using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class CharacterController2D : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData playerData;
        private PlayerInputs playerInputs;

        private Rigidbody2D rb;
        private Animator animator;
        private new Collider2D collider;

        [SerializeField] private bool isGrounded;

        public UnityEvent<int, Vector2> onDamaged;
        public UnityEvent onJump;

        private int health;
        public int Health => health;
        
        private float move;
        private float moveTime;
        private float lastJumpTime = -9999f;
        private float lastDamagedTime = -9999f;
        private bool canJump;
        private float currentGroundedThreshold;
        
        private readonly int jumpingAnimation = Animator.StringToHash("Jumping");
        private readonly int walkingAnimation = Animator.StringToHash("Walking");

        private void Awake()
        {
            playerInputs = new PlayerInputs();

            playerInputs.Gameplay.Movement.performed += GetMovement;
            playerInputs.Gameplay.Jump.performed += Jump;

            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider2D>();

            onDamaged.AddListener(BounceOnDamage);
        }

        private void Start()
        {
            health = playerData.MaxHealth;
            currentGroundedThreshold = playerData.GroundedCheckThreshold;
        }

        private void Update()
        {
            Move();

            if(rb.velocity.y < 0) 
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (playerData.FallMultiplier - 1) * Time.deltaTime);
            else if(rb.velocity.y > 0 && !playerInputs.Gameplay.Jump.ReadValue<float>().Equals(1)) 
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (playerData.LowJumpMultiplier - 1) * Time.deltaTime);

            if (!IsGrounded()) currentGroundedThreshold -= Time.deltaTime;
            else currentGroundedThreshold = playerData.GroundedCheckThreshold;
            
            canJump = !(currentGroundedThreshold <= 0) && Time.time - lastJumpTime > playerData.JumpCooldown;
        }

        private void GetMovement(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<float>();
        }

        private void Move()
        {
            if (move == 0f)
            {
                moveTime = 0f;
                return;
            }

            float speed = playerData.MovementSpeed * playerData.SmoothMovementSpeed.Evaluate(moveTime) * move;
            Vector2 offset = new Vector2(speed * Time.deltaTime, 0f);

            transform.localScale = new Vector3(move < 0 ? 1 : -1, 1, 1);

            animator.SetTrigger(walkingAnimation);
            transform.position += (Vector3)offset;
            moveTime += Time.deltaTime;
        }

        private void Jump(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed || !canJump) return;

            if(isGrounded) animator.SetTrigger(jumpingAnimation);

            rb.AddForce(Vector2.up * playerData.JumpForce);
            
            currentGroundedThreshold = playerData.GroundedCheckThreshold;
            lastJumpTime = Time.time;

            onJump?.Invoke();
        }

        private bool IsGrounded()
        {
            Bounds bounds = collider.bounds;
            return Physics2D.BoxCast(bounds.center, bounds.size * 0.75f, 0f, Vector2.down, playerData.GroundedDistance, playerData.RaycastMask);
        }

        public void TakeDamage(Vector2 direction, int damage = 1)
        {
            if(!IsSensitive()) return;
            
            health -= damage;
            
            lastDamagedTime = Time.time;
            lastJumpTime = Time.time;
            
            onDamaged?.Invoke(damage, direction);
            
            if (health <= 0) Die();
        }

        private void BounceOnDamage(int damage, Vector2 direction)
        {
            rb.AddForce(new Vector2(0, -direction.y) * playerData.BounceForce, ForceMode2D.Impulse);
        }

        private bool IsSensitive()
        {
            return Time.time - lastDamagedTime > playerData.InsensitivityTime;
        }

        public void PlayJumpingParticles()
        {
            Vector3 particlesPosition = transform.position + Vector3.down;

            GameObject particlesObject = Instantiate(playerData.JumpParticles, particlesPosition, Quaternion.identity);
            particlesObject.GetComponent<ParticleSystem>().Play();
        }

        public void Die()
        {
            Debug.Log("Player died");
        }

        private void OnEnable()
        {
            playerInputs.Enable();
        }

        private void OnDisable()
        {
            playerInputs.Disable();
        }

        private void OnDrawGizmos()
        {
            if (!collider) return;
            
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Bounds bounds = collider.bounds;
            
            Gizmos.DrawRay(bounds.center + new Vector3(bounds.extents.x, 0), Vector2.down * (bounds.extents.y + playerData.GroundedDistance));
            Gizmos.DrawRay(bounds.center - new Vector3(bounds.extents.x, 0), Vector2.down * (bounds.extents.y + playerData.GroundedDistance));
            Gizmos.DrawRay(bounds.center - new Vector3(bounds.extents.x, bounds.extents.y + playerData.GroundedDistance), Vector2.down * (bounds.extents.y + playerData.GroundedDistance));
        }
    }
}
