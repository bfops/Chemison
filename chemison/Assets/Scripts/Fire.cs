using UnityEngine;

public class Fire : Transmutable
{
  public override Element GetElement()
  {
    return Element.Fire;
  }

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Orange");
  }

  public override void Apply(Element element)
  {
    if (element == Element.Water)
    {
      Change<Rock>();
    }
  }
}
