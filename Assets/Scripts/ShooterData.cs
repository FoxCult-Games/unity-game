using UnityEngine;

[CreateAssetMenu(fileName = "New Shooter Data", menuName = "Data/Shooter Data")]
public class ShooterData : ScriptableObject
{
    [SerializeField] private float fireRate;
    [SerializeField] private float fireRange;
        
    public float FireRate => fireRate;
    public float FireRange => fireRange;
}