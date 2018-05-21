using UnityEngine;

public class Dirt : Transmutable
{
  float? timeElapsed = 0;

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("Brown");
  }

  private void Update()
  {
    if (timeElapsed >= 2)
    {
      Spawn<Wood>(transform.position + new Vector3(0, 1, 0), transform.rotation)
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
