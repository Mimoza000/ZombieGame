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
    bool isShooting;
    float reloadingTime = 3.3f;
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
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource fire;
    enemySystem enemy;
    float duration = 0.5f;
    /*LineRenderer line;*/

    void Start()
    {
        ammo = maxAmmo;
        nowReloadTime = 0;
        
    }

    public async void Fire(bool trigger)
    {
        
        isShooting = trigger;
        if (enableSemiAuto && !trigger) return;
        if (enableSemiAuto) Shot();
        
        //if (GameManager.Instance.fire == 0) // crosshair.Damaged// crosshair(false, damaged// crosshairTime);
    }

    void Update()
    {
        

        if (nowReloading) 
        {
            nowReloadTime += Time.deltaTime;
        }
        else nowReloadTime = 0;

        if (enemy != null && !enableSemiAuto)
        {   
            crosshair.HitCrossHairFade(false,false,duration,hitInfo_1);
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

        ammo--;
        muzzleFlash.Play();
        fire.PlayOneShot(clip);
        
        if (hitInfo_1.collider.CompareTag("Enemy")) enemy = hitInfo_1.collider.GetComponentInParent<enemySystem>();
        if (enemy != null && !enemy.animator.GetBool("dead"))
        {
            enemy.Damaged(damage);
            crosshair.HitCrossHairFade(true,enableSemiAuto,duration,hitInfo_1);
        }
    }

    public async void ReloadStart()
    {
        nowReloading = true;
        await UniTask.Delay((int)reloadingTime * 1000);
        ReloadFinish();
    }

    void ReloadFinish()
    {
        ammo = maxAmmo;
        nowReloading = false;
    }

    void Timer()
    {
        reloadingTime += Time.deltaTime;
    }
}
