using UnityEngine;

namespace Collectibles
{
    [CreateAssetMenu(fileName = "New Collectibles Data", menuName = "Data/Collectibles Data", order = 3)]
    public class CollectiblesData : ScriptableObject
    {
        [SerializeField] private GameObject collectedParticles;
        
        public GameObject CollectedParticles => collectedParticles;
    }
}