using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Important Assignment")]
    [SerializeField] Rigidbody rb;
    [SerializeField] List<AudioClip> soundToPlayAfterMinigame;
    [SerializeField] Light Flashlight;

    [Header("Movement")]
    public int speed;
    [SerializeField] private int maxSpeed;

    [Header("Important Variables")]
    public int alzheimerStage = 0;

    [Header("Important Bools")]
    public bool playingMinigame;
    public bool isAlreadyTalking = false;
    public bool stopMoving = false;

    [Header("Camera")]
    [SerializeField] float viewHeightMax;
    [SerializeField] float viewHeightMin;
    [SerializeField] GameObject cameraObj;

    [Header("Pause Screen")]
    [SerializeField] GameObject pauseScreen;

    [Header("Extra")]
    public AudioSource footSteps;
    public MinigameManager minigameManager;
    public MusicHandler musicHandler;
    public GameObject bookToRead;
    public Image Fader;
    public AudioClip AfterFaderClip;
    public AmbientSoundHandler phoneAmbient;
    [SerializeField] private GameObject walls;

    [Header("Public for other scripts")]
    public AudioSource dialoguePlayer;

    public IEnumerator Fade(GameObject thisObject = null, bool removeObject = false)
    {
        stopMoving = true;
        isAlreadyTalking = true;
        print("Fade In");
        yield return Fader.DOFade(255, 1f);
        yield return new WaitForSeconds(1f);
        yield return Fader.DOFade(0, 3f);
        yield return new WaitForSeconds(1f);
        print("Fade Out");
        stopMoving = false;
        isAlreadyTalking = false;
        if (removeObject)
        {
            dialoguePlayer.clip = AfterFaderClip;
            dialoguePlayer.Play();
            thisObject.SetActive(false);
        }
    }

    public void BANANADONE(GameObject thisObject, bool removeObject)
    {
        StartCoroutine(Fade(thisObject, removeObject));
    }

    // Update is called once per frame
    void Update()
    {
        //Player Controls
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Flashlight.enabled = !Flashlight.enabled;
            Flashlight.gameObject.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //Pause and Unpause
        {
            PauseScreen();
        }

        if (!playingMinigame && !stopMoving)
        {
            Move();
            Rotation();
        }
    }

    public void Move()
    {
        print(Mathf.Approximately(Input.GetAxis("Horizontal"), 0));
        rb.AddRelativeForce(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        rb.maxLinearVelocity = maxSpeed;

        if ((!Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f)) && !footSteps.isPlaying)
        {
            footSteps.Play();
        }
        else if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) && Mathf.Approximately(Input.GetAxis("Vertical"), 0f))
        {
            footSteps.Stop();
        }
    }

    public void Rotation()
    {
        var cam = cameraObj.GetComponent<Camera>().transform;
        var camEuler = cam.eulerAngles;

        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }

        cam.localEulerAngles = new Vector3(cam.localEulerAngles.x, 0, cam.localEulerAngles.z);

        if (camEuler.x > viewHeightMax && camEuler.x < (viewHeightMax + 10))
            cameraObj.transform.eulerAngles = new Vector3(viewHeightMax, cameraObj.transform.eulerAngles.y, camEuler.z);
        else if (camEuler.x < viewHeightMin && camEuler.x > (viewHeightMin - 10))
            cameraObj.transform.eulerAngles = new Vector3(viewHeightMin, cameraObj.transform.eulerAngles.y, camEuler.z);
        else
            cameraObj.transform.eulerAngles -= new Vector3(Input.GetAxis("Mouse Y"), 0, 0);
    }

    public void PauseScreen()
    {
        stopMoving = !stopMoving;
        pauseScreen.SetActive(stopMoving);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Awake()
    {
        phoneAmbient.TogglePlaying(true);
    }

    [SerializeField] Material materialForWhite;

    public void ChangeWallTexture()
    {
        walls.GetComponent<Renderer>().material = materialForWhite;
    }
}
