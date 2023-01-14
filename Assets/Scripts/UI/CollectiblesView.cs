using TMPro;
using UnityEngine;

namespace UI
{
    public interface IViewContext
    {
        void Refresh();
    }
    
    public class CollectiblesView : MonoBehaviour, IViewContext
    {
        [SerializeField] private TextMeshProUGUI counter;


        public void Refresh()
        {
            counter.text = "0";
        }
    }
}
