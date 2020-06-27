using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float horizVel = 0;
    public int laneNum = 2;
    public string controlLocked = "n";

    public GameObject failScreen;
    public GameObject SuccesScreen;

    public GUIStyle style;
    Animator animator;
    Rigidbody rb;
    public AudioSource coinSound;


    // Karekterin zemine basıp basmadığını kontrol edecek olan boolean tipindeki değişken
    public bool isGrounded;

    // Oyun içi bazı durumlarda karakterimizin iç rengini değiştirecez, o yüzden karakterimizin iç modelini GameObject tipindeki değişkene atıyoruz.
    public GameObject CharacterInside;
    public GameObject finish;

    public bool canMove;
    public bool isFinish;
    public bool isFail;

    public Text skortxt;
    public int skor;

    [Header("Confeti Particle")]
    public GameObject confetiParticle;

    void Start()
    {
        // Karakterimizin Animator component-ine erişiyoruz
        animator = GetComponent<Animator>();

        // Karakterimizin Rigidbody component-ine erişiyoruz
        rb = GetComponent<Rigidbody>();
        style.fontSize = 50;
        canMove = false;
        confetiParticle.SetActive(false);
    }

    

    private void Update()
    {

        if (canMove)
        {
            // Karakterimizi z ekseni yönünde hareket ettirmemizi sağlayan kod yapısı. Verdiğimiz hız değerine göre yavaşlayıp hızlanabilir.
            //transform.position += Vector3.forward * Time.deltaTime * speed;

            rb.velocity = new Vector3(horizVel, 0, 20);
            animator.SetBool("Run", true);

            if (Input.GetKeyDown(KeyCode.A) && (laneNum > 1) && controlLocked == "n")
            {
                horizVel = -16;
                StartCoroutine(stopSlide());
                laneNum -= 1;
                controlLocked = "y";
            }

            if (Input.GetKeyDown(KeyCode.D) && (laneNum < 3) && controlLocked == "n")
            {
                horizVel = 16;
                StartCoroutine(stopSlide());
                laneNum += 1;
                controlLocked = "y";
            }
        }

    }

    // İki collider ilk çarpıştığı anda gerçekleşecek olaylar
    private void OnCollisionEnter(Collision collision)
    {
        // Karakterimizin collider-ı Finish tagli objeye çarparsa olacak işlemler (İşin özeti karakter finish noktasına gelince olacak işlemler)
        if (collision.gameObject.tag == "Finish")
        {
            // Hızımızı sıfırlıyoruz
            rb.velocity = new Vector3(0, 0, 0);

            // Dans etme animasyonunu oynatıyoruz
            animator.SetTrigger("Dance");
            
            // Karakterimizin iç rengini yeşil yapıyoruz 
            CharacterInside.GetComponent<Renderer>().material.color = Color.green;
            CharacterInside.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
           
            finish.SetActive(true);

            SuccesScreen.SetActive(true);

            confetiParticle.SetActive(true);

            isFinish = true;
        }

        // Karakterimizin collider-ı Barrier tagli objeye çarparsa olacak işlemler (İşin özeti karakter fail olursa olacak işlemler)
        if (collision.gameObject.tag == "Barrier")
        {
            // Hızımızı sıfırlıyoruz
            rb.velocity = new Vector3(0, 0, 0);

            // Fail olma animasyonunu oynatıyoruz
            animator.SetTrigger("Falling");
            
            // Karakterimizin iç rengini kırmızı yapıyoruz
            CharacterInside.GetComponent<Renderer>().material.color = Color.red;
            CharacterInside.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

            failScreen.SetActive(true);

            isFail = true;
        }

        // Karakterimizin collider-ı Ground tagli objeye çarparsa olacak işlemler (işin özeti karakter zemine değince isGrounded true yapıyoruz)
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Zemine değip değmediğimizi kontrol eden değişkeni true yapıyoruz
            isGrounded = true;
            
        }

    }

    // İki collider çarpıştıktan sonra birbirininde ayrılınca olacak işlemler
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGrounded false yağıyoruz. Yani işin mantığı şu karakterim zemine basmıyorsa isGrounded false olsun, basıyorsa true olsun.
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            coinSound.Play();
            skor += 10;

            skortxt.text = skor.ToString();
            Destroy(other.gameObject);
        }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controlLocked = "n";
    }

}
