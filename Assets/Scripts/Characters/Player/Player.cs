using Unity.VisualScripting;
using UnityEngine;

namespace XFramework.FSM
{
  //将Player组件添加到对象上时,也会自动添加 PlayerInput组件到对象上
  [RequireComponent(typeof(PlayerInput))]
  public class Player : MonoBehaviour
  {
    [field: Header("References")]
    [field: SerializeField] public PlayerSO data { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
    public Rigidbody rb { get; private set; }
    public PlayerInput Input { get; private set; }
    public Transform MainCameraTransform { get; private set; }
    private PlayerMovementStateMachine movementStateMachine;
    private void Awake()
    {
      rb = GetComponent<Rigidbody>();

      Input = GetComponent<PlayerInput>();

      MainCameraTransform = Camera.main.transform;

      movementStateMachine = new PlayerMovementStateMachine(this);
    }
    private void OnValidate()
    {

      ColliderUtility.Initialize(this.gameObject);
      ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
      movementStateMachine.ChangeState(movementStateMachine.idlingState);
    }

    private void Update()
    {
      movementStateMachine.HandleInput();

      movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
      movementStateMachine.PhysicsUpdate();
    }
  }
}
