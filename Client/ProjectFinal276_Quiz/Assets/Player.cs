using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
      Text name;

    [SerializeField]
      Text status;



    public bool isLocalPlayer =false;
    void Start()
    {
        name = name.GetComponent<Text>();
        status = status.GetComponent<Text>();
    }

    public void setstatus(string statuss)
    {
       
        status.text = statuss;
    }

    public void setname(string names,string statuss)
    {
        name.text = names;
        status.text = statuss;
        
    }
}
