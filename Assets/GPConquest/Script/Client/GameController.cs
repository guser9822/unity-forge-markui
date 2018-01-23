//using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
//using BeardedManStudios.SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // Use this for initialization
    void Start () {
        NetworkManager.Instance.InstantiateDwarf(0,new Vector3(10,10,10));
        //NetworkManager.Instance.InstantiateDwarfNetworkObject();
    }

}
