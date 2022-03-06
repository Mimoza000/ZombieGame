using UnityEngine;
using Cysharp.Threading.Tasks;

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
    [SerializeField] int fireRate = 120;
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
    public async void Fire()
    {    
       // Shooting System
        if (isShooting)
        {
            isShooting = false;
            if (ammo <= 0 && !nowReloading) ReloadStart();
            if (nowReloading) return;
            if (enableSemiAuto) 
            {
                await UniTask.Delay(fireRate);
            }
            return;
        }
        if (GameManager.Instance.fire == 0) crosshair.DamagedCrosshair(false, damagedCrosshairTime);
        if (nowReloading) reloadingTime += Time.deltaTime;
        else reloadingTime = 0;

        if (GameManager.Instance.reload == 1 && !nowReloading && ammo < 30) ReloadStart(); 
    }

    private void FixedUpdate()
    {
        // Debug用目線表示
        ray_0.origin = Camera.main.transform.position;
        ray_0.direction = Camera.main.transform.forward;
        if (Physics.Raycast(ray_0, out hitInfo_0, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(ray_0.origin, ray_0.direction * 100, Color.red, 1);
            Debug.DrawLine(ray_0.origin, hitInfo_0.collider.transform.position, Color.green, 1);

            ray_1.origin = muzzle.transform.position;
            ray_1.direction = hitInfo_0.point - ray_1.origin;

            Physics.Raycast(ray_1, out hitInfo_1, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
            Debug.DrawRay(ray_1.origin, ray_1.direction * 100, Color.blue, 1);
        }
    }

    void Shooting()
    {
        ammo--;
        muzzleFlash.Play();
        isShooting = true;

        if (hitInfo_1.collider.CompareTag("Enemy")) enemy = hitInfo_1.collider.GetComponentInParent<enemySystem>();
        if (enemy != null && !enemy.animator.GetBool("dead"))
        {
            enemy.Damaged(damage);
            crosshair.DamagedCrosshair(true, damagedCrosshairTime);
        }

    }

    public void ReloadStart()
    {
        isShooting = false;
        nowReloading = true;
        // await UniTask.Delay(reloadingTime);
    }

    void ReloadFinish()
    {
        ammo = maxAmmo;
        isShooting = true;
        nowReloading = false;
    }
}
