using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject hud;
    public Text speedometer;
    public Movement playerMovement;
    // Start is called before the first frame update
    void Awake()
    {
        hud = GameObject.Find("HUD");
        speedometer = GameObject.Find("speedometer").GetComponent<Text>();
        playerMovement = GameObject.Find("player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 horizontal = new Vector2(playerMovement.rb.velocity.x, playerMovement.rb.velocity.z);
        speedometer.text = horizontal.magnitude.ToString("F0") + " u/s";
    }
}
