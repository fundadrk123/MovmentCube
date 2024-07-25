using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManger :MonoBehaviour
{
    public List<GameObject> targets; // gameobjeleri listelemek i�in olu�turulan bir de�i�ken.
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI TimerText;
    public bool isGameActive; // oyun aktifli�i
    public int score;
    private float spawnrate = 1.0f; // yumurtlama oran�
    public float gameTime = 60f; // Oyun s�resi
    //private Target Target;


    void Start()
    {
        UpdateTimerText(); // oyun s�resini g�ncelleyen methot.
    }
    private void Update()
    {
        if (isGameActive)
        {
            gameTime -= Time.deltaTime;
            UpdateTimerText();
           
            if (gameTime <= 0f)
            {
                GameOver();
                Debug.Log("durdu");
            }
        }
    }

   public void UpdateTimerText() // oyun neselerimi her karde �a�r�p g�stermesi i�in olu�an fonksiyon.
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f); // saniye cinsinden ka� dakika oldu�unu hesaplar (mathf.floortoInt) virg�lden sonras�n� al�r ve en k���k tam say�y� verir.
        int seconds = Mathf.FloorToInt(gameTime % 60f); // saniye cinsinden kalan k�sm� hesapl�yor.
        string timeString = string.Format("{0:00}", seconds); // iki basamakl� saat format�na �evirir
        TimerText.text = "Time: " + timeString; // metin haline �eviriyor.
    }

    IEnumerator SpawnTarget() // hedeflerin rasgele olu�turulmas� i�in olu�an fonksiyon.
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnrate); // her saniyede bir  random olarak objelerimizin do�mas�n� sa�l�yor.
            int index = Random.Range(0, targets.Count); // listenin indexini al�p �nstantiate ediyoruz.
            Instantiate(targets[index]); // yumurtla ve target prefablar�n uzunlu�unu al.
        }
    }
   public void updateScore(int scoreToAdd) // eklenen score al�cak.
   {
        score += scoreToAdd; // parametre koyulan her score ekleyecek.
        scoreText.text = "Score" + score;
   }
    public void GameOver() // oyunu bitirme fonksiyonu
    {
        restartButton.gameObject.SetActive(true); // butonu aktifle�tirmek i�in set activli�i true.
        gameoverText.gameObject.SetActive(true); // gameover textini aktifle�tirmek i�in.
        isGameActive = false; // oyun aktifli�ini false olarak ayarla ve bitir demek.
        Debug.Log("game over");
    }
    public void RestartGame() // oyunu yeniden ba�latmak i�in olu�turulan bir fonksiyon.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // sahne i�indeki oyun sahnesini al ve yeniden y�kle demek.
    }
    public void StartGame(int difficulty) // oyunun ba�lang�� ayarlar�n� yap�p yeniden ba�latmak i�in.
    {
        isGameActive = true; // oyunun ne zaman ba�lad���n� ��renmek i�in.
        score = 0;
        spawnrate /= difficulty;// seviye zorluklar� i�in easy(kolay) seviyeyi difficultyye b�l�cek.ve her zorlu�u kendi de�erinde al�p b�l�p ba�lat�cak.
        StartCoroutine(SpawnTarget());
        
        updateScore(0);
        titleScreen.gameObject.SetActive(false); // seviye panellerini false olarak ald�.
    }
}

// (sceneManger) = sahne y�kleme kitapl���.
//(loadScene) = bulundu�umuz sahneyi y�kler.
//(name) = sahnenin ismini al�r.