using UnityEngine;

public class Leaf : Transmutable
{
  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Green");
  }

  public override void Apply(Element element)
  {
    if (element == Element.Fire)
    {
      Change<Fire>();
    }
  }
}
