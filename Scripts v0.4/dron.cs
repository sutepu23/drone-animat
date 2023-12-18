using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dron : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class DroneSmellTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Логика, выполняемая при входе в зону
        if (other.CompareTag("SomethingToSmell"))
        {
            // Ваш код обработки обоняния
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Логика, выполняемая при выходе из зоны
        if (other.CompareTag("SomethingToSmell"))
        {
            // Ваш код обработки завершения обоняния
        }
    }
}
