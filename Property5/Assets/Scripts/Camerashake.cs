using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerashake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;



    public Transform camTransform;

    
    public float shakeDuration = 0f;

   
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
    //void Start()
    //{
    //  CinemachineBrain cinemachineBrain = GetComponent<CinemachineBrain>();
    //if (cinemachineBrain)
    //{
    //  virtualCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    //}
    //}
    //public IEnumerator Shake(float duration,float magnitude)
    //{
    //  Vector3 orginalPos = transform.localPosition;
    // float elapsed = 0.0f;
    // while (elapsed < duration)
    //{
    //  float x = Random.Range(-1f, 1f) * magnitude;
    //float y= Random.Range(-1f, 1f) * magnitude;
    //float z = Random.Range(-1f, 1f) * magnitude;

    //transform.localPosition = new Vector3(x, y,orginalPos.z);
    //elapsed += Time.deltaTime;
    // yield return null;
    //}
    //transform.localPosition = orginalPos;
    //}
}
