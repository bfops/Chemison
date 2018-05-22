using UnityEngine;

public class Dirt : Transmutable
{
  float? timeElapsed = 0;

  public override Element GetElement()
  {
    return Element.Dirt;
  }

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Brown");
  }

  private void Update()
  {
    if (timeElapsed >= 2)
    {
      Spawn<Wood>(point + new VoxelStorage.Vector(0, 1, 0))
        .InitTrunk(height: 0);
      timeElapsed = null;
    }

    if (timeElapsed != null)
    {
      timeElapsed += Time.deltaTime;
    }
  }

  public override void Apply(Element element)
  {
    if (element == Element.Fire)
    {
      Change<Rock>();
    }
  }
}
