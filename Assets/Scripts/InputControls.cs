using UnityEngine;
using YG;

public class InputControls : MonoBehaviour
{
    [SerializeField] private Joystick joystickDesktop;
    [SerializeField] private Joystick joystickMobile;
    private Joystick joystick;
    private Player player;
    private Camera cam;
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        cam = Camera.main;
        player = Player.instance;


        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    private void GetLoad()
    {
        SwitchJoystick(YandexGame.EnvironmentData.isDesktop);
    }

    private void SwitchJoystick(bool desktop)
    {
#if !UNITY_EDITOR
        joystickDesktop.gameObject.SetActive(desktop);
        joystickMobile.gameObject.SetActive(!desktop); 
#else
        joystickDesktop.gameObject.SetActive(true);
        joystickMobile.gameObject.SetActive(false);
#endif
    }

    private void Update()
    {
        player.SetInputAxis(joystickDesktop.Direction + joystickMobile.Direction + GetDirection());
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
