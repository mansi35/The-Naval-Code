using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead;
    public bool isDead
    {
        get {return _isDead; }
        protected set { _isDead = value;}
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void PlayerSetup ()
    {
        GameManager.instance.SetSceneCameraActive(false);
        GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);

        CmdBroadcastNewPlayerSetup();
    }

    [Command]
    private void CmdBroadcastNewPlayerSetup ()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients ()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    // void Update() {
    //     if (!isLocalPlayer)
    //         return;
        
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         RpcTakeDamage(999);
    //     }
    // }
    
    [ClientRpc]
    public void RpcTakeDamage (int _amount)
    {
        if (isDead)
            return;
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

        Debug.Log(transform.name + " is DEAD!");

        // CALL RESPAWN METHOD
    }

    public void SetDefaults ()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }

}
