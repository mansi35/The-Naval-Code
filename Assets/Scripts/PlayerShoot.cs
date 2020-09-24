using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";
    
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start() {
        if (cam == null)
        {
            Debug.Log("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
    }

    void Update() {
        if (PauseGame.IsOn)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot ()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot (string _PlayerID, int _damage)
    {
        Debug.Log(_PlayerID + " has been shot.");

        Player _player = GameManager.GetPlayer(_PlayerID);
        _player.RpcTakeDamage(_damage);
    }

}
