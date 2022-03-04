using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] float multiplier = 1;
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0,1 * multiplier,0);
    }
}
