using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManger gameManger;
    public int difficulty; // oyun zorluk seviyesi
   
    void Start()
    {
        button = GetComponent<Button>();
        gameManger = GameObject.Find("Game Manager").GetComponent<GameManger>();
        button.onClick.AddListener(SetDifficulty); // butona t�kland���n da setdifficulty fonskiyonu �a��r.
    }
  public void SetDifficulty()
  {
        Debug.Log(button.gameObject.name + "t�kland�");
        gameManger.StartGame(difficulty); // se�ilen zorluk seviye butonuna bas�ld���nda oyunu ba�lat.
  }
}
