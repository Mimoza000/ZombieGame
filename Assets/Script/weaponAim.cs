using UnityEngine;
using DG.Tweening;

public class weaponAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UI_Manager_Game crosshair;
    public bool isAiming;
    [Header("Value")]
    public Transform aimPoint;
    public float aimingTime = 0;

    void Update()
    {   
        if (GameManager.Instance.aim == 1)
        {
            transform.DOLocalMove(aimPoint.localPosition, aimingTime)/*.SetEase(Ease.InOutCirc)*/;
            crosshair.AimingCrosshair(true,aimingTime);
            isAiming = true;
        }
        else
        {
            transform.DOLocalMove(Vector3.zero, aimingTime)/*.SetEase(Ease.InOutCirc)*/;
            crosshair.AimingCrosshair(false,aimingTime);
            isAiming = false;
        }
    }
}
