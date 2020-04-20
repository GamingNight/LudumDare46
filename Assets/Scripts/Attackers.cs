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
        // set game over turn and speed of complete noob
        float a = 0.33f;
        float b = 2.2f;
        // set game over and speed of "full blue" player
        float c = 40f;
        float d = 4f;
        //  attack power formula
        float res = a * Mathf.Pow(turn, b)+Mathf.Pow(2f,turn)*Mathf.Pow(turn/c,d);
        // transiton blue to orange gate at turn 7
        res = res * Mathf.Min((0.2f/3.5f/3.5f*(turn-3f)*(turn-10f)+1f),1);
        Debug.Log("res="+res);
        int intres = (int)res;
        //intres = 0;
        return intres;
    }
}
