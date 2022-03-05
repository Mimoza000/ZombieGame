
using System.Collections;
using UnityEngine;

public class materialFade : MonoBehaviour
{
    [SerializeField] float duration = 0.001f;
    MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = mesh.material.color - new Color32(0,0,0,0);
    }
     void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine("Transparent");
        }
    }

    private IEnumerator Transparent()
    {
        if (mesh.material.color.a < 0) yield break;
        for ( int i = 0 ; i < 255 ; i++ )
        {
            mesh.material.color -= new Color32(0,0,0,1);
            yield return new WaitForSeconds(duration);
        }
    }
}
