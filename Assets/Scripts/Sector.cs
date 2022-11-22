using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using ai.nanosemantics;
using UnityEngine.UI;
using Random = UnityEngine.Random;



public class Sector : MonoBehaviour
{
    public GameObject CivilHouse;
    public GameObject HospitalGO;
    public GameObject FireDepartGO;
    public GameObject PoliceGO;
    public bool fl = false;
    public bool Hospfl = false;
    public bool Policefl = false;
    public bool Firefl = false;
    private int check = -2000;
    public GameObject Camera;


    // Start is called before the first frame update
    void Start()
    {

    }


    
    // Update is called once per frame
    void Update()
    {
        
        if (fl == true)
        {
            CivilSpawnObject();
        }
        if (Hospfl == true)
        {
            HospitalSpawnObject();
        }
        if (Policefl == true)
        {
            PoliceSpawnObject();
        }
        if (Firefl == true)
        {
            FireDepSpawnObject();
        }
    }


    private void CivilSpawnObject()
    {
        GameObject a1 = (GameObject)Instantiate(CivilHouse, transform.position, Quaternion.identity);
        //gameObject.SetActive(false);
        Camera.GetComponent<CameraController>().allmoney += check;
    }

    private void HospitalSpawnObject()
    {
        GameObject a1 = (GameObject)Instantiate(HospitalGO, transform.position, Quaternion.identity);
       // gameObject.SetActive(false);
        Camera.GetComponent<CameraController>().allmoney += check*4;
    }
    private void PoliceSpawnObject()
    {
        GameObject a1 = (GameObject)Instantiate(PoliceGO, transform.position, Quaternion.identity);
       // gameObject.SetActive(false);
        Camera.GetComponent<CameraController>().allmoney += check * 4;
    }
    private void FireDepSpawnObject()
    {
        GameObject a1 = (GameObject)Instantiate(FireDepartGO, transform.position, Quaternion.identity);
        //gameObject.SetActive(false);
        Camera.GetComponent<CameraController>().allmoney += check * 4;
    }

}
