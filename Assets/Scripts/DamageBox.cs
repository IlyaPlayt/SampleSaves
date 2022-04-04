using UnityEngine;

namespace DefaultNamespace
{
    public class DamageBox : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            PlayerParameters.Instance.GetDamage(50);
            gameObject.SetActive(false);
        }
    }
}