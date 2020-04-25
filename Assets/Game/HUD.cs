using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text health;
    public Text stamina;
    // Start is called before the first frame update
    void Start()
    {
        StaticGlobalReferences._Player.GetComponent<HealthManager>().healthHUD = health;
        StaticGlobalReferences._Player.GetComponent<HealthManager>().staminaHUD = stamina;
    }
}
