using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public PlayerUI playerUi;
    private int interactableCollisionCount = 0;

    private InteractableObject latestInteractable;

    private InputAction interactAction;

    void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void OnEnable()
    {
        interactAction.Enable();
    }

    void OnDisable()
    {
        interactAction.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractableCollider"))
        {
            Transform parent = other.transform.parent;
            if (parent.GetComponent<InteractableObject>().isInteractable)
            {
                interactableCollisionCount++;
                playerUi.showInteract = true;
                latestInteractable = parent.GetComponent<InteractableObject>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableCollider"))
        {
            Transform parent = other.transform.parent;
            if (parent.GetComponent<InteractableObject>().isInteractable)
            {
                interactableCollisionCount--;
                if (interactableCollisionCount == 0)
                {
                    playerUi.showInteract = false;
                    latestInteractable = null;
                }
            }
        }
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame() && interactAction.IsPressed())
        {
            if (latestInteractable != null)
            {
                // Use latestInteractable (The Interactable Object)
            }
        }
    }
}
