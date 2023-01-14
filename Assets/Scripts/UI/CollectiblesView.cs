using TMPro;
using UnityEngine;
using Managers;

namespace UI
{
    using Unity.VisualScripting;

    public interface ICollectiblesView
    {
        void Refresh();
    }
    
    public class CollectiblesView : MonoBehaviour, ICollectiblesView, ISubManager
    {
        [SerializeField] private CollectiblesTypes collectibleType;
        [SerializeField] private TextMeshProUGUI counter;

        private IGameContext gameContext;
        
        public CollectiblesTypes CollectibleType => collectibleType;
        
        public void Initialize(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            
            Refresh();
        }

        public void Refresh()
        {
            counter.text = gameContext.CollectiblesController.GetCollectedProgress(CollectiblesTypes.ICE_CREAM).ToString();
        }
    }
}
