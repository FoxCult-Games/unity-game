using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy")]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy Stats")]
        [Range(1, 5)]
        [SerializeField] private int maxHealth;
        
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private float waitTime;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;

        [SerializeField] private float jumpCooldown;
        [SerializeField] private float fallMultiplier;
        [SerializeField] private float lowJumpMultiplier;
        
        [Header("Obstacle Check")]
        [SerializeField] private float obstacleCheckDistance;
        [SerializeField] private LayerMask obstacleLayerMask;
        
        [Header("Ground Check")]
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayerMask;
        
        [Header("VFX")]
        [SerializeField] private GameObject jumpParticles;
        
        public int MaxHealth => maxHealth;
        
        public float Speed => speed;
        public float WaitTime => waitTime;
        
        public float JumpForce => jumpForce;
        public float JumpCooldown => jumpCooldown;
        public float FallMultiplier => fallMultiplier;
        public float LowJumpMultiplier => lowJumpMultiplier;
        
        public float ObstacleCheckDistance => obstacleCheckDistance;
        public LayerMask ObstacleLayerMask => obstacleLayerMask;
        
        public float GroundCheckDistance => groundCheckDistance;
        public LayerMask GroundLayerMask => groundLayerMask;
        
        public GameObject JumpParticles => jumpParticles;
    }
}