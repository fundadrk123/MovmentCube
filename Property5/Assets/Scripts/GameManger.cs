using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManger :MonoBehaviour
{
    public List<GameObject> targets; // gameobjeleri listelemek için oluþturulan bir deðiþken.
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI TimerText;
    public bool isGameActive; // oyun aktifliði
    public int score;
    private float spawnrate = 1.0f; // yumurtlama oraný
    public float gameTime = 60f; // Oyun süresi
    //private Target Target;


    void Start()
    {
        UpdateTimerText(); // oyun süresini güncelleyen methot.
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

   public void UpdateTimerText() // oyun neselerimi her karde çaðrýp göstermesi için oluþan fonksiyon.
    {
        int minutes = Mathf.FloorToInt(gameTime / 60f); // saniye cinsinden kaç dakika olduðunu hesaplar (mathf.floortoInt) virgülden sonrasýný alýr ve en küçük tam sayýyý verir.
        int seconds = Mathf.FloorToInt(gameTime % 60f); // saniye cinsinden kalan kýsmý hesaplýyor.
        string timeString = string.Format("{0:00}", seconds); // iki basamaklý saat formatýna çevirir
        TimerText.text = "Time: " + timeString; // metin haline çeviriyor.
    }

    IEnumerator SpawnTarget() // hedeflerin rasgele oluþturulmasý için oluþan fonksiyon.
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnrate); // her saniyede bir  random olarak objelerimizin doðmasýný saðlýyor.
            int index = Random.Range(0, targets.Count); // listenin indexini alýp ýnstantiate ediyoruz.
            Instantiate(targets[index]); // yumurtla ve target prefablarýn uzunluðunu al.
        }
    }
   public void updateScore(int scoreToAdd) // eklenen score alýcak.
   {
        score += scoreToAdd; // parametre koyulan her score ekleyecek.
        scoreText.text = "Score" + score;
   }
    public void GameOver() // oyunu bitirme fonksiyonu
    {
        restartButton.gameObject.SetActive(true); // butonu aktifleþtirmek için set activliði true.
        gameoverText.gameObject.SetActive(true); // gameover textini aktifleþtirmek için.
        isGameActive = false; // oyun aktifliðini false olarak ayarla ve bitir demek.
        Debug.Log("game over");
    }
    public void RestartGame() // oyunu yeniden baþlatmak için oluþturulan bir fonksiyon.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // sahne içindeki oyun sahnesini al ve yeniden yükle demek.
    }
    public void StartGame(int difficulty) // oyunun baþlangýç ayarlarýný yapýp yeniden baþlatmak için.
    {
        isGameActive = true; // oyunun ne zaman baþladýðýný öðrenmek için.
        score = 0;
        spawnrate /= difficulty;// seviye zorluklarý için easy(kolay) seviyeyi difficultyye bölücek.ve her zorluðu kendi deðerinde alýp bölüp baþlatýcak.
        StartCoroutine(SpawnTarget());
        
        updateScore(0);
        titleScreen.gameObject.SetActive(false); // seviye panellerini false olarak aldý.
    }
}

// (sceneManger) = sahne yükleme kitaplýðý.
//(loadScene) = bulunduðumuz sahneyi yükler.
//(name) = sahnenin ismini alýr.