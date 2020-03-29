using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultCoursor : MonoBehaviour
{
    GameObject coursor;
    GameObject enemySprawner;
    SpawnAttackers sprawner;
    bool _isProjectilePressed;
    public bool IsProjectilePressed
    {
        set
        {
            _isProjectilePressed = value;
            if (_isProjectilePressed)
            {
            UnsetAnimation();
            }
        }
    }


    void Start()
    {
        coursor = transform.Find("Cursor").gameObject;
        enemySprawner = FindLayer("Sprawner", Vector2.right);
        sprawner = enemySprawner.GetComponent<SpawnAttackers>();
        sprawner.AttackerSpawned += SetAnimation;
        sprawner.AttackersRanOut += UnsetAnimation;
        if(!CheckLayer("Enemy", Vector2.right))
        {
            coursor.SetActive(false);
        }
        
    }

    private void UnsetAnimation()
    {
        coursor.SetActive(false);
    }

    private void SetAnimation()
    {
        if (!_isProjectilePressed)
        {
            coursor.SetActive(true);
        }
    }

    private GameObject FindLayer(string layerName, Vector2 direction)
    {
        LayerMask mask = LayerMask.GetMask(layerName);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,
            Mathf.Infinity, mask, -Mathf.Infinity, Mathf.Infinity);
        return hit.transform.gameObject;
    }

    private bool CheckLayer(string layerName, Vector2 direction)
    {
        LayerMask mask = LayerMask.GetMask(layerName);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,
            Mathf.Infinity, mask, -Mathf.Infinity, Mathf.Infinity);
        if (hit)
        {
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        sprawner.AttackerSpawned -= SetAnimation;
        sprawner.AttackersRanOut -= UnsetAnimation;
    }

    
}
