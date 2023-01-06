using UnityEngine;

public interface IDamageable
{
    void TakeDamage(Vector2 direction, int damage);
    void Die();
}