using UnityEngine;

namespace AlyxAdventure
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
      
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}