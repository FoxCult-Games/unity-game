using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyWaffleLauncher : Enemy, IShooter
    {
        [SerializeField] private GameObject wafflePrefab;
        [SerializeField] private Transform waffleSpawnPoint;
        
        [SerializeField] private ShooterData shooterData;

        public UnityEvent<Vector2> onShoot;
            
        private float lastFiredTime = -9999f;

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
            
            Shoot();
        }

        private bool IsPlayerInRange()
        {
            return true;
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
    }
}