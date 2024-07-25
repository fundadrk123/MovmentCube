using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using Cinemachine;
using static Cinemachine.CinemachineTargetGroup;


public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManger gameManger;
    public ParticleSystem explosionParticle; //public ParticleSystem explosion; // oyun patlatma efekti.
    
    private float minSpeed = 25; 
    private float maxSpeed = 50;
    private float maxTorque = 10;
    private float xRange =10; // objelerin arasýndaki mesafeler.
    private float yspawnPos=-4;
    public int pointValue; // obejelere atanan puanlarý almasý.
    public bool FinishGame=false; // oyunu bitir.
    public float duration =3f; // yavaþlýk süresi.
    public bool TimeSlow = false; // süre azaltma.
    public bool timeÝncrease = false; // süre artýrma.
    public float increaseFactor = 5.0f; // artacak süre.
   
    public bool slow = false; // oyun yavaþlatma.1
    public float slowFactor =-5f; // Zamanýn yavaþlatýlma oraný.2
    private float originalTimeScale; //.3

    public Camerashake camerashake;
    public bool explosion=false; // patlama. 
  



    void Start()
    {
       camerashake = FindObjectOfType<Camerashake>();
       //.camerascripts.instance.StartCoroutine(camerashake.Shake( 0.5f,0.2f));
        targetRb = GetComponent<Rigidbody>();
        gameManger = GameObject.Find("Game Manager").GetComponent<GameManger>(); // gamemanager scriptimizi almak için.
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // x,y,z deðerleri içinde her biri -10 ile 10 deðeri arasýnda dönmesini saðlayacak.
        // AddTorque nesnelerin dönmesi için kuvvet uygular.
        transform.position = RandomSpawnPos();
        originalTimeScale = Time.timeScale;
    }
    public void Slow()
    {
        Debug.Log(slow);
        if (slow)
        {
            Time.timeScale = slowFactor;
            Debug.Log("Time.timeScale: " + Time.timeScale);
            tEMP.instance.StartCoroutine(ResetTime(0.5f));
        }
    }
    public IEnumerator ResetTime(float reset)
    {
        Debug.Log("RESET TIME "+reset);
        yield return new WaitForSeconds(reset); // Zaman ölçeðini dikkate alarak bekle ve zamaný resetle.
        Debug.Log("bekledi");
        Time.timeScale = originalTimeScale; // Orijinal zaman ölçeðine geri dön
        Debug.Log("hýzlandý");
        slow = false;
    }
    public void Explosion()
    {
        Debug.Log("patlama çalýþtý");
        //.camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f));
        explosion = false;
        Debug.Log("patlama gerçekleþti");
    }
  
    public void OnMouseDown() // objelerimize maous ile týklaya bilme fonksiyonu
    {
        if (gameManger.isGameActive)
        {
           // Explosion();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // patlama efektinin pozisyonunu ve rotasyonunu ayarlýyoruz.
           //.camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f));
            gameManger.updateScore(pointValue);
            Destroy(gameObject);
        }
        finish();
        StartCoroutine(ResetTime(0.3f));
        SlowT();
        TimeÝncrease();
        Slow();
    }
 

    private void OnTriggerEnter(Collider other)
    {
         Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
          gameManger.GameOver();
        }
    }
    void finish() // oyun bitirme.sorunsuz çalýþýyor.
    {
        if (FinishGame == true)
        {
            gameManger.gameTime = -Time.deltaTime;
            gameManger.gameoverText.gameObject.SetActive(false);
            Debug.Log("oyun bitti");
        }
    }
   
    void SlowT() // süre azaltma.!! düzeltidi.
    {
        if (TimeSlow == true)
        {
             gameManger.gameTime /= duration; // oyun süresi
            gameManger.gameTime -= duration;
            TimeSlow = false;
            Debug.Log("süre azaldý");
        }
    }
    void TimeÝncrease()  // süre artýrma.!! düzeltildi.
    {
        if (timeÝncrease == true)
        {
            gameManger.gameTime /= increaseFactor;
            gameManger.gameTime += increaseFactor;
            Debug.Log("Süre arttý");
        }
    }
  
    Vector3 RandomForce()
    {
        return Vector3.up * UnityEngine.Random.Range(minSpeed, maxSpeed); // vector3 deðerini yeni bir deðer ile çarpýyoruz.
    }
    float RandomTorque()
    {
        return UnityEngine.Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
            float adjustedYSpawnPos = yspawnPos * transform.localScale.y; // Y eksenindeki boyut deðiþikliðini hesaplamak için objenin ölçeklerini kullanýn
            return new Vector3(UnityEngine.Random.Range(-xRange, xRange), adjustedYSpawnPos);
        // return new Vector3(UnityEngine.Random.Range(-xRange, xRange), -yspawnPos); // yeni vector3 deðerlerine sahip olan yeni random pozisyonlarý alýyoruz.
    }
}




// private IEnumerator HandleExplosion(float explosionDuration, float intenstiy)
//{
//yield return new WaitForSeconds(0.1f); // Patlama efektinin bitmesini bekle.
//  camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f)); // Patlama olduðunda kamera sallanmasý
// yield return new WaitForSeconds(explosionDuration);
// explosion = true;
// Debug.Log("fldmsldmsl");
//}









