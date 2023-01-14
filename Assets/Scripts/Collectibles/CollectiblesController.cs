using System.Collections.Generic;
using Managers;

namespace Collectibles
{
    using UnityEngine;

    public interface ICollectiblesController
    {
        int Collect(CollectiblesTypes collectible, int amount);
        int GetCollectedProgress(CollectiblesTypes collectible);
    }
    
    public class CollectiblesController : ICollectiblesController
    {
        private readonly Dictionary<CollectiblesTypes, int> collected;

        private IGameContext gameContext;
    
        public CollectiblesController(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            
            collected = new Dictionary<CollectiblesTypes, int>();
            
            InitializeCollectibles();
        }

        private void InitializeCollectibles()
        {
            foreach (CollectiblesTypes collectible in System.Enum.GetValues(typeof(CollectiblesTypes)))
            {
                collected.Add(collectible, 0);
            }
        }

        public int Collect(CollectiblesTypes collectible, int amount)
        {
            if (!collected.ContainsKey(collectible))
                collected.Add(collectible, amount);

            collected[collectible] += amount;
            
            gameContext.UIManager.RefreshCollectiblesCounter(collectible);
            return collected[collectible];
        }

        public int GetCollectedProgress(CollectiblesTypes type)
        {
            return collected[type];
        }
    }
}
