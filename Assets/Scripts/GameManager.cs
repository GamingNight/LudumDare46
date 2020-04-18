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
    private Resources ResourcesD = new Resources(0, Resources.ResourcesType.D);
    private Attackers attackersD = new Attackers(Resources.ResourcesType.D);

    public int roundCount;
    public GameObject organContainer;

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
        Debug.Log(dataSave + " - " + power + " = " + ResourcesD.count);
        if (!res) {
            // TODO
            Debug.Log("END OF THE GAME");
        }
    }

    public void LaunchPreparation() {

    }

    public void LaunchReward() {

    }

    public void GoToNextTurn() {
        LaunchAttack();
        LaunchReward();
        foreach (Organ org in organContainer.GetComponentsInChildren<Organ>()) {

            org.OnReward();
        }
        roundCount += 1;
        LaunchPreparation();
    }
}
