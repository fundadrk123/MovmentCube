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
        button.onClick.AddListener(SetDifficulty); // butona týklandýðýn da setdifficulty fonskiyonu çaðýr.
    }
  public void SetDifficulty()
  {
        Debug.Log(button.gameObject.name + "týklandý");
        gameManger.StartGame(difficulty); // seçilen zorluk seviye butonuna basýldýðýnda oyunu baþlat.
  }
}
