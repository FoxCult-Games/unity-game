using System.ComponentModel;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Stats")]
        [Range(1, 10)]
        [SerializeField] private int maxHealth;
        [Description("The amount of time player is invulnerable after taking damage")]
        [SerializeField] private float insensitivityTime;
        
        [Header("Movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private AnimationCurve smoothMovement;
        [SerializeField] private float bounceForce;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float fallMultiplier;
        [SerializeField] private float lowJumpMultiplier;
        
        [Header("Ground Check")]
        [SerializeField] private float groundedDistance;
        [SerializeField] private float groundedCheckThreshold;
        [SerializeField] private LayerMask raycastMask;

        [Header("VFX")]
        [SerializeField] private GameObject jumpParticles;
        
        [Header("SFX")]
        [SerializeField] private AudioClip jumpSound;

        public int MaxHealth => maxHealth;
        public float InsensitivityTime => insensitivityTime;
        
        public float MovementSpeed => movementSpeed;
        public AnimationCurve SmoothMovementSpeed => smoothMovement;
        public float BounceForce => bounceForce;
        
        public float JumpForce => jumpForce;
        public float JumpCooldown => jumpCooldown;
        public float FallMultiplier => fallMultiplier;
        public float LowJumpMultiplier => lowJumpMultiplier;
        
        public float GroundedDistance => groundedDistance;
        public float GroundedCheckThreshold => groundedCheckThreshold;
        public LayerMask RaycastMask => raycastMask;
        
        public GameObject JumpParticles => jumpParticles;
        
        public AudioClip JumpSound => jumpSound;
    }
}
