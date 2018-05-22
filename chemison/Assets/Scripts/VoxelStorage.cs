using System.Collections.Generic;
using UnityEngine;

public class VoxelStorage : Singleton<VoxelStorage>
{
  public struct Point
  {
    public int x, y, z;

    public Vector3 ToVector3()
    {
      return new Vector3(x, y, z);
    }

    public static Point operator +(Point p, Vector v)
    {
      return new Point
      {
        x = p.x + v.x,
        y = p.y + v.y,
        z = p.z + v.z,
      };
    }

    public override string ToString()
    {
      return string.Format("VoxelStorage.Point {{x={0}, y={1}, z={2}}}", x, y, z);
    }
  }

  public struct Vector
  {
    public int x, y, z;

    public Vector(int x, int y, int z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public static Point OfVector3(Vector3 p)
    {
      return new Point { x = (int)p.x, y = (int)p.y, z = (int)p.z };
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
      return new Vector
      {
        x = v1.x + v2.x,
        y = v1.y + v2.y,
        z = v1.z + v2.z,
      };
    }
  }

  Dictionary<Point, Transmutable> entries = new Dictionary<Point, Transmutable>();

  public void AddOrReplace(Point p, Transmutable entry)
  {
    entries[p] = entry;
  }

  public Transmutable Lookup(Point p)
  {
    Transmutable r = null;
    entries.TryGetValue(p, out r);
    return r;
  }

  public void Remove(Point p)
  {
    entries.Remove(p);
  }
}
