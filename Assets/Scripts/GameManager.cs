using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    private ResourceConf resourcesConf = new ResourceConf();
    private Attackers attackersD = new Attackers(Resources.ResourcesType.D);
    public int roundCount;
    public GameObject organContainer;

    public MenuNavig menuNavig;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {
        roundCount = 0;
        resourcesConf.A.Set(10, Resources.ResourcesType.A);
        resourcesConf.B.Set(10, Resources.ResourcesType.B);
        resourcesConf.C.Set(0, Resources.ResourcesType.C);
        resourcesConf.D.Set(100, Resources.ResourcesType.D);
    }

    public void LaunchAttack() {
        int power = attackersD.GetPower(roundCount);
        Debug.Log("A = " + resourcesConf.A.count + " B = " + resourcesConf.B.count +" C = " + resourcesConf.C.count +" D = " + resourcesConf.D.count + " Attack = " + power);

        if (power > resourcesConf.D.count) {
            menuNavig.endMenu();
        }
    }

    public void LaunchPreparation() {
        roundCount += 1;
    }

    public void LaunchReward() {
        foreach (Organ org in organContainer.GetComponentsInChildren<Organ>()) {
            org.OnReward();
        }
    }

    public void GoToNextTurn() {
        LaunchAttack();
        LaunchReward();
        LaunchPreparation();
    }

    public Resources GetResources(Resources.ResourcesType type) {
        Resources resources = resourcesConf.D;
        if (type == Resources.ResourcesType.A) {
            resources = resourcesConf.A;
        }
        else if (type == Resources.ResourcesType.B) {
            resources = resourcesConf.B;
        }
        else if (type == Resources.ResourcesType.C) {
            resources = resourcesConf.C;
        }
        return resources;
    }
}
