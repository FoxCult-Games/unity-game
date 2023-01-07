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
            
            startingPosition = transform.position;
            rb.isKinematic = true;
        }

        protected override void Update()
        {
            base.Update();

            Fall();

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

            float nextX = Mathf.MoveTowards(transform.position.x, landingPosition.x, enemyData.Speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startingPosition.y, landingPosition.y, (nextX - startingPosition.x) / distance);

            float height = 2 * (nextX - startingPosition.x) * (nextX - landingPosition.x) / (-0.25f * distance * distance);
            
            Debug.Log("baseY + height: " + baseY + height);
            Debug.Log("baseY: " + baseY);
            Debug.Log("height: " + height);
            
            Vector2 nextPosition = new Vector2(nextX, baseY + height);
            
            transform.position = nextPosition;
            
            transform.up = nextPosition - (Vector2)transform.position;
        }

        public void SetLandingPosition(Vector2 position)
        {
            landingPosition = position;
        }
        
        private Vector2 GetDirection() => landingPosition - (Vector2)transform.position;
    }
}
