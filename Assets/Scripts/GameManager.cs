using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    private ResourceCollection resourcesConf = new ResourceCollection();
    private ResourceCollection resourcesSimu = new ResourceCollection();
    private Attackers attackersD = new Attackers(Resources.ResourcesType.D);
    public int roundCount;
    public GameObject organContainer;
    private int defOnTurn = 0;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {
        defOnTurn = 0;
        roundCount = 0;
        resourcesConf.A.Set(3, Resources.ResourcesType.A);
        resourcesConf.B.Set(0, Resources.ResourcesType.B);
        resourcesConf.C.Set(0, Resources.ResourcesType.C);
        resourcesConf.D.Set(0, Resources.ResourcesType.D);
        UpdateSimulation();
    }

    public void Reset() {
        Start();
        ResetSimulation();
    }

    private void ResetTurnFunction() {
        ResetSimulation();
        OrganSettlementManager MyMan = gameObject.GetComponent<OrganSettlementManager>();
        List<GameObject> Obj2Unlist = new List<GameObject>();
        foreach (GameObject orgObj in MyMan.GetInstantiatedOrgans()) {
            Organ org = orgObj.GetComponent<Organ>();
            org.OnResetTurn();
            if (org.GetBuildTurn() == roundCount) {
                Refund(org.resourcesType);
                LineDrawer.ClearOrganRelations(org.GetBuildTurn());
                Obj2Unlist.Add(orgObj);
                Destroy(orgObj);
            } else {
                org.OnSimulateReward();
            }
        }
        foreach (GameObject orgObj in Obj2Unlist) {
            MyMan.GetInstantiatedOrgans().Remove(orgObj);
        }

    }

    public void ResetTurn() {
        ResetTurnFunction();
        ResetTurnFunction();
        for (int i = 0; i < defOnTurn; i++) {
            RefundDef();
        }
        defOnTurn = 0;
    }

    void ResetSimulation() {
        resourcesSimu.A.Set(0, Resources.ResourcesType.A);
        resourcesSimu.B.Set(0, Resources.ResourcesType.B);
        resourcesSimu.C.Set(0, Resources.ResourcesType.C);
        resourcesSimu.D.Set(0, Resources.ResourcesType.D);
    }

    public bool CanBuy(Resources.ResourcesType type) {
        ResourceCollectionCost collec = new ResourceCollectionCost(type);
        bool res = resourcesConf.CanBuy(collec);
        return res;
    }

    public bool Buy(Resources.ResourcesType type) {
        ResourceCollectionCost collec = new ResourceCollectionCost(type);
        bool res = resourcesConf.Buy(collec);
        DebugDisplay();
        return res;
    }

    public bool CanBuyDef() {
        return CanBuy(Resources.ResourcesType.D);
    }

    public void BuyDef() {

        if (GameScenario.IS_TUTORIAL) {
            GameScenario.GetInstance().ReachState(GameScenario.StateName.COST_BLUE);
        } else {
            if (Buy(Resources.ResourcesType.D)) {
                Add(Resources.ResourcesType.D);
                defOnTurn += 1;
            }
        }
    }

    public int GetDefCost() {
        ResourceCollectionCost collec = new ResourceCollectionCost(Resources.ResourcesType.D);
        return collec.A.count;
    }

    public bool CanGenerate() {
        ResourceCollectionHeartCost collec = new ResourceCollectionHeartCost();
        return resourcesConf.CanBuy(collec);
    }

    public void Generate() {
        ResourceCollectionHeartCost collec = new ResourceCollectionHeartCost();
        bool res = resourcesConf.Buy(collec);
        if (res) {
            Debug.Log("TODO END OF GAME");
        }
    }

    public void RefundDef() {
        resourcesConf.D.count = resourcesConf.D.count - 1;
        ResourceCollectionCost collec = new ResourceCollectionCost(Resources.ResourcesType.D);
        resourcesConf.Add(collec);
    }

    public bool BuyBoost() {
        ResourceCollectionBoostCost collec = new ResourceCollectionBoostCost();
        bool res = resourcesConf.Buy(collec);
        return res;
    }

    public void Refund(Resources.ResourcesType type) {
        ResourceCollectionCost collec = new ResourceCollectionCost(type);
        resourcesConf.Add(collec);
    }

    public void RefundBoost() {
        ResourceCollectionBoostCost collec = new ResourceCollectionBoostCost();
        resourcesConf.Add(collec);
    }

    public void Add(Resources.ResourcesType type) {
        ResourceCollectionReward collec = new ResourceCollectionReward(type);
        resourcesConf.Add(collec);
    }

    public void SimulationAdd(Resources.ResourcesType type) {
        ResourceCollectionReward collec = new ResourceCollectionReward(type);
        resourcesSimu.Add(collec);
    }

    public int GetTestLevel() {
        return attackersD.GetPower(roundCount);
    }

    public bool LaunchAttack() {
        Debug.Log("attack");
        if (attackersD.GetPower(roundCount) > resourcesConf.D.count) {
            Debug.Log("perdu au tour " + roundCount);
            return false;
        }
        return true;
    }

    public void LaunchPreparation() {
        roundCount += 1;
    }

    public void LaunchReward() {
        resourcesConf.Add(resourcesSimu);
    }

    public void UpdateSimulation() {
        ResetSimulation();
        OrganSettlementManager MyMan = gameObject.GetComponent<OrganSettlementManager>();
        foreach (GameObject orgObj in MyMan.GetInstantiatedOrgans()) {
            Organ org = orgObj.GetComponent<Organ>();
            org.OnSimulateReward();
        }
    }

    public bool GoToNextTurn() {
        bool res = LaunchAttack();
        if (res) {
            LaunchReward();
            LaunchPreparation();
            foreach (Organ org in organContainer.GetComponentsInChildren<Organ>()) {
                org.OnGoToNextTurn();
            }
            foreach (MainOrgan mOrg in organContainer.GetComponentsInChildren<MainOrgan>()) {
                mOrg.OnGoToNextTurn();
            }
            UpdateSimulation();
            DebugDisplay();
            Debug.Log("tour " + roundCount);
        }
        defOnTurn = 0;
        return res;
    }

    public Resources GetResources(Resources.ResourcesType type) {
        return resourcesConf.GetResources(type);
    }

    public Resources GetSimulation(Resources.ResourcesType type) {
        return resourcesSimu.GetResources(type);
    }

    private void DebugDisplay() {
        Debug.Log("A = " + resourcesConf.A.count + " B = " + resourcesConf.B.count + " C = " + resourcesConf.C.count + " D = " + resourcesConf.D.count + " Attack = " + attackersD.GetPower(roundCount));
    }

    void Update() {

        if (Input.GetMouseButtonDown(1)) {
            ResetTurn();
        }
    }
}
