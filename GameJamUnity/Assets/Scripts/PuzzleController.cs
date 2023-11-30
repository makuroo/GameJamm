using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Linq;
public class PuzzleController : MonoBehaviour
{
    public float timeBetweenLights = 1.0f;

    private int currentIndex = 0;
    [SerializeField]
    private bool isInputActive = false;

    public GameObject[] nodesUrutanLight;
    public GameObject[] nodes;
    //public TMP_Text textPetunjuk;

    [SerializeField]
    private string playerSequence;
    [SerializeField]
    private string correctSequence;
    private string[] storageSequence = { "W", "A", "D", "S", "SD", "WD", "SA", "WA" }; //KunciJawaban
    private int storageIndex = 0;

    bool[] diGembok = new bool[8];
    bool gembokWin = false;
    [SerializeField]
    private int sequenceScore;
    private KeyCode[] excludedKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public PlayerMovement playerMovementScript;
    public GameObject tempatBolaKosong;
    public GameObject tempatBolaAda;
    public Dialogue dialogueScript;
    public bool isWin;


    public void StartThePuzzle()
    {
        Invoke("ExecutePuzzle", 1f);
        playerMovementScript.isRolling = true;
    }

    private void ExecutePuzzle()
    {
        InvokeRepeating("LightHintShow", 0.0f, timeBetweenLights);
        playerMovementScript.movementSpeed = 0f;
        playerSequence = "";
        correctSequence += storageSequence[storageIndex];
    }

    private void LightHintShow()
    {
        if (currentIndex < nodes.Length)
        {
            if (currentIndex > 0)
            {
                nodesUrutanLight[currentIndex - 1].GetComponent<Light2D>().intensity = 0;
            }

            nodesUrutanLight[currentIndex].GetComponent<Light2D>().intensity = 11;

            currentIndex++;
        }
        else
        {
            //SemuaNodesSudahTerang
            CancelInvoke("LightHintShow");
            TurnOnAllLights(Color.yellow);
            Invoke("TurnOffAllLights", 1f);
            Invoke("DelayInputActive", 1f);
        }
    }

    private void TurnOnAllLights(Color lightColor)
    {
        foreach (GameObject nodes in nodes)
        {
            nodes.GetComponent<Light2D>().intensity = 11;
            nodes.GetComponent<Light2D>().color = lightColor;
        }
    }

    private void TurnOffAllLights()
    {
        foreach (GameObject nodes in nodes)
        {
            nodes.GetComponent<Light2D>().intensity = 0;
            nodes.GetComponent<Light2D>().color = Color.yellow;
        }
    }

    private void ResetPuzzle()
    {
        Debug.Log("ResetColors");
        TurnOffAllLights();
        correctSequence = "";
        playerSequence = "";
        storageIndex = 0;
        correctSequence += storageSequence[storageIndex];
        currentIndex = 0;
        isInputActive = false;
        sequenceScore = 0;
    }

    private void CheckPlayerInput()
    {
        if (isInputActive)
        {
            if (Input.GetKeyDown(KeyCode.W) && excludedKeys.Where(key => key != KeyCode.W).All(key => !Input.GetKey(key)))
            {
                nodes[0].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[0])
                {
                    diGembok[0] = true;
                    playerSequence += "W";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Up";
            }

            if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.D))
            {
                nodes[1].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[1])
                {
                    diGembok[1] = true;
                    playerSequence += "WD";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Up Right";
            }

            if (Input.GetKeyDown(KeyCode.D) && excludedKeys.Where(key => key != KeyCode.D).All(key => !Input.GetKey(key)))
            {
                nodes[2].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[2])
                {
                    diGembok[2] = true;
                    playerSequence += "D";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Right";
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
            {
                nodes[3].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[3])
                {
                    diGembok[3] = true;
                    playerSequence += "SD";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Down Right";
            }

            if (Input.GetKeyDown(KeyCode.S) && excludedKeys.Where(key => key != KeyCode.S).All(key => !Input.GetKey(key)))
            {
                nodes[4].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[4])
                {
                    diGembok[4] = true;
                    playerSequence += "S";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Down";
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.A))
            {
                nodes[5].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[5])
                {
                    diGembok[5] = true;
                    playerSequence += "SA";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Down Left";
            }

            if (Input.GetKeyDown(KeyCode.A) && excludedKeys.Where(key => key != KeyCode.A).All(key => !Input.GetKey(key)))
            {
                nodes[6].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[6])
                {
                    diGembok[6] = true;
                    playerSequence += "A";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Left";
            }

            if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.A))
            {
                nodes[7].GetComponent<Light2D>().intensity = 11f;
                if (!diGembok[7])
                {
                    diGembok[7] = true;
                    playerSequence += "WA";
                    CheckSequence();
                    sequenceScore++;
                }
                //textPetunjuk.text = "You are now facing Up Left";
            }
        }
    }

    private void CheckSequence()
    {
        if (sequenceScore != 8)
        {
            if (playerSequence == correctSequence)
            {
                //BETUL SEQUENCE
                Debug.Log("SequenceBetul");
                if (storageIndex != storageSequence.Length - 1)
                {
                    storageIndex++;
                    correctSequence += storageSequence[storageIndex];
                }

            }
            else
            {
                //SALAH SEQUENCE DAN RESTART
                Debug.Log("SequenceSalah");
                TurnOnAllLights(Color.red);
                Invoke("ResetPuzzle", 1.0f);
                Invoke("DelayInputActive", 10f);
                InvokeRepeating("LightHintShow", 1f, timeBetweenLights);
                TextGagalMuncul();
                isInputActive = false;


                for (int i = 0; i < diGembok.Length; i++)
                {
                    diGembok[i] = false;
                }
            }
        }

    }

    private void TextGagalMuncul()
    {
        Debug.Log("textgagalMUNCUL");
        dialogueScript.StartText();
        Invoke("ResetText", 7f);
    }

    private void ResetText()
    {
        Debug.Log("ResetText");
        dialogueScript.textComponent.text = "";
    }

    private void DelayInputActive()
    {
        isInputActive = true;
    }


    private void Update()
    {
        CheckPlayerInput();
        if (sequenceScore == 8 && !gembokWin)
        {
            //MENANG
            Debug.Log("Hooray");
            TurnOnAllLights(Color.green);
            isInputActive = false;
            gembokWin = true;
            playerMovementScript.movementSpeed = 2f;
            tempatBolaKosong.SetActive(false);
            tempatBolaAda.SetActive(true);
            isWin = true;
        }
    }







}