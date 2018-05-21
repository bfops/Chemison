using UnityEngine;

public abstract class Transmutable : MonoBehaviour
{
  public static T Spawn<T>(Vector3 position, Quaternion rotation) where T : Transmutable
  {
    return Instantiate(Resources.Load<GameObject>("Transmutable"), position, rotation).AddComponent<T>();
  }

  protected T Change<T>() where T : Transmutable
  {
    var r = gameObject.AddComponent<T>();
    Destroy(this);
    return r;
  }

  public abstract void Apply(Element element);
}
