using UnityEngine;

[CreateAssetMenu(fileName = "New Shooter Data", menuName = "Data/Shooter Data")]
public class ShooterData : ScriptableObject
{
    [SerializeField] private float fireRate;
        
    public float FireRate => fireRate;
}