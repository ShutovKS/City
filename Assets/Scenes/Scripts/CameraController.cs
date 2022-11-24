using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ai.nanosemantics;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    private float cx;
    private float cy;
    private TMP_Text money;
    private int moneyPlayer = 18000000;
    public ASRWithVAD aSR;
    [SerializeField] private float _speed;
    [SerializeField] private float mouseSensivity;


    void Start()
    {
        aSR.OnAsrMessage += Processing;
        //Cursor.visible = false; //������ � ������ ������� ������
    }



    void Processing(string data)
    {
        Voice_Command voice_Command = DOS.RecognisedText(data);
        Debug.Log(voice_Command.commandClass);
        Debug.Log(voice_Command.building);
        //Debug.Log(voice_Command.holiday);
        Debug.Log(voice_Command.sector);

        if (voice_Command.commandClass != null)
        {
            switch (voice_Command.commandClass)
            {
                case "�������":
                    {
                        if (voice_Command.building != null)
                            if (voice_Command.sector != null)
                            {
                                if (GameObject.Find($"building {voice_Command.sector}") == null)
                                {
                                    //Create building (�������� ������)
                                    GameObject a1 = Instantiate(GameObject.Find(voice_Command.building), GameObject.Find(voice_Command.sector).transform.position, Quaternion.identity);
                                    //Rename building (������������� ������ ������)
                                    a1.name = $"building {voice_Command.sector}";
                                    //Sectore noActive (��������� ������ ��� �������)
                                    GameObject.Find(voice_Command.sector).SetActive(false);
                                    Debug.Log($"��������� ������ '{voice_Command.building}' � ������� {voice_Command.sector}. ������ '{voice_Command.building}' ������������� � building {voice_Command.sector}");
                                    //���������� �����
                                    moneyPlayer = moneyPlayer - 4000000;
                                }
                            }
                        return;
                    }

                case "������":
                    {
                        if (voice_Command.sector != null)
                        {
                            if (GameObject.Find($"building {voice_Command.sector}") != null)
                            {
                                //Delete building (������� ������� ������)
                                Destroy(GameObject.Find($"building {voice_Command.sector}"));
                                //Sectore Active (������������ ������ ��� ������ �������)
                                GameObject.Find(voice_Command.sector).SetActive(true);
                            }
                        }
                        return;
                    }

                case "���������": //� ��������� ���������
                    {
                        //��������� �� ��� �������� �������
                        //��������� ��� ������ ������ �� �����
                        if (GameObject.Find($"building {voice_Command.sector}") != null)
                        {
                            //Create building (�������� ������ ������)
                            GameObject a1 = Instantiate(GameObject.Find(voice_Command.building), GameObject.Find(voice_Command.sector).transform.position, Quaternion.identity);
                            //Rename building (������������� ������ ������ ������)
                            a1.name = $"building {voice_Command.sector}";
                            //Sectore noActive (��������� ������ ��� �������)
                            GameObject.Find(voice_Command.sector).SetActive(false);
                            //Delete building (������� ������� ������)
                            Destroy(GameObject.Find($"building {voice_Command.sector}"));
                            //Sectore Active (������������ ������ ��� ������ �������)
                            GameObject.Find(voice_Command.sector).SetActive(true);
                        }
                        return;
                    }

            }
        }
    }

    void Update()
    {
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
}


/*["��������", "�����������", "�����", "���������", "�����", "�����", "���",
   "�������", "��������", "����� �������� �����", "����� �������� �������",
   "����� �������� �������", "����������� �������", "�������� �����",
   "�������� �����", "��������������", "���������", "������ ����"] */