using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascripts : MonoBehaviour
{
    public static camerascripts instance;
    //public CinemachineVirtualCamera virtualCamera;


    void Start()
    {
        instance = this;
        // CinemachineBrain cinemachineBrain = GetComponent<CinemachineBrain>();
        
    }
}
//if (cinemachineBrain)
// virtualCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
//}
//}
//public IEnumerator Shake(float duration, float magnitude)
//{
//Vector3 orginalPos = transform.localPosition;
//float elapsed = 0.0f;
//while (elapsed < duration)
//{
//float x = Random.Range(-1f, 1f) * magnitude;
//float y = Random.Range(-1f, 1f) * magnitude;
//float z = Random.Range(-1f, 1f) * magnitude;

//  transform.localPosition = new Vector3(x, y, z);
//elapsed += Time.deltaTime;
//yield return null;
//}
//transform.localPosition = orginalPos;
// }








//void Start()
//{
//  instance = this;
// CinemachineBrain cinemachineBrain = GetComponent<CinemachineBrain>();
// if (cinemachineBrain)
//{
//  virtualCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
//}
//}

//public void Explosion()
//{
//  Debug.Log("patlama çalýþtý");
// if (!explosion)
//{
//  StartCoroutine(HandleExplosion(explosionDuration, 0.2f));
// explosion = true;
//}
//}

//private IEnumerator HandleExplosion(float duration, float magnitude)
//{
//  StartCoroutine(Shake(duration, magnitude)); // Call the Shake method directly
// yield return new WaitForSeconds(duration);
// explosion = false; // Reset explosion flag
//}

//private IEnumerator Shake(float duration, float magnitude)
//{
//  Vector3 orginalPos = transform.localPosition;
// float elapsed = 0.0f;
//while (elapsed < duration)
//{
//  float x = Random.Range(-1f, 1f) * magnitude;
//float y = Random.Range(-1f, 1f) * magnitude;
// float z = Random.Range(-1f, 1f) * magnitude;

// transform.localPosition = new Vector3(x, y, z);
//elapsed += Time.deltaTime;
// yield return null;
//}
// transform.localPosition = orginalPos;
// }
