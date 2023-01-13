using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Key : MonoBehaviour
{
    public int id;

    public void DestroyKey()
    {
        Destroy(gameObject);
    }
}