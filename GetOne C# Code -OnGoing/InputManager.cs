using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    TouchControls touchControls;
    Camera cameraMain;

    private void Awake()
    {
        touchControls = new TouchControls();
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += context => StartTouch(context);
        touchControls.Touch.TouchPress.canceled += context => EndTouch(context);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        GameEventSystem.current.StartTouch(ConvertTapPosToScreenPos(touchControls.Touch.TouchPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    void EndTouch(InputAction.CallbackContext context) {
        GameEventSystem.current.EndTouch(ConvertTapPosToScreenPos(touchControls.Touch.TouchPosition.ReadValue<Vector2>()), (float)context.time);
    }

    Vector3 ConvertTapPosToScreenPos(Vector2 tpos)
    {
        return new Vector3(tpos.x, tpos.y, cameraMain.nearClipPlane);
    }

    public Vector3 ConvertScreenposToWorldPos(Vector3 screenCoords)
    {
        Vector3 worldCoords = cameraMain.ScreenToWorldPoint(screenCoords);
        worldCoords.z = 0;
        return worldCoords;
    }
}
