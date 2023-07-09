using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public ClientSocket clientSocketScript;

    // Start is called before the first frame update
    void Start()
    {
        clientSocketScript = GameObject.FindGameObjectWithTag("Network").GetComponent<ClientSocket>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void write0()
    {
        clientSocketScript.writeSocket("0");
    }

    public void write1()
    {
        clientSocketScript.writeSocket("1");
    }
}
