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
        float res = 0.33f * Mathf.Pow(turn, 2.2f)+Mathf.Pow(2f,turn)*Mathf.Pow(turn/25f,4);
        Debug.Log("res="+res);
        int intres = (int)res;
        //intres = 0;
        return intres;
    }
}
