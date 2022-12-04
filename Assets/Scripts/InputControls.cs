using UnityEngine;
using YG;

public class InputControls : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
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
#if !UNITY_EDITOR
        if (YandexGame.EnvironmentData.isDesktop)
            joystick.isDesktop = true;
        else
        {
            joystick.isDesktop = false;
            cam.orthographicSize = 10;
        }
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
