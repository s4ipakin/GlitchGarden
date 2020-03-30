
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System;

public class CatapultProjectile : ProjectileBase, IProjectile
{

    #region Variables
    [SerializeField] float damage = 100;
    public float Damage { get { return damage; } }
    [SerializeField]   
    float maxDistance = 2;
    [SerializeField]
    GameObject Explosion;
    bool isPressed;
    [SerializeField] GameObject rifhtPoint;
    [SerializeField] GameObject leftPoint;
    [SerializeField] GameObject pointer;
    Rigidbody2D thisRigidbody;
    Rigidbody2D slingRigidbody;
    SpringJoint2D thisSpring;
    Vector3 rightPos;
    Vector3 leftPos;
    Cathapult myCatapult;
    float DelayToRelease;
    bool isShot;
    bool isActiveOld;
    float haitOld;
    CatapultCoursor catapultCoursor;
    public static event Action<CatapultProjectile> SayImDead;
    #endregion


    #region MonoBehaviour Methods

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisSpring = GetComponent<SpringJoint2D>();
        slingRigidbody = thisSpring.connectedBody;
        catapultCoursor = slingRigidbody.gameObject.GetComponentInParent<CatapultCoursor>();
        DelayToRelease = 1 / (thisSpring.frequency * 5);
        myCatapult = GetComponentInParent<Cathapult>();
    }

    protected override void Update()
    {
        if (isPressed)
        {
            DragBall();
        }
        if (isShot)
        {
            SwitchOff();
        }
        else
        {
            myCatapult.DrawStrips(rifhtPoint.transform.position, leftPoint.transform.position);
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent(typeof(Enemy));
        if (enemy)
        {
            if ((enemy.gameObject.transform.position.y < slingRigidbody.position.y + 0.5)
                && (enemy.gameObject.transform.position.y > slingRigidbody.position.y - 0.5) && isShot)
            {
                SetDamage(enemy.gameObject, damage);
            }
        }
    }


    private void OnMouseDown()
    {
        isPressed = true;
        thisRigidbody.isKinematic = true;
        catapultCoursor.IsProjectilePressed = true;
        Debug.Log("Click");
    }

    private void OnMouseUp()
    {
        isPressed = false;
        thisRigidbody.isKinematic = false;
        StartCoroutine(ProjectileRelease());
        myCatapult.SetOffStrings();
        catapultCoursor.IsProjectilePressed = false;
    }


    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.z = -0.2f;
        transform.position = position;
    }

    #endregion


    #region Costom Methods

    private void SwitchOff()
    {
        if ((transform.position.y < (slingRigidbody.position.y - 0.5f)) 
            && (haitOld > transform.position.y))
        {           
            isShot = false;
            Reload();
            if (SayImDead != null)
            {
                SayImDead(this);
            }
            this.gameObject.SetActive(false);
        }
        haitOld = transform.position.y;
    }

   

    private void Reload()
    {
        Cathapult cathapult = slingRigidbody.gameObject.GetComponentInParent<Cathapult>();
        cathapult.LoadProjectile();        
    }   

    private void DragBall()
    {
        
        slingRigidbody = thisSpring.connectedBody;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePos, slingRigidbody.position);
        if (distance > maxDistance)
        {
            Vector2 direction = (mousePos - slingRigidbody.position).normalized;
            thisRigidbody.position = slingRigidbody.position + direction * maxDistance;
        }
        else
        {
            thisRigidbody.position = mousePos;
        }
        Vector2 position = thisRigidbody.position;
        if (position.y > slingRigidbody.position.y)
        {
            position.y = slingRigidbody.position.y;
        }
        if (position.x > slingRigidbody.position.x)
        {
            position.x = slingRigidbody.position.x;
        }
        thisRigidbody.position = position;
    }

    
    private IEnumerator ProjectileRelease()
    {
        yield return new WaitForSeconds(DelayToRelease);
        thisSpring.enabled = false;
        isShot = true;
    }
    #endregion
}
