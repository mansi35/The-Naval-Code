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

    public Transform shootPoint1;
    public Transform shootPoint2;
    private Vector3 Vo;
    private GameObject obj;
    public GameObject bulletPrefabs;

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
            if (Input.mousePosition.x > Screen.width / 2)
            {
                Vo = CalculateVelocity(hit.point, shootPoint1.position, 2f);
            }
            else
            {
                Vo = CalculateVelocity(hit.point, shootPoint2.position, 2f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                CmdFire(Vo);
            }
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
        // _player.RpcTakeDamage(_damage);
    }

    [Command]
    void CmdFire(Vector3 Vo)
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            obj = (GameObject)Instantiate(bulletPrefabs, shootPoint1.position, Quaternion.identity);
            shootPoint1.transform.rotation = Quaternion.LookRotation(Vo);
        }
        else
        {
            obj = (GameObject)Instantiate(bulletPrefabs, shootPoint2.position, Quaternion.identity);
            shootPoint2.transform.rotation = Quaternion.LookRotation(Vo);
        }
        obj.GetComponent<Rigidbody>().velocity = Vo;

        NetworkServer.Spawn(obj);
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (isLocalPlayer)
        {
            if (collisionInfo.gameObject.tag == "cannon")
            {
                GetComponent<PlayerSetup>().playerUIInstance.GetComponent<PlayerUI>().ActivateQuestion();
                Destroy(collisionInfo.gameObject, 1f);
            }
        }
    }

}
