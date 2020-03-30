using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cathapult  : MonoBehaviour
{

    #region Variables
    private SpawnPool spawnProjectile;
    [SerializeField] Rigidbody2D sling;
    [SerializeField] bool debugLoad;
    [SerializeField] GameObject rightHook;
    bool setPrm;
    Vector3 rightHookPos;
    Vector3 lefttHookPos;
    LineRenderer lhLineRenderer;
    LineRenderer rhLineRenderer;
    #endregion

    IEnumerator Start()
    {
        if (!setPrm)
        {
            setPrm = true;
            rightHookPos = rightHook.transform.position;
            lefttHookPos = sling.transform.position;
            rhLineRenderer = rightHook.GetComponent<LineRenderer>();
            lhLineRenderer = sling.GetComponent<LineRenderer>();
            rightHookPos.z = -0.7f;
            rightHook.transform.position = rightHookPos;
        }
        yield return new WaitForSeconds(0.5f);
        spawnProjectile = GetComponent<SpawnPool>();
        GetComponent<Defender>().SetType(3);
        LoadProjectile();
    }


    #region Costom Methods

    public void LoadProjectile()
    {
        Vector2 slingPos = sling.position;
        Transform projectilePos = spawnProjectile.GenerateFromPool(slingPos, Quaternion.identity);
        GameObject projectile = projectilePos.gameObject;
        SpringJoint2D projectileSpring = projectile.GetComponent<SpringJoint2D>();
        projectileSpring.connectedBody = sling;
        projectileSpring.enabled = true;
        rhLineRenderer.enabled = true;
        lhLineRenderer.enabled = true;
    }

    public void DrawStrips(Vector3 RightPos, Vector3 LeftPos)
    {
        Vector3[] rPos = new Vector3[2];
        Vector3[] lPos = new Vector3[2];
        rPos[1] = rightHookPos;
        rPos[0] = RightPos;
        lPos[1] = lefttHookPos;
        lPos[0] = LeftPos;
        rhLineRenderer.SetPositions(rPos);
        lhLineRenderer.SetPositions(lPos);
    }
    public void SetOffStrings()
    {
        rhLineRenderer.enabled = false;
        lhLineRenderer.enabled = false;
    }
    #endregion
}