using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject PauseMenu;
    public GameObject skor;

    public GameObject PauseButton;

    PlayerController playerController;

    void Start()
    {
        MainMenu.SetActive(true);

        Screen.SetResolution(720, 1920, true);

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Ses ayarı ile oynamak için oluşturduğumuz fonksiyon
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    //Start butonuna tıkladığımızda olacak işlemleri tutan fonksiyon
    public void gameStart()
    {
        //Main menümüzü kapatıp ana ekrandaki butonu aktif hale getirdik
        PauseButton.SetActive(true);
        MainMenu.SetActive(false);
        skor.SetActive(true);

        //Main menude hareket tuşları çalışmıcak şekilde ayarlamıştık burada start tuşuna bastığımızda hareket tuşları tekrardan aktif oluyor
        playerController.canMove = true;
    }

    //Pause menusunu açmaya yarayan fonksiyon
    public void gamePause()
    {
        //menu açma kapama işlemleri
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);

        //Pause ekranında zamanı durduruyoruz
        Time.timeScale = 0f;
        //Hareket tuşlarını kitliyoruz
        playerController.canMove = false;
    }

    //Continue butonuna basınca olacak işlemleri tutan fonksiyon
    public void gameResume()
    {
        //menu açma kapama işlemleri
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);

        //Zamanı normale çeviriyoruz
        Time.timeScale = 1f;
        //Hareket tuşlarını açıyoruz
        playerController.canMove = true;
    }

    //Ana menüye dönmemizi sağlayan (Home butonu) fonksiyon
    public void Home()
    {
        //menu açma kapama işlemleri
        MainMenu.SetActive(true);

        skor.SetActive(false);
        SettingsMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseButton.SetActive(false);

        Time.timeScale = 1f;
    }

    //Ayarlar menüsünü açan fonksiyon
    public void settings()
    {
        //menu açma kapama işlemleri
        SettingsMenu.SetActive(true);

        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseButton.SetActive(false);
    }

    //Credits bölümünün açılmasını sağlayan fonksiyon
    public void credits()
    {
        //menu açma kapama işlemleri

        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseButton.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    //Sahnemizi restartlamamıza yarayan forksiyon
    public void restartGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

}
