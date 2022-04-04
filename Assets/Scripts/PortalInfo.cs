using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class PortalInfo : ScriptableObject
    {
        [SerializeField] private int playerLevelToOpenPortal;
        [SerializeField] private string nextLevelName;

        public int PlayerLevelToOpenPortal => playerLevelToOpenPortal;

        public string NextLevelName => nextLevelName;
    }
}