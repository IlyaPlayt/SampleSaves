using UnityEngine;


    public abstract class Metal : MonoBehaviour,ICollectable
    {
        protected Inventory.Metal _cindOfMetal;
        public void Collect()
        {
            Inventory.Instance.AddResource(_cindOfMetal);
            PlayerParameters.Instance.AddExp(_cindOfMetal);
            gameObject.SetActive(false);
        }
        protected void FixedUpdate()
        {
            transform.Rotate(Vector3.up,5);
        }

    }
