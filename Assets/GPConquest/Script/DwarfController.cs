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
        if (Input.GetKeyDown(KeyCode.UpArrow))
            networkObject.SendRpc(RPC_MOVE, Receivers.AllBuffered, Vector3.up);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            networkObject.SendRpc(RPC_MOVE, Receivers.AllBuffered, Vector3.down);

    }

    public override void Move(RpcArgs args)
    {
        MainThreadManager.Run(() =>
        {
            transform.position += args.GetNext<Vector3>();
        });
    }
}
