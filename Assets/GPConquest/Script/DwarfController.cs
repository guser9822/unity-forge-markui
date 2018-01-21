using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;
using BeardedManStudios.Forge.Networking.Unity;

public class DwarfController : DwarfBehavior {

    public string DwarfName;
    private string[] names = { "Turin", "Gimli", "Erizzoun Hillbuster", "Gaghout", "Axeforged","Tukhot","Barbedbraids"};
    public float speed = 6.0f;

    private void Awake()
    {
        DwarfName = names[(int)UnityEngine.Random.Range(0.0f, names.Length - 1)];
    }

    protected override void NetworkStart()
    {
        networkObject.SendRpc(RPC_INIT_UP_DWARF, Receivers.AllBuffered, DwarfName);   
    }

    public override void InitUpDwarf(RpcArgs args)
    {
        DwarfName = args.GetNext<string>();
    }

    private void Update()
    {
         // assign it to the position and rotation specified
         if (!networkObject.IsOwner)
        {
            transform.position = networkObject.netPosition;
            return;
        }
        // Get the movement based on the axis input values
        Vector3 translation = new Vector3(Input.GetAxis("Horizontal"),
        Input.GetAxis("Vertical"), 0);
        // Scale the speed to normalize for processors
        translation *= speed * Time.deltaTime;
        // Move the object by the given translation
        transform.position += translation;
        // Just a random rotation on all axis
        transform.Rotate(new Vector3(speed, speed, speed) * 0.25f);
        // Since we are the owner, tell the network the updated position
        networkObject.netPosition = transform.position;


    }

}
