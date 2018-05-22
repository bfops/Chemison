using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Tooltip("In degrees per second")]
  public float rotationSpeed;
  public float moveSpeed;
  public GameObject view;

  float pitch = 0;

  static readonly Element[] selections = new Element[] { Element.Water, Element.Fire };

  Element selected = Element.Fire;

  protected void Update()
  {
    UpdateMovement();
    UpdateRotation();
    UpdateSelection();
    UpdateFiring();
  }

  protected void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  protected void UpdateMovement()
  {
    float y = Input.GetAxisRaw("Vertical");
    float x = Input.GetAxisRaw("Horizontal");

    var moveInput = transform.TransformVector(new Vector3(x, 0, y));
    moveInput.y = 0;
    if (moveInput.sqrMagnitude > 0)
    {
      var box = GetComponent<BoxCollider>();
      var movement = moveSpeed * Time.deltaTime * moveInput.normalized;
      var center = transform.TransformPoint(box.center);
      if (Physics.OverlapBox(center + movement, box.size/2, transform.rotation).Length <= 2)
      {
        transform.position += movement;
      }
    }
  }

  protected void UpdateRotation()
  {
    {
      var x = Input.GetAxis("Mouse X");
      var rot = transform.rotation.eulerAngles;
      if (Mathf.Abs(x) > 0.01)
      {
        rot.y += rotationSpeed * Time.deltaTime * x;
      }
      transform.rotation = Quaternion.Euler(rot);
    }
    {
      var y = Input.GetAxis("Mouse Y");
      pitch += rotationSpeed * Time.deltaTime * -y;
      view.transform.position = transform.position + transform.forward * 2;
      var quat = Quaternion.AngleAxis(pitch, transform.right);
      view.transform.position = quat * (view.transform.position - Camera.main.transform.position) + Camera.main.transform.position;
    }
  }

  protected void UpdateSelection()
  {
    for (var i = 0; i < selections.Length; ++i)
    {
      if (Input.GetKey(KeyCode.Alpha1 + i))
      {
        selected = selections[i];
      }
    }
  }

  RaycastHit? Raycast()
  {
    RaycastHit hitInfo;
    if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1)), out hitInfo, maxDistance: 25))
    {
      return hitInfo;
    }
    return null;
  }

  protected void UpdateFiring()
  {
    if (Input.GetMouseButtonDown(0))
    {
      var hit = Raycast();
      if (hit.HasValue)
      {
        var transmutable = hit.Value.collider.GetComponentInParent<Transmutable>();
        if (transmutable != null)
        {
          transmutable.Apply(selected);
        }
      }
    }
    if (Input.GetMouseButtonDown(1))
    {
      var hit = Raycast();
      if (hit.HasValue)
      {
        if (selected == Element.Fire)
        {
          Transmutable.Spawn<Fire>(VoxelStorage.Vector.OfVector3(hit.Value.point));
        }
        else if (selected == Element.Water)
        {
          Transmutable.Spawn<Water>(VoxelStorage.Vector.OfVector3(hit.Value.point));
        }
      }
    }
  }
}
