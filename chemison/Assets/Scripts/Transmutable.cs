using UnityEngine;

public abstract class Transmutable : MonoBehaviour
{
  public VoxelStorage.Point point { get; private set; }

  public abstract Element GetElement();

  static void UpdateNeighbors(VoxelStorage.Point point, Element element)
  {
    var neighbors =
      new VoxelStorage.Vector[]
      {
        new VoxelStorage.Vector { x =  0, y =  0, z = -1 },
        new VoxelStorage.Vector { x =  0, y =  0, z =  1 },
        new VoxelStorage.Vector { x =  0, y = -1, z =  0 },
        new VoxelStorage.Vector { x =  0, y =  1, z =  0 },
        new VoxelStorage.Vector { x = -1, y =  0, z =  0 },
        new VoxelStorage.Vector { x =  1, y =  0, z =  0 },
      };
    foreach (var neighbor in neighbors)
    {
      var transmutable = VoxelStorage.instance.Lookup(point + neighbor);
      if (transmutable != null)
      {
        Debug.Log("Enqueue " + (point+neighbor) + " " + element + " from " + point);
        TransmutableUpdater.instance.Enqueue(transmutable, element);
      }
    }
  }

  public static T Spawn<T>(VoxelStorage.Point point) where T : Transmutable
  {
    GameObject gameObject;
    {
      Transmutable prev = VoxelStorage.instance.Lookup(point);
      if (prev != null)
      {
        gameObject = prev.gameObject;
        Destroy(prev);
      }
      else
      {
        gameObject = Instantiate(Resources.Load<GameObject>("Transmutable"), point.ToVector3(), Quaternion.identity);
      }
    }
    var spawned = gameObject.AddComponent<T>();
    spawned.point = point;
    VoxelStorage.instance.AddOrReplace(point, spawned);
    UpdateNeighbors(point, spawned.GetElement());
    return spawned;
  }

  protected T Change<T>() where T : Transmutable
  {
    var r = gameObject.AddComponent<T>();
    r.point = point;
    VoxelStorage.instance.AddOrReplace(r.point, r);
    Destroy(this);
    UpdateNeighbors(r.point, r.GetElement());
    return r;
  }

  public abstract void Apply(Element element);
}
