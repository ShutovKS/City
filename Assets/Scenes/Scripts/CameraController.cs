using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ai.nanosemantics;
using Random = UnityEngine.Random;


public class CameraController : MonoBehaviour
{
    Voice_Command voice_Command1 = new Voice_Command();

    private float cx;
    private float cy;
    [SerializeField] private float _speed;
    [SerializeField] private float mouseSensivity;
    public TMP_Text money;
    public int moneyPlayer = 1000000;
    public ASRWithVAD aSR;



    // Start is called before the first frame update
    void Start()
    {
        aSR.OnAsrMessage += Processing;
        //Cursor.visible = false; //Убрать с экрана мышиный курсор
    }



    void Processing(string data)
    {
        //DOS.RecognisedText(data);

        Voice_Command voice_Command = DOS.RecognisedText(data);
        Debug.Log(voice_Command.commandClass);
        Debug.Log(voice_Command.building);
        Debug.Log(voice_Command.holiday);
        Debug.Log(voice_Command.sector);



        Instantiate(GameObject.Find(voice_Command.building), GameObject.Find(voice_Command.sector).transform.position, Quaternion.identity);
        moneyPlayer = moneyPlayer - 40000;
    }


    void Update()
    {
        money.text = "" + moneyPlayer;
        if (Input.GetMouseButton(1))
        {
            CameraRotation();
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.Rotate(-2, 0, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.Rotate(2, 0, 0);
        }
        Movement();
    }

    private void CameraRotation()
    {
        cx += Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, cx, 0);
        cy -= Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        cy = Mathf.Clamp(cy, 20, 90);
        transform.rotation = Quaternion.Euler(cy, cx, 0);


    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, _speed * Time.deltaTime * -1, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, _speed * Time.deltaTime, 0);
        }
    }

    [System.Serializable]
    public class SectorContainer
    {
        public string name;
        public List<GameObject> Section;

    }
}
