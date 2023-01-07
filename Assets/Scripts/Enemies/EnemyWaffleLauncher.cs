using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyWaffleLauncher : Enemy, IShooter
    {
        private Animator animator;

        [SerializeField] private GameObject wafflePrefab;
        [SerializeField] private Transform waffleSpawnPoint;
        
        [SerializeField] private ShooterData shooterData;

        public UnityEvent<Vector2> onShoot;
            
        private float lastFiredTime = -9999f;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            
            lastFiredTime = Time.time;
        }

        protected override void Update()
        {
            base.Update();

            if (!IsPlayerInRange()) return;
            
            if(Time.time - lastFiredTime < shooterData.FireRate) return;
            
            ShootAnimation();
        }

        private bool IsPlayerInRange()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            return Vector2.Distance(transform.position, player.position) <= shooterData.FireRange;
        }
        
        private void ShootAnimation()
        {
            animator.Play("Waffle_launcher_shoots");
        }

        public void Shoot()
        {
            GameObject waffleObject = Instantiate(wafflePrefab, waffleSpawnPoint.position, Quaternion.identity);
            
            EnemyWaffle waffle = waffleObject.GetComponent<EnemyWaffle>();
            Vector2 position = GameObject.FindWithTag("Player").transform.position;
            
            waffle.SetLandingPosition(position);
            onShoot?.Invoke(position);
            
            lastFiredTime = Time.time;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsPlayerInRange() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, shooterData.FireRange);
        }
    }
}