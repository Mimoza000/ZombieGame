using UnityEngine;
using Cysharp.Threading.Tasks;
public class weaponFire : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform muzzle;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] UIManager_Game crosshair;
    public float nowReloadTime;
    bool nowReloading;
    [SerializeField] Ray ray_0;
    [SerializeField] Ray ray_1;
    public RaycastHit hitInfo_0;
    public RaycastHit hitInfo_1;
    [Header("Value")]
    [HideInInspector] public int ammo = 0;
    public int maxAmmo = 20;
    
    [Tooltip("100%")]
    [SerializeField] float fireRate;
    [SerializeField] float fireRecoil = 0;
    public bool enableSemiAuto = true;
    [SerializeField] int damage = 1;
    [SerializeField] AudioClip fireSE;
    [SerializeField] AudioClip reloadSE;
    [SerializeField] AudioSource muzzleAudio;
    [SerializeField] AudioSource magazineAudio;
    enemySystem enemy;
    float duration = 0.5f;
    bool canShoot = false;
    float nowShootTime = 0;
    void Start()
    {
        ammo = maxAmmo;
        nowReloadTime = 0;
    }

    public void Fire(bool trigger)
    {    
        if (enableSemiAuto)    
        {
            if (!trigger) 
            {
                canShoot = false;
                return;
            }
            Shot();
        }
        else
        {
            if (!trigger) 
            {
                canShoot = false;
                if (crosshair.hitCrosshair.color.a > 0) crosshair.HitCrossHairFade(false,false,duration);
                nowShootTime = 0;
                return;
            }
            Shot();
        }
    }

    void Update()
    {
        if (nowReloading) 
        {
            nowReloadTime += Time.deltaTime;
        }
        else nowReloadTime = 0;

        if (!enableSemiAuto && canShoot) 
        {
            nowShootTime += Time.deltaTime;
            if (nowShootTime >= fireRate) 
            {
                Shot();
                nowShootTime = 0;
            }
        }
        if (hitInfo_1.collider != null && canShoot)
        {
            if (!hitInfo_1.collider.CompareTag("Enemy"))
            {
                crosshair.HitCrossHairFade(false,false,duration);
                enemy = null;
            }
        }
    }

    private void FixedUpdate()
    {
        // For Debug
        ray_0.origin = Camera.main.transform.position;
        ray_0.direction = Camera.main.transform.forward;
        if (Physics.Raycast(ray_0, out hitInfo_0, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {
            // Camera
            Debug.DrawRay(ray_0.origin, ray_0.direction * 100, Color.red, 1);
            Debug.DrawLine(ray_0.origin, hitInfo_0.collider.transform.position, Color.green, 1);

            ray_1.origin = muzzle.transform.position;
            ray_1.direction = hitInfo_0.point - ray_1.origin;

            // Camera + Muzzle
            Physics.Raycast(ray_1, out hitInfo_1, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
            Debug.DrawRay(ray_1.origin, ray_1.direction * 100, Color.blue, 1);
        }
    }

    void Shot()
    {
        // Reload Check
        if (ammo <= 0 && !nowReloading) ReloadStart();
        if (nowReloading) return;

        // Shoot
        canShoot = true;
        ammo--;
        muzzleFlash.Play();
        muzzleAudio.PlayOneShot(fireSE);
        
        // Check ray and get enemySystem
        if (hitInfo_1.collider.CompareTag("Enemy")) enemy = hitInfo_1.collider.GetComponentInParent<enemySystem>();
        if (enemy != null && !enemy.animator.GetBool("dead"))
        {
            enemy.Damaged(damage);
            crosshair.HitCrossHairFade(true,enableSemiAuto,duration);
        }
    }

    public async void ReloadStart()
    {
        if (ammo >= maxAmmo) return;
        nowReloading = true;
        canShoot = false;
        magazineAudio.PlayOneShot(reloadSE);
        await UniTask.Delay((int)(reloadSE.length * 1000));
        ReloadFinish();
    }

    void ReloadFinish()
    {
        ammo = maxAmmo;
        nowReloading = false;
    }
}
