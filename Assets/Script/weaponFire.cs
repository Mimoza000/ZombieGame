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
    float reloadingTime = 3;
    public float nowReloadTime;
    bool nowReloading;
    [SerializeField] Ray ray_0;
    [SerializeField] Ray ray_1;
    public RaycastHit hitInfo_0;
    public RaycastHit hitInfo_1;
    [Header("Value")]
    public int ammo = 0;
    public int maxAmmo = 30;
    [Tooltip("sec")]
    public float reloadTime = 3.3f;
    [Tooltip("100%")]
    float fireRate = 0.12f;
    [SerializeField] float fireRecoil = 0;
    bool enableSemiAuto = false;
    [SerializeField] int damage = 1;// crosshairTime = 0.1f;

    enemySystem enemy;
    /*LineRenderer line;*/

    void Start()
    {
        ammo = maxAmmo;
        // crosshair.Damaged// crosshair(true,0,true);
        /*line = GetComponent<LineRenderer>();*/
    }

    // Update is called once per frame
    public void Fire(bool trigger)
    {
        
        isShooting = trigger;
        if (enableSemiAuto && !trigger) return;
        if (enableSemiAuto) Shot();
        
        //if (GameManager.Instance.fire == 0) // crosshair.Damaged// crosshair(false, damaged// crosshairTime);
    }

    void Update()
    {
        Debug.Log(nowReloading);
        if (isShooting && !enableSemiAuto)
        {
            
            Shot();
        }
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

    async void Shot()
    {
        // Reload Check
        if (ammo <= 0 && !nowReloading) ReloadStart();
        if (nowReloading) return;

        ammo--;
        muzzleFlash.Play();
        await UniTask.Delay((int)fireRate * 1000);
        if (hitInfo_1.collider.CompareTag("Enemy")) enemy = hitInfo_1.collider.GetComponentInParent<enemySystem>();
        if (enemy != null && !enemy.animator.GetBool("dead"))
        {
            enemy.Damaged(damage);
            // crosshair.Damaged// crosshair(true, damaged// crosshairTime);
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
}
