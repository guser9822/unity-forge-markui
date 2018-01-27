using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace TC.GPConquest {

    public class GameController : MonoBehaviour
    {
               
        // Use this for initialization
        void Start()
        { 
            NetworkManager.Instance.InstantiateDwarf(0, new Vector3(10, 10, 10));
        }

    }

}