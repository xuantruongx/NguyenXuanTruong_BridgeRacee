using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private Vector2 JoystickSize = new Vector2(300, 300);
    [SerializeField] private NavMeshAgent Player;
    public FloatingJoystick Joystick;

    private Finger MovementFinger;
    private Vector2 MovementAmount;
    public Vector3 MoveDirection;
    public void OnInit()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    //private void OnEnable()
    //{
    //    EnhancedTouchSupport.Enable();
    //    ETouch.Touch.onFingerDown += HandleFingerDown;
    //    ETouch.Touch.onFingerUp += HandleLoseFinger;
    //    ETouch.Touch.onFingerMove += HandleFingerMove;
    //}

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, Joystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
            //if (!Player.GetComponent<Player>().canMoveUp && MovementAmount.y > 0)
            //    MovementAmount.y = 0;

        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < JoystickSize.x / 2)
        {
            StartPosition.x = JoystickSize.x / 2;
        }

        if (StartPosition.y < JoystickSize.y / 2)
        {
            StartPosition.y = JoystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - JoystickSize.y / 2)
        {
            StartPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return StartPosition;
    }

    private void FixedUpdate()
    {
        MoveDirection = new Vector3(MovementAmount.x, 0, MovementAmount.y);
        Vector3 scaledMovement = Player.speed * MoveDirection;

        Player.transform.LookAt(Player.transform.position + MoveDirection, Vector3.up);
        //if (Player.GetComponent<Player>().canMoveUp)
        //{
        //    Player.velocity = scaledMovement;
        //}
        //else
        //{
        //    Player.velocity = Vector3.zero;
        //}
        if (Player.GetComponent<Character>().canMoveUp)
        {
            Player.velocity = scaledMovement;
        }
        else
        {
            Player.velocity = Vector3.zero;
        }
    }

}