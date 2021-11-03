using UnityEngine;

namespace DefaultNamespace
{
    public class Parachute: MonoBehaviour
    {

        public void ShowMesh()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}