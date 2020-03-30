using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOperate : MonoBehaviour
{

    #region Variables
    [SerializeField] GameObject explosionType;
    SpawnPool explosionPool;
    #endregion


    #region MonoBehaviour Methods

    void Start()
    {
        explosionPool = GetComponent<SpawnPool>();
        Health.SayImDead += Health_SayImDead;
        CatapultProjectile.SayImDead += CatapultProjectile_SayImDead;
    }

    private void CatapultProjectile_SayImDead(CatapultProjectile obj)
    {
        Explode(obj.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Health.SayImDead -= Health_SayImDead;
        CatapultProjectile.SayImDead -= CatapultProjectile_SayImDead;
    }

    #endregion

    #region Costom Methods
    private void Health_SayImDead(Health obj)
    {
        Explode(obj.transform.position, Quaternion.identity);
    }

    public void Explode(Vector2 position, Quaternion rotation)
    {
        Transform expltransform = explosionPool.GenerateFromPool(position, rotation);
        StartCoroutine(StopExplosion(expltransform.gameObject));
    }

    private IEnumerator StopExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(2F);
        explosion.SetActive(false);        
    }
    #endregion
}
