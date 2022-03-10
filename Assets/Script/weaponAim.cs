using UnityEngine;
using DG.Tweening;

public class weaponAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIManager_Game crosshair;
    [Header("Value")]
    public Transform aimPoint;
    float aimingTime = 1;

    public void Aim(bool isAiming)
    {
        if (isAiming)
        {
            transform.DOLocalMove(aimPoint.localPosition, aimingTime)/*.SetEase(Ease.InOutCirc)*/;
            crosshair.CrossHairFade(false,aimingTime);
        }
        else
        {
            transform.DOLocalMove(Vector3.zero, aimingTime)/*.SetEase(Ease.InOutCirc)*/;
            crosshair.CrossHairFade(true,aimingTime);
        }
    }
}
