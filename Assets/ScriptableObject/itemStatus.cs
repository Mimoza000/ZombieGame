using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New itemStatus", menuName ="itemStatus")]
public class itemStatus : ScriptableObject
{
    public new string name;
    public int id;
    public Sprite image;
    public int size;
    public int value;
}
