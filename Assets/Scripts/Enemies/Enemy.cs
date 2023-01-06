using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour, IEnemy, IDamageable
    {
        [SerializeField] protected GameObject obstacleCheck;
        [SerializeField] protected GameObject groundedCheck;
        protected Rigidbody2D rb;
        
        [Space(16)]
        
        [SerializeField] protected EnemyData enemyData;
        
        [Space(8)]
        
        [SerializeField] protected Transform[] waypoints;
        
        [Space(16)]

        public UnityEvent<int> onWaypointReached;
        public UnityEvent<int, Vector2> onDamageTaken;
        public UnityEvent onDeath;

        private int health;
        
        private int currentWaypointIndex;
        private bool isGrounded;
        private bool canMove = true;
        
        private float lastJumpTime = -9999f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            if(waypoints.Length > 0) 
                transform.position = waypoints[currentWaypointIndex].position;
            health = enemyData.MaxHealth;
        }

        protected virtual void Update()
        {
            if(rb.velocity.y < 0)
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (enemyData.FallMultiplier - 1) * Time.deltaTime);
            else if(rb.velocity.y > 0)
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (enemyData.LowJumpMultiplier - 1) * Time.deltaTime);
            
            isGrounded = Physics2D.Raycast(groundedCheck.transform.position, Vector2.down, enemyData.GroundCheckDistance, enemyData.GroundLayerMask);
            
            if(waypoints.Length == 0) return;
            
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.2f) 
                ChangeWaypoint();
            
            Move();
        }

        public void Move()
        {
            if (!canMove) return;
            
            transform.localScale = new Vector3(GetDirection(), 1, 1);
            
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, enemyData.Speed * Time.deltaTime);
            
            if(Physics2D.Raycast(obstacleCheck.transform.position, Vector2.right * GetDirection(), enemyData.ObstacleCheckDistance, enemyData.ObstacleLayerMask) && isGrounded && Time.time - lastJumpTime > enemyData.JumpCooldown)
                Jump();
        }

        public void Jump()
        {
            rb.AddForce(Vector2.up * enemyData.JumpForce);

            Vector3 particlesPosition = transform.position + Vector3.down;
            
            GameObject particlesObject = Instantiate(enemyData.JumpParticles, particlesPosition, Quaternion.identity);
            particlesObject.GetComponent<ParticleSystem>().Play();
            
            lastJumpTime = Time.time;
        }

        public void ChangeWaypoint()
        {
            if(waypoints.Length == 0) return;
            
            onWaypointReached.Invoke(currentWaypointIndex);
            
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        public void AddWaypoint()
        {
            GameObject waypoint = Instantiate(Resources.Load<GameObject>("Prefabs/Waypoint"), transform.position, Quaternion.identity, transform);
            
            List<Transform> waypointsList = waypoints.ToList();
            waypointsList.Add(waypoint.transform);
            waypoints = waypointsList.ToArray();
            
            Selection.activeGameObject = waypoint;
            SceneView.FrameLastActiveSceneView();
        }

        public void TakeDamage(Vector2 direction, int damage)
        {
            health -= damage;
            
            onDamageTaken?.Invoke(damage, direction);
            
            if (health <= 0) Die();
        }

        public void Die()
        {
            onDeath?.Invoke();
        }

        private int GetDirection()
        {
            return transform.position.x < waypoints[currentWaypointIndex].position.x ? 1 : -1;
        }
        
        public void WaitAtWaypoint(int waypointIndex)
        {
            StartCoroutine(IEWaitAtWaypoint(waypointIndex));
        }
        
        private IEnumerator IEWaitAtWaypoint(int waypointIndex)
        {
            yield return new WaitForSeconds(enemyData.WaitTime);
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        public void EnableMovement()
        {
            canMove = true;
        }
        
        public void DisableMovement()
        {
            canMove = false;
        }

        private void OnDrawGizmos()
        {
            if(waypoints.Length == 0) return;
            
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(waypoints[i].position, 0.5f);
                
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
            }
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(obstacleCheck.transform.position, Vector3.right * GetDirection() * enemyData.ObstacleCheckDistance);
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(groundedCheck.transform.position, Vector3.down * enemyData.GroundCheckDistance);
        }
    }
}
