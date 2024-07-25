using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyAfterDelayy : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 2); // 2 Saniye sonra yok et.
    }


}

