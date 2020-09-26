// using System.ComponentModel;
// using UnityEngine;
// using UnityEngine.Networking;

// public class NetworkUI : MonoBehaviour
// {
//     [SerializeField]
//     private NetworkManager manager;

//     public void HostGame()
//     {
//         if (!NetworkClient.active)
//         {
//             // Server + Client
//             manager.StartHost();
//         }
//     }

//     public void JoinGame()
//     {
//         if (!NetworkClient.active)
//         {
//             // Client + IP
//             manager.StartClient();
//         }
//     }

//     public void ServerOn()
//     {
//         manager.StartServer();
//     }
// }

using System.ComponentModel;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkUI : MonoBehaviour
{
    [SerializeField]
    private NetworkManager manager;
    public void StartHostButton()
    {
        manager.StartHost();
    }

    //Press the "Disconnect" Button to stop the Host
    // public void StopHostButton()
    // {
    //     manager.StopHost();
    //     manager.StopClient();
    // }

    public void StartClientButton()
    {
        manager.StartClient();
    }

    public void StartServerButton()
    {
        manager.StartServer();
    }
}