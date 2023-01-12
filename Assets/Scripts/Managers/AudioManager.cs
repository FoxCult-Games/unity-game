using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour, ISubManager
    {
        private IGameContext gameContext;
        
        private AudioListener audioListener;
        
        public void Initialize(IGameContext gameContext)
        {
            this.gameContext = gameContext;

            audioListener = Camera.main!.GetComponent<AudioListener>();
        }
        
        public void PlaySound(AudioClip audioClip)
        {
            AudioSource.PlayClipAtPoint(audioClip, audioListener.transform.position);
        }
    }
}
