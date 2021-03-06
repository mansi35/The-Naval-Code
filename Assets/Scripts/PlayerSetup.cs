﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
	public GameObject playerUIInstance;

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        } 
        else
        {
            // Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
                Debug.Log("No PlayerUi component on playerUIPrefab");
            ui.SetPlayer(GetComponent<Player>());
            playerUIInstance.GetComponent<submitOnclick>().SetPlayerPrefab(GetComponent<Player>());

            GetComponent<Player>().PlayerSetup();

            string _username = "Loading...";
			if (UserAccountManager.IsLoggedIn)
				_username = UserAccountManager.LoggedIn_Username;
			else
				_username = transform.name;

			CmdSetUsername(transform.name, _username);
        }

    }

    [Command]
	void CmdSetUsername (string playerID, string username)
	{
		Player player = GameManager.GetPlayer(playerID);
		if (player != null)
		{
			Debug.Log(username + " has joined!");
			player.username = username;
		}
	}

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents ()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable() 
    {
        Destroy(playerUIInstance);
        if (isLocalPlayer)
            GameManager.instance.SetSceneCameraActive(true);

        GameManager.UnRegisterPlayer(transform.name);
    }

}
