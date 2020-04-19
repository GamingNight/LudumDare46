using UnityEngine;

public class Attackers {

    public Resources.ResourcesType target = Resources.ResourcesType.D;

    public Attackers(Resources.ResourcesType attackersTarget) {
        target = attackersTarget;
    }

    public int GetPower(int roundCount) {
        float turn = (float) roundCount; // f(x) = x
                                         //      float rand_range = 0.1f * result;
                                         //      int min = (int) (result - rand_range);
                                         //      int max = (int) (result + rand_range);
                                         //      int res = (int) Random.Range(min, max);
        float res = 2.0f * 1.0f * 0.1f * (turn + 1.0f) * (turn + 2.0f) / 2.0f;
        int intres = (int)res + 1;
        Debug.Log(intres);
        Debug.Log("attack");
        return intres;
    }
}
