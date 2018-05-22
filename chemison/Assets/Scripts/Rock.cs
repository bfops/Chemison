using UnityEngine;

public class Rock : Transmutable
{
  public override Element GetElement()
  {
    return Element.Rock;
  }

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Grey");
  }

  public override void Apply(Element element)
  {
    if (element == Element.Water)
    {
      Change<Dirt>();
    }
  }
}
