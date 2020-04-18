using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    public enum RoudState {
        PREPARATION, ATTACK, REWARD
    }

    private Resources[] resources_list = new Resources[4];
    private Resources ResourcesA = new Resources(0, Resources.ResourcesType.A);
    private Resources ResourcesB = new Resources(0, Resources.ResourcesType.B);
    private Resources ResourcesC = new Resources(0, Resources.ResourcesType.C);
    private Resources ResourcesD = new Resources(0, Resources.ResourcesType.D);
    private Attackers attackersD = new Attackers(Resources.ResourcesType.D);
    
    public int roundCount;
    public RoudState roudState;
    public GameObject organContainer;

    public MenuNavig MenuNavig;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {
        roundCount = 0;
        roudState = RoudState.PREPARATION;
        // FakeSenarioTV();
    }

    public void SwitchState() {
        if (roudState == RoudState.PREPARATION) {
            roudState = RoudState.ATTACK;
            LaunchAttack();
        }
        else if (roudState == RoudState.ATTACK) {
            roudState = RoudState.REWARD;
            roundCount += 1;
            LaunchPreparation();
        }
        else {
            roudState = RoudState.ATTACK;
            LaunchReward();
        }

        foreach (Organ org in organContainer.GetComponentsInChildren<Organ>()) {

            org.OnSwitchState();
        }
    }

    public void LaunchAttack()
    {
        int dataSave = ResourcesD.count;
        int power = attackersD.GetPower(roundCount);

        bool res = ResourcesD.Use(power);
        Debug.Log( dataSave + " - " + power + " = " + ResourcesD.count);
        if (!res) {
            // TODO
            MenuNavig.endMenu();
        }
    }

    public void LaunchPreparation()
    {

    }

    public void LaunchReward()
    {

    }

    public void FakeSenarioTV()
    {
		ResourcesD.count = 10;
		while (true) {
			SwitchState();
            if ((roudState == RoudState.PREPARATION))
                ResourcesD.Add(1);
            if (roundCount > 100) {
                Debug.Log("END OF LOOP");
                return;
            }
		}
    }
}
