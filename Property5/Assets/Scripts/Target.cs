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
    private float xRange =10; // objelerin aras�ndaki mesafeler.
    private float yspawnPos=-4;
    public int pointValue; // obejelere atanan puanlar� almas�.
    public bool FinishGame=false; // oyunu bitir.
    public float duration =3f; // yava�l�k s�resi.
    public bool TimeSlow = false; // s�re azaltma.
    public bool time�ncrease = false; // s�re art�rma.
    public float increaseFactor = 5.0f; // artacak s�re.
   
    public bool slow = false; // oyun yava�latma.1
    public float slowFactor =-5f; // Zaman�n yava�lat�lma oran�.2
    private float originalTimeScale; //.3

    public Camerashake camerashake;
    public bool explosion=false; // patlama. 
  



    void Start()
    {
       camerashake = FindObjectOfType<Camerashake>();
       //.camerascripts.instance.StartCoroutine(camerashake.Shake( 0.5f,0.2f));
        targetRb = GetComponent<Rigidbody>();
        gameManger = GameObject.Find("Game Manager").GetComponent<GameManger>(); // gamemanager scriptimizi almak i�in.
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse); // x,y,z de�erleri i�inde her biri -10 ile 10 de�eri aras�nda d�nmesini sa�layacak.
        // AddTorque nesnelerin d�nmesi i�in kuvvet uygular.
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
        yield return new WaitForSeconds(reset); // Zaman �l�e�ini dikkate alarak bekle ve zaman� resetle.
        Debug.Log("bekledi");
        Time.timeScale = originalTimeScale; // Orijinal zaman �l�e�ine geri d�n
        Debug.Log("h�zland�");
        slow = false;
    }
    public void Explosion()
    {
        Debug.Log("patlama �al��t�");
        //.camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f));
        explosion = false;
        Debug.Log("patlama ger�ekle�ti");
    }
  
    public void OnMouseDown() // objelerimize maous ile t�klaya bilme fonksiyonu
    {
        if (gameManger.isGameActive)
        {
           // Explosion();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // patlama efektinin pozisyonunu ve rotasyonunu ayarl�yoruz.
           //.camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f));
            gameManger.updateScore(pointValue);
            Destroy(gameObject);
        }
        finish();
        StartCoroutine(ResetTime(0.3f));
        SlowT();
        Time�ncrease();
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
    void finish() // oyun bitirme.sorunsuz �al���yor.
    {
        if (FinishGame == true)
        {
            gameManger.gameTime = -Time.deltaTime;
            gameManger.gameoverText.gameObject.SetActive(false);
            Debug.Log("oyun bitti");
        }
    }
   
    void SlowT() // s�re azaltma.!! d�zeltidi.
    {
        if (TimeSlow == true)
        {
             gameManger.gameTime /= duration; // oyun s�resi
            gameManger.gameTime -= duration;
            TimeSlow = false;
            Debug.Log("s�re azald�");
        }
    }
    void Time�ncrease()  // s�re art�rma.!! d�zeltildi.
    {
        if (time�ncrease == true)
        {
            gameManger.gameTime /= increaseFactor;
            gameManger.gameTime += increaseFactor;
            Debug.Log("S�re artt�");
        }
    }
  
    Vector3 RandomForce()
    {
        return Vector3.up * UnityEngine.Random.Range(minSpeed, maxSpeed); // vector3 de�erini yeni bir de�er ile �arp�yoruz.
    }
    float RandomTorque()
    {
        return UnityEngine.Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
            float adjustedYSpawnPos = yspawnPos * transform.localScale.y; // Y eksenindeki boyut de�i�ikli�ini hesaplamak i�in objenin �l�eklerini kullan�n
            return new Vector3(UnityEngine.Random.Range(-xRange, xRange), adjustedYSpawnPos);
        // return new Vector3(UnityEngine.Random.Range(-xRange, xRange), -yspawnPos); // yeni vector3 de�erlerine sahip olan yeni random pozisyonlar� al�yoruz.
    }
}




// private IEnumerator HandleExplosion(float explosionDuration, float intenstiy)
//{
//yield return new WaitForSeconds(0.1f); // Patlama efektinin bitmesini bekle.
//  camerascripts.instance.StartCoroutine(camerashake.Shake(0.5f, 0.2f)); // Patlama oldu�unda kamera sallanmas�
// yield return new WaitForSeconds(explosionDuration);
// explosion = true;
// Debug.Log("fldmsldmsl");
//}









