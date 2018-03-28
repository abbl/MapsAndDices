using UnityEngine;

public class Dice {
    public static int Roll(int min, int max)
    {
        return Random.Range(min, max);
    }
}
