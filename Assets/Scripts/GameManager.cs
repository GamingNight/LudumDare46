using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    public enum RoudState {
        PREPARATION, ATTACK
    }

    private Resources resourcesA = new Resources(0, Resources.ResourcesType.A);
    private Resources resourcesB = new Resources(0, Resources.ResourcesType.B);
    private Resources resourcesC = new Resources(0, Resources.ResourcesType.C);
    private Resources resourcesD = new Resources(0, Resources.ResourcesType.D);
    
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
    }

    public void SwitchState() {
    	if (roudState == RoudState.PREPARATION) {
    		roudState = RoudState.ATTACK;
    		return;
    	}
    	roudState = RoudState.PREPARATION;
    	roundCount += 1;
    }

    public Resources GetResourcesA() {
    	return resourcesA;
    }

    public Resources GetResourcesB() {
    	return resourcesB;
    }

    public Resources GetResourcesC() {
    	return resourcesC;
    }

    public Resources GetResourcesD() {
    	return resourcesD;
    }
}
