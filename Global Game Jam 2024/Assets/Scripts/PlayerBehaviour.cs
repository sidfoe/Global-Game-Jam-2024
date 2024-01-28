using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Moving")]
    public float speed = 25; // multiplier for movement
    //public float rotateSpeed = 100; // multiplier for turning
    public float jumpHeight;

    [Header("Camera")]
    [SerializeField] float rotationSpeed = 180.0f;
    [SerializeField] float pitchSpeed = 180.0f;
    [SerializeField] float playerLookInputLerpTime = .35f;

    public Transform cameraFollow;

    [SerializeField] InputController input;

    public List<Image> itemPickedUp = new List<Image>();

    private Rigidbody rb; // to walk, move body, not this

    Vector3 playerMoveInput = Vector3.zero;
    Vector3 playerLookInput = Vector3.zero;
    Vector3 prevPlayerLookInput = Vector3.zero;

    private float cameraPitch = 0.0f;


    //public List<ObjectBehaviour> collectedObjects = new List<ObjectBehaviour>(5) { null, null, null, null, null };
    private ObjectBehaviour[] collectedObjects = new ObjectBehaviour[5];
    private ObjectBehaviour objectPickupable = null;

    public Color pickedUpColor;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerLookInput = GetLookInput();
        Look();
        PitchCamera();

        playerMoveInput = GetMoveInput();
        Move();

        rb.AddRelativeForce(playerMoveInput, ForceMode.Force);
    }

    private void Update()
    {
        if (input.interactInputs && objectPickupable)
        {
            if (collectedObjects[objectPickupable.type] == null)
            {
                collectedObjects[objectPickupable.type] = objectPickupable;
                itemPickedUp[objectPickupable.type].color = pickedUpColor;
                objectPickupable.gameObject.SetActive(false);
            }
            else
            {
                collectedObjects[objectPickupable.type].gameObject.SetActive(true);
                collectedObjects[objectPickupable.type] = objectPickupable;
                objectPickupable.gameObject.SetActive(false);
            }
        }
    }

    private Vector3 GetLookInput()
    {
        prevPlayerLookInput = playerLookInput;
        playerLookInput = new Vector3(input.lookInputs.x, (input.InvertMouseY ? -input.lookInputs.y : input.lookInputs.y), 0.0f);
        return Vector3.Lerp(prevPlayerLookInput, playerLookInput * Time.deltaTime, playerLookInputLerpTime);
    }

    private void Look()
    {
        rb.rotation = Quaternion.Euler(0.0f, rb.rotation.eulerAngles.y + (playerLookInput.x * rotationSpeed), 0.0f);
    }

    private void PitchCamera()
    {
        Vector3 rotationValues = cameraFollow.rotation.eulerAngles;
        cameraPitch += playerLookInput.y * pitchSpeed;
        cameraPitch = Mathf.Clamp(cameraPitch, 44.9f, 44.9f);

        cameraFollow.rotation = Quaternion.Euler(cameraPitch, rotationValues.y, rotationValues.z);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(input.moveInputs.x, 0.0f, input.moveInputs.y);
    }

    private void Move()
    {
        playerMoveInput = (new Vector3(playerMoveInput.x * speed * rb.mass, playerMoveInput.y, playerMoveInput.z * speed * rb.mass));
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("object"))
        {
            objectPickupable = col.GetComponent<ObjectBehaviour>();
            col.GetComponent<Highlight>()?.ToggleHighlight(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("object"))
        {
            col.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
    }

    public int CalculateScore(int selectGen, int selectAge)
    {
        int score = 0;

        foreach(ObjectBehaviour ob in collectedObjects)
        {
            if(ob == null)
            {
                score -= 10;
                continue;
            }

            if(ob.age == selectAge)
            {
                score += 10;
            }

            if(ob.gender == selectGen)
            {
                score += 10;
            }

            //didnt get anything correct no points
            //max points 100. 20 per item

        }

        return score; 
    }
}
