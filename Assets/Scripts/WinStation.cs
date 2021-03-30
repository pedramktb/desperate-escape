using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStation : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPCBehaviour>().IsSafe = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPCBehaviour>().IsSafe = false;
        }
    }
}
