using UnityEngine;

public class Wood : Transmutable
{
  class Branch
  {
    public int depth;
    public VoxelStorage.Vector direction;
  }

  int height;
  Branch branchSettings;

  float timeElapsed = 0;
  bool hasSpawnedChild = false;
  bool hasSpawnedBranches = false;
  bool hasSpawnedLeaves = false;

  public override Element GetElement()
  {
    return Element.Wood;
  }

  public void InitTrunk(int height)
  {
    this.height = height;
    branchSettings = null;
  }

  public void InitBranch(int height, int branchDepth, VoxelStorage.Vector direction)
  {
    this.height = height;
    branchSettings = new Branch { depth = branchDepth, direction = direction };
  }

  private void Start()
  {
    GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("LightBrown");
  }

  int BranchDepth()
  {
    if (branchSettings != null)
    {
      return branchSettings.depth;
    }
    return 0;
  }

  void SpawnBranch(VoxelStorage.Vector direction)
  {
    Spawn<Wood>(point + direction)
      .InitBranch(height: height, branchDepth: BranchDepth() + 1, direction: direction);
  }

  void SpawnLeaves(VoxelStorage.Vector direction)
  {
    Spawn<Leaf>(point + direction);
  }

  private void Update()
  {
    if (branchSettings == null && !hasSpawnedChild && timeElapsed >= 2 * (1 << height))
    {
      Spawn<Wood>(point + new VoxelStorage.Vector(0, 1, 0))
        .InitTrunk(height + 1);
      hasSpawnedChild = true;
    }

    if (!hasSpawnedBranches && height % 4 == 3 && timeElapsed >= 0.5f * (1 << (height + BranchDepth())))
    {
      if (branchSettings == null)
      {
        for (int i = 0; i < 4; ++i)
        {
          SpawnBranch(new VoxelStorage.Vector(((i & 1) == 0 ? 1 : -1), 0, ((i & 2) == 0 ? 1 : -1)));
        }
        for (int i = 0; i < 2; ++i)
        {
          SpawnBranch(new VoxelStorage.Vector(((i & 1) == 0 ? 1 : -1), 0, 0));
        }
        for (int i = 0; i < 2; ++i)
        {
          SpawnBranch(new VoxelStorage.Vector(0, 0, ((i & 1) == 0 ? 1 : -1)));
        }
      }
      else
      {
        SpawnBranch(branchSettings.direction);
      }
      hasSpawnedBranches = true;
    }

    if (!hasSpawnedLeaves && branchSettings != null && branchSettings.depth % 2 == 1 && timeElapsed >= 0.125 * (1 << (height + BranchDepth())))
    {
      for (int i = 0; i < 2; ++i)
      {
        SpawnLeaves(new VoxelStorage.Vector(((i & 1) == 0 ? 1 : -1), 0, 0));
      }
      for (int i = 0; i < 2; ++i)
      {
        SpawnLeaves(new VoxelStorage.Vector(0, ((i & 1) == 0 ? 1 : -1), 0));
      }
      hasSpawnedLeaves = true;
    }

    timeElapsed += Time.deltaTime;
  }

  public override void Apply(Element element)
  {
    if (element == Element.Fire)
    {
      Change<Fire>();
    }
  }
}
