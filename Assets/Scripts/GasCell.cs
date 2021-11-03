using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GasCell: MonoBehaviour
    {

        public void Unclock()
        {
            transform.parent = null;
            gameObject.AddComponent<Rigidbody>();
        }
    }
}