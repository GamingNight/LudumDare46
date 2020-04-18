using UnityEngine;

public class Attackers {

    public Resources.ResourcesType target = Resources.ResourcesType.D;

    public Attackers(Resources.ResourcesType attackersTarget) {
        target = attackersTarget;
    }

    public int GetPower(int roundCount) {
        float result = (float) roundCount; // f(x) = x
        float rand_range = 0.1f * result;
        int min = (int) (result - rand_range);
        int max = (int) (result + rand_range);
        
        //Random aleatoire = new Random();
        //int nombreAleatoire = min + (int)(Math.random() * ((max - min) + 1));
        //Debug.Log("nombreAleatoire");
        //Debug.Log(nombreAleatoire);

        int res = (int) Random.Range(min, max);
        Debug.Log(res);


        return res;
    }
}
