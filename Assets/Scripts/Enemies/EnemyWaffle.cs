using System;
using Enemies;
using UnityEngine;

namespace Enemies
{
    public class EnemyWaffle : Enemy
    {
        private Vector2 startingPosition;
        private Vector2 landingPosition;

        private bool hasLanded;

        protected override void Start()
        {
            base.Start();

            Fall();

            startingPosition = transform.position;
            rb.isKinematic = true;
        }

        protected override void Update()
        {
            base.Update();

            if (!hasLanded && Vector2.Distance(transform.position, landingPosition) < 0.2f)
            {
                rb.isKinematic = false;
                hasLanded = true;
            }
        }

        private void Fall()
        {
            if (hasLanded) return;
            
            float distance = Vector2.Distance(landingPosition, startingPosition);

            float tanAngle = Mathf.Tan(60f * Mathf.Deg2Rad);
            float height = landingPosition.y - transform.position.y;

            float Vz = Mathf.Sqrt(Physics2D.gravity.y * distance * distance / (2f * (height - distance * tanAngle)) );
            float Vy = Vz * tanAngle;

            Vector2 localVelocity = new Vector2(-Vz, Vy);
            Vector2 globalVelocity = transform.TransformDirection(localVelocity);

            rb.velocity = globalVelocity;
        }

        public void SetLandingPosition(Vector2 position)
        {
            landingPosition = position;
        }
        
        private Vector2 GetDirection() => landingPosition - (Vector2)transform.position;
    }
}
