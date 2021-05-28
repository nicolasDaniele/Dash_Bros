using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItem : MonoBehaviour
{
    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
