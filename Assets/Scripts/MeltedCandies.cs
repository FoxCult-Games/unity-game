using UnityEngine;

public class MeltedCandies : MonoBehaviour
{
    [SerializeField] private GameObject onHitParticles;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage( transform.position - col.transform.position, 1);
            
            GameObject particlesObject = Instantiate(onHitParticles, col.transform.position + (Vector3.down * 0.75f), Quaternion.identity);
            particlesObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
