using UnityEngine;
using DG.Tweening;

public class ObjectRotation : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(Vector3.up,1)
        .SetLoops(-1,LoopType.Restart)
        .SetLink(gameObject);
    }
}
