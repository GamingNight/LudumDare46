using UnityEngine;

public class Attackers
{

    public Resources.ResourcesType target = Resources.ResourcesType.D;
    public float setLastTurn = 15f;
    public float setRedGate = 0.2f;
    public float setGreenGate = 0.6f;
    public Attackers(Resources.ResourcesType attackersTarget)
    {
        target = attackersTarget;
    }

    public int GetPower(int roundCount)
    {
        float turn = (float)roundCount; // f(x) = x
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
        float g = setGreenGate;
        //  attack power formula
        float res = a * Mathf.Pow(turn, b) + Mathf.Pow(2f, turn) * Mathf.Pow(turn / c, d);
        // transiton blue to red gate at turn 7
        res = res * Mathf.Min((e / 3.5f / 3.5f * (turn - 3f) * (turn - 10f) + 1f), 1);
        Debug.Log("res=" + res);
        float M1 = Mathf.Min((g / 3.5f / 3.5f * (turn - 8f) * (turn - 15f) + 1f),1);
        float M2 = Mathf.Min((g /1.5f / 3.5f / 3.5f * (turn - 11.5f) * (turn - 18.5f) + 1f),1);
        float M3 = Mathf.Min((g /2f / 3.5f / 3.5f * (turn - 15f) * (turn - 22f) + 1f),1);
        //res = res * (Mathf.Min(M1+M2+M3,1));
        res = res * (M1 + M2 + M3)/3;
        res = res * Mathf.Min((g / 3.5f / 3.5f * (turn - 8f) * (turn - 15f) + 1f), 1);
        Debug.Log("resgate=" + res);
        float logres = Mathf.Max(0, 4.5f * Mathf.Log10(Mathf.Max(res, 1f)) - 1);
        int logresint = (int)logres;
        Debug.Log("logres=" + logresint);
        int intres = (int)res;
        //intres = 0;
        return logresint;
    }
}