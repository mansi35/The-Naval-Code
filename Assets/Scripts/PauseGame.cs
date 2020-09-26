using UnityEngine;
using UnityEngine.Networking;

public class PauseGame : MonoBehaviour {

	public static bool IsOn = false;

	private NetworkManager networkManager;

	void Start ()
	{
		networkManager = NetworkManager.singleton;
	}

	public void LeaveRoom ()
	{
		networkManager.StopHost();
    }

}