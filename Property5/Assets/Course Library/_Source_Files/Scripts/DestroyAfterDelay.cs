using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyAfterDelay : MonoBehaviour
{

    public bool slow = false; // oyun yavaşlatma.1
    public float slowFactor = 5f; // Zamanın yavaşlatılma oranı.2
    private float originalTimeScale; //.3
    void Start()
    {
        originalTimeScale = Time.timeScale;
        Destroy(gameObject, 2);
    }

   
   public void Slow()
    {
        if (!slow)
        {
            slow = true;
            Time.timeScale = slowFactor;
            Debug.Log("Time.timeScale: " + Time.timeScale);
            StartCoroutine(DelayAndResetTime(0.3f));
        }
    }
  public  IEnumerator DelayAndResetTime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = originalTimeScale; // Zaman ölçeğini orijinal değere geri döndür
        slow = false;
        Debug.Log("hızlandı");
    }
}
