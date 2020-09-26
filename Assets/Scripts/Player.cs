using UnityEngine;
using System.Collections;
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
    public int currentHealth;

    [SerializeField]
    private int maxCoins = 200;

    [SyncVar]
    public int currentCoins;

    [SerializeField]
    private int recievedCoins;
    [SerializeField]
    private int recievedHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SyncVar]
	public string username = "Loading...";

    public float GetHealthPct ()
	{
		return (float)currentHealth / maxHealth;
	}

    void OnReceivedData (string data)
	{
		if (currentHealth == null ||currentCoins == null)
			return;

		recievedHealth = DataTranslator.DataToHealth(data);
		recievedCoins = DataTranslator.DataToCoins(data);
        CmdSetDamage(maxHealth - recievedHealth);
        CmdSetCoins(recievedCoins);
	}

    public void PlayerSetup ()
    {
        GameManager.instance.SetSceneCameraActive(false);
        GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);

        if (UserAccountManager.IsLoggedIn)
			UserAccountManager.instance.GetData(OnReceivedData);

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

    IEnumerator waiter ()
	{
		yield return new WaitForSeconds(5f);
	}

    // void Update() {
    //     if (!isLocalPlayer)
    //         return;
        
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         RpcTakeDamage(999);
    //     }
    // }

    [Command]
    public void CmdSetDamage(int _amount)
    {
        RpcTakeDamage(_amount);
    }
    
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

    [Command]
    public void CmdSetCoins (int _amount)
    {
        RpcTakeCoins(_amount);
    }
    
    [ClientRpc]
    public void RpcTakeCoins (int _amount)
    {
        currentCoins += _amount;

        Debug.Log(transform.name + " now has " + currentCoins + " coins.");

    }

    public void SetDefaults ()
    {

        isDead = false;

        currentHealth = maxHealth;

        currentCoins = 0;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }

}
