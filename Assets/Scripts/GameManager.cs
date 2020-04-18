using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    public enum RoudState {
        PREPARATION, ATTACK
    }

    private Resources[] resources_list = new Resources[4];
    private Resources ResourcesA = new Resources(0, Resources.ResourcesType.A);
    private Resources ResourcesB = new Resources(0, Resources.ResourcesType.B);
    private Resources ResourcesC = new Resources(0, Resources.ResourcesType.C);
    private Resources ResourcesD = new Resources(0, Resources.ResourcesType.D);
    private Attackers attackersD = new Attackers(Resources.ResourcesType.D);
    
    public int roundCount;
    public RoudState roudState;

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
            LaunchAtack();
        }
        else {
            roudState = RoudState.PREPARATION;
            roundCount += 1;
            LaunchPreparation();
        }
    }

    public void LaunchAtack()
    {
        int power = attackersD.GetPower(roundCount);

        bool res = ResourcesD.Use(power);
        if (!res) {
            // TODO
            Debug.Log("END OF THE GAME");
        }
    }

    public void LaunchPreparation()
    {
    	// TODO update Resources
    }

    public void FakeSenarioTV()
    {
		ResourcesD.count = 10;
		while (true) {
			SwitchState();
            int res = ResourcesD.count;
            if (roundCount > 100) {
                return;
            }
		}
    }
}
