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
    public int allmoney = 1000000000;
    public ASRWithVAD aSR;
    public SectorContainer[] sectors;
    public List<GameObject> sectorsZ = new List<GameObject>();
    public GameObject HospitalGO;
    public List<GameObject> Zdaniya = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        aSR.OnAsrMessage += Processing;

    }



    void Processing(string data)
    {
        //DOS.RecognisedText(data);
       
        Voice_Command voice_Command = DOS.RecognisedText(data);
        Debug.Log(voice_Command.commandClass);
        Debug.Log(voice_Command.building);
        Debug.Log(voice_Command.holiday);
        Debug.Log(voice_Command.sector);
        //string zone = voice_Command1.sector.ToUpper();
        GameObject.Find(voice_Command1.sector);


        for (int num = 0; num < 81; num++)
        {
            string s = Convert.ToString(num);
            if (voice_Command.sector == s)
            {
                GameObject a1 = (GameObject)Instantiate(Zdaniya[Random.Range(0,10)], GameObject.Find(s).transform.position, Quaternion.identity);
                allmoney = allmoney - 40000;
                break;
            }
            

            //int name = Convert.ToString(sectorsZ[i]);

        }
    }


    // Update is called once per frame
    void Update()
    {
        money.text = "" + allmoney;
        if (Input.GetMouseButton(1))
        {
            CameraRotation();
        }


        if(Input.GetAxis("Mouse ScrollWheel") > 0)
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
            transform.Translate(0,_speed * Time.deltaTime * -1, 0);
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
