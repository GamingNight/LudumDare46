using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    private Resources[] resources_list = new Resources[4];
    private Resources ResourcesA = new Resources(0, Resources.ResourcesType.A);
    private Resources ResourcesB = new Resources(0, Resources.ResourcesType.B);
    private Resources ResourcesC = new Resources(0, Resources.ResourcesType.C);
    private Resources ResourcesD = new Resources(100, Resources.ResourcesType.D);
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
    }

    public void LaunchAttack() {
        int dataSave = ResourcesD.count;
        int power = attackersD.GetPower(roundCount);

        bool res = ResourcesD.Use(power);
        Debug.Log("A = " + ResourcesA.count + " B = " + ResourcesB.count +" C = " + ResourcesC.count +" D = " + dataSave + " - " + power + " = " + ResourcesD.count);

        if (!res) {
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
        Resources resources = ResourcesD;
        if (type == Resources.ResourcesType.A) {
            resources = ResourcesA;
        }
        else if (type == Resources.ResourcesType.B) {
            resources = ResourcesB;
        }
        else if (type == Resources.ResourcesType.C) {
            resources = ResourcesC;
        }
        return resources;
    }
}
