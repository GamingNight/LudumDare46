using UnityEngine;

public class Attackers {

    public Resources.ResourcesType target = Resources.ResourcesType.D;
    public float setLastTurn=15f;
    public float setRedGate=0.2f;
    public float setGreenGate;
    public Attackers(Resources.ResourcesType attackersTarget) {
        target = attackersTarget;
    }

    public int GetPower(int roundCount) {
        float turn = (float) roundCount; // f(x) = x
                                         //      float rand_range = 0.1f * result;
                                         //      int min = (int) (result - rand_range);
                                         //      int max = (int) (result + rand_range);
                                         //      int res = (int) Random.Range(min, max);                                                
        // set game over turn and speed of complete noob
        float a = 0.33f;
        float b = 2.2f;
        // set game over and speed of "full blue" player
        float c = setLastTurn;
        float d = 4f;
        // set gate depth
        float e = setRedGate;
        // set gate depth
        float g = 0.3f;
        //  attack power formula
        float res = a * Mathf.Pow(turn, b)+Mathf.Pow(2f,turn)*Mathf.Pow(turn/c,d);
        // transiton blue to red gate at turn 7
        res = res * Mathf.Min((e/3.5f/3.5f*(turn-3f)*(turn-10f)+1f),1);
        // transiton red to green gate at turn 7
        res = res * Mathf.Min((g / 2f / 3f * (turn - 7f) * (turn - 12f) + 1f), 1);
        Debug.Log("res="+res);
        float logres = Mathf.Max(0,4.5f * Mathf.Log10(Mathf.Max(res, 1f))-1);
        int logresint = (int)logres;
        Debug.Log("logres=" + logresint);
        int intres = (int)res;
        //intres = 0;
        return logresint;
    }
}
