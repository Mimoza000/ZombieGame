using UnityEngine;
using DG.Tweening;

public class itemDrop : MonoBehaviour
{
    [SerializeField] float duration = 1;
    Vector3 velocity;
    Transform player;
    Vector3 position;

    void Update()
    {
        if (player != null)
        {
            var acceleration = Vector3.zero;

            var diff = player.position - position;
            acceleration += (diff - velocity * duration) * 2 / (duration * duration);

            duration -= Time.deltaTime;
            if (duration < 0) return;

            velocity += acceleration * Time.deltaTime;
            position += velocity * Time.deltaTime;
            transform.position = position;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) player = collider.GetComponent<Transform>();
    }
}
