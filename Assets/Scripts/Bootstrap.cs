
using System;
using DefaultNamespace;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<Bootstrap>().gameObject;
                if (obj == null)
                {
                    obj = new GameObject("Bootstrap");
                    obj.AddComponent<Bootstrap>();
                }
                _instance = obj.GetComponent<Bootstrap>();
            }

            return _instance;
        }
    }

    private static Bootstrap _instance;

    public GasCell gasCell;
    public Parachute parachute;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        gasCell = FindObjectOfType<GasCell>();
        parachute = FindObjectOfType<Parachute>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}