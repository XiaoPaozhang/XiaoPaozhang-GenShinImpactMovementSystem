using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerInput : MonoBehaviour
  {

    //这个是输入系统的实例
    public PlayerInputActions inputActions { get; private set; }
    //这个相当于新输入系统中的actions的实例
    //PlayerActions 类名的由来是你自定义的actions名称+自动加的Actions后缀
    //比如你在新系统中添加了个Player行为,那这个类的名字就是PlayerActions
    public PlayerInputActions.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
      inputActions = new PlayerInputActions();

      playerActions = inputActions.Player;
    }

    private void OnEnable()
    {
      inputActions.Enable();
    }
    private void OnDisable()
    {

      inputActions.Disable();
    }

    public void DisableActionFor(InputAction action, float seconds)
    {
      DisableAction(action, seconds);
    }

    private IEnumerator DisableAction(InputAction action, float seconds)
    {
      action.Disable();

      yield return new WaitForSeconds(seconds);

      action.Enable();
    }
  }
}
