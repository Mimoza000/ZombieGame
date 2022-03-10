using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New itemStatus", menuName ="itemStatus")]
public class itemStatus : ScriptableObject
{
    public new string name;
    public Mesh mesh;
    public Sprite image;
    public int value;
}
