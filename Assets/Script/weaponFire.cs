using UnityEngine;
using TMPro;

public class weaponFire : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform muzzle;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] TrailRenderer tracerEffect;
    [SerializeField] UI_Manager_Game crosshair;
    [Header("Debug")]
    public float reloadingTime = 0;
    public bool isShooting = false;
    public bool nowReloading = false;
    [SerializeField] Ray ray_0;
    [SerializeField] Ray ray_1;
    public RaycastHit hitInfo_0;
    public RaycastHit hitInfo_1;
    [Header("Value")]
    public int ammo = 0;
    public int maxAmmo = 30;
    [Tooltip("sec")]
    [SerializeField] float reloadTime = 1;
    [Tooltip("100%")]
    [SerializeField] float fireRate = 12;
    [SerializeField] float fireRecoil = 0;
    [SerializeField] bool enableSemiAuto = false;
    [SerializeField] int damage = 1;
    [SerializeField] float damagedCrosshairTime = 0.1f;

    enemySystem enemy;
    /*LineRenderer line;*/

    void Start()
    {
        ammo = maxAmmo;
        isShooting = true;
        crosshair.DamagedCrosshair(true,0,true);
        /*line = GetComponent<LineRenderer>();*/
    }

    // Update is called once per frame
    void Update()
    {    
       // Shooting System
        if (GameManager.Instance.fire == 1 && isShooting)
        {
            isShooting = false;
            if (ammo <= 0 && !nowReloading) ReloadStart();
            if (nowReloading)
            {
                GameManager.Instance.fire = 0;
                
                return;
            }
            if (enableSemiAuto) GameManager.Instance.fire = 0;
            Invoke("Shoot", fireRate / 100);
            return;
        }
        if (GameManager.Instance.fire == 0) crosshair.DamagedCrosshair(false, damagedCrosshairTime);
        if (nowReloading) reloadingTime += Time.deltaTime;
        else reloadingTime = 0;

        if (GameManager.Instance.reload == 1 && !nowReloading && ammo < 30) ReloadStart(); 
    }

    private void FixedUpdate()
    {
        ray_0.origin = Camera.main.transform.position;
        ray_0.direction = Camera.main.transform.forward;
        if (Physics.Raycast(ray_0, out hitInfo_0, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(ray_0.origin, ray_0.direction * 100, Color.red, 1);
            Debug.DrawLine(ray_0.origin, hitInfo_0.collider.transform.position, Color.green, 1);

            ray_1.origin = muzzle.transform.position;
            ray_1.direction = hitInfo_0.point - ray_1.origin;

            Physics.Raycast(ray_1, out hitInfo_1, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
            // Debug.Log(hitInfo_1.collider.name);
            Debug.DrawRay(ray_1.origin, ray_1.direction * 100, Color.blue, 1);
        }
    }

    void Shoot()
    {
        ammo--;
        muzzleFlash.Emit(1);
        isShooting = true;

        // TrailRenderer
        // var tracer = Instantiate(tracerEffect, muzzle.transform.position, Quaternion.identity);
        // tracer.AddPosition(ray.origin);
        // tracer.transform.position = hit.point;

        if (hitInfo_1.collider.CompareTag("Enemy")) enemy = hitInfo_1.collider.GetComponentInParent<enemySystem>();
        if (enemy != null && !enemy.animator.GetBool("dead"))
        {
            enemy.Damaged(damage);
            crosshair.DamagedCrosshair(true, damagedCrosshairTime);
        }

    }

    void ReloadStart()
    {
        isShooting = false;
        nowReloading = true;
        Invoke("ReloadFinish", reloadTime);
    }

    void ReloadFinish()
    {
        ammo = maxAmmo;
        isShooting = true;
        nowReloading = false;
    }
}
