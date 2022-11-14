using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControls : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Player player;

    private void Start()
    {
        player = Player.instance;     
    }

    private void Update()
    {
        player.SetInputAxis(joystick.Direction + GetDirection());
    }

    private Vector2 GetDirection() 
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
