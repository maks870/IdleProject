using UnityEngine;
using YG;

public class InputControls : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private Player player;

    private void Start()
    {
        player = Player.instance;

#if !UNITY_EDITOR
        if (YandexGame.EnvironmentData.isDesktop)
            joystick.isDesktop = true;
        else
            joystick.isDesktop = false;
#else
        joystick.isDesktop = true;
#endif
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
