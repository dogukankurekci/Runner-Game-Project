using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    public AudioSource confeti;
    public AudioSource fail;

    public PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerController.isFinish = false;
    }


    void Update()
    {

        //Karakterimiz finishe gelince isFinish boolean değeri true oluyor ve confeti sesi oynatıyor
        if (playerController.isFinish)
            confeti.enabled = true;

        //Karakterimiz fail olunca isFail boolean değeri true oluyor ve fail olma sesi oynatılıyor
        if (playerController.isFail)
            fail.enabled = true;
    }
}
