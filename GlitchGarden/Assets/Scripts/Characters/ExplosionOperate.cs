using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOperate : MonoBehaviour
{

    [SerializeField] GameObject explosionType;
    SpawnPool explosionPool;

    void Start()
    {
        explosionPool = GetComponent<SpawnPool>();
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
}
