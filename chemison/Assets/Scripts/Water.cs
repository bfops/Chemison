using UnityEngine;

public class Water : Transmutable
{
  public override Element GetElement()
  {
    return Element.Water;
  }

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Blue");
  }

  public override void Apply(Element element)
  {
    if (element == Element.Dirt)
    {
      Change<Dirt>();
    }
    else if (element == Element.Rock)
    {
      Change<Dirt>();
    }
    else if (element == Element.Fire)
    {
      Change<Rock>();
    }
  }
}
