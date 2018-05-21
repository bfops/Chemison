using UnityEngine;

/// <summary>
/// Cache the result of GetComponent when this is called for the first time.
/// </summary>
public class LazyGetComponent<T> where T : Component
{
  bool initialized = false;
  T component = null;

  bool inChildren;
  bool includeInactive;

  private LazyGetComponent()
  {
  }

  public static LazyGetComponent<T> InSiblings()
  {
    var r = new LazyGetComponent<T>();
    r.inChildren = false;
    r.includeInactive = false;
    return r;
  }

  public static LazyGetComponent<T> InChildren(bool includeInactive = false)
  {
    var r = new LazyGetComponent<T>();
    r.inChildren = true;
    r.includeInactive = includeInactive;
    return r;
  }

  public T Get(MonoBehaviour inThis)
  {
    if (!initialized)
    {
      if (inChildren)
      {
        component = inThis.GetComponentInChildren<T>(includeInactive);
      }
      else
      {
        component = inThis.GetComponent<T>();
      }
      initialized = true;
    }
    return component;
  }
}
