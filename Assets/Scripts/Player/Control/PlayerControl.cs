using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;

public class PlayerControl : MonoBehaviour
{
    Transform _camera;
    AudioSource footsteps, flashlightClick, speech;
    Transform groundChecker;
    CharacterController controller;
    public Transform flashlight;

    Text objectText;

    [Range(5f, 70f)]
    public float mouseSensitivity = 60f;
    [Range(1f, 10f)]
    public float movementSpeed = 1f;

    Inventory inventory;

    float cameraRotation = 0f;
    float playerRotaion = 0f;

    float defaultVolume;

    float gravity = -9.8f;
    public bool grounded;

    public Vector3 movement;
    Vector3 velocity;
    const float SPACE = -2f;
    public float jumpHeight = 0.3f;

    public LayerMask itemsLayer;
    public LayerMask groundLayer;

    public const float PICKUP_DISTANCE = 3f;

    bool moving = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = transform.Find("Camera");
        controller = GetComponent<CharacterController>();
        footsteps = transform.Find("Footsteps").GetComponent<AudioSource>();
        flashlightClick = transform.Find("FlashlightClick").GetComponent<AudioSource>();
        speech = transform.Find("Speech").GetComponent<AudioSource>();
        groundChecker = transform.Find("GroundChecker");
        defaultVolume = footsteps.volume;
        objectText = GameObject.Find("ObjectStatus").GetComponent<Text>();
        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Trigger") && moving) hit.transform.GetComponent<Bottle>().Bump(movement);
    }

    void Update()
    {
        Debug.Log(controller.velocity.magnitude);
        grounded = Physics.CheckSphere(groundChecker.position, 0.3f, groundLayer);

        movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(movement * movementSpeed * Time.deltaTime);

        if (new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude > 0f && grounded)
        {
            moving = true;
            footsteps.volume = defaultVolume;
            if (!footsteps.isPlaying) footsteps.Play();
        }
        else if (footsteps.isPlaying) {
            moving = false;
            if (footsteps.volume > 0) footsteps.volume -= Time.deltaTime * 2.5f;
            else footsteps.Pause();
        }

        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        if (grounded && velocity.y < 0) velocity.y = SPACE;
        cameraRotation -= mouseY * mouseSensitivity * Time.deltaTime;
        playerRotaion = mouseX * mouseSensitivity * Time.deltaTime;

        cameraRotation = Mathf.Clamp(cameraRotation, -90f, 90f);

        _camera.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);
        if (flashlight) flashlight.localRotation = _camera.localRotation;
        transform.Rotate(Vector3.up * playerRotaion);

        if (Input.GetButtonDown("Jump") && grounded) velocity.y = Mathf.Sqrt(jumpHeight * SPACE * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetMouseButtonDown(1) && inventory.SpecialItemExists("flashlight"))
        {
            flashlight.gameObject.SetActive(!flashlight.gameObject.activeSelf);
            flashlightClick.Play();
        }

        RaycastHit hit;
        Ray ray = _camera.GetComponent<Camera>().ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, PICKUP_DISTANCE, itemsLayer))
        {
            Item item = hit.transform.GetComponent<Item>();
            objectText.gameObject.SetActive(true);
            objectText.text = item.displayName.GetLocalizedString();
            if (Input.GetMouseButtonDown(0))
            {
                speech.PlayOneShot(item.speech);
                if (item.pickable) item.PickUp(inventory);
            }
        }
        else objectText.gameObject.SetActive(false);
    }
}
