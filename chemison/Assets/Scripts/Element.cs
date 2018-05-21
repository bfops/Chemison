public enum Element { Fire, Water, Rock, Dirt, Wood, Leaf }

public static class ElementExtensions
{
  public static Element React(this Element e1, Element e2)
  {
    if (e1 == Element.Fire)
    {
      if (e2 == Element.Water)
      {
        return Element.Rock;
      }
      if (e2 == Element.Rock)
      {
        return Element.Rock;
      }
      if (e2 == Element.Dirt)
      {
        return Element.Dirt;
      }
    }
    if (e1 == Element.Water)
    {
      if (e2 == Element.Fire)
      {
        return Element.Rock;
      }
      if (e2 == Element.Rock)
      {
        return Element.Dirt;
      }
      if (e2 == Element.Dirt)
      {
        return Element.Dirt;
      }
    }
    if (e1 == Element.Dirt)
    {
      if (e2 == Element.Fire)
      {
        return Element.Rock;
      }
      if (e2 == Element.Rock)
      {
        return Element.Rock;
      }
    }
    if (e1 == Element.Rock)
    {
      if (e2 == Element.Water)
      {
        return Element.Dirt;
      }
    }
    return e1;
  }
}