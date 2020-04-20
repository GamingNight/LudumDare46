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

    public MenuNavig menuNavig;
    public GameObject menuCanvas;

    void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
    }

    void Start() {
        roundCount = 0;
        resourcesConf.A.Set(3, Resources.ResourcesType.A);
        resourcesConf.B.Set(0, Resources.ResourcesType.B);
        resourcesConf.C.Set(0, Resources.ResourcesType.C);
        resourcesConf.D.Set(1, Resources.ResourcesType.D);
        UpdateSimulation();
    }

    public void Reset() {
        Start();
        ResetSimulation();
    }

    void ResetSimulation() {
        resourcesSimu.A.Set(0, Resources.ResourcesType.A);
        resourcesSimu.B.Set(0, Resources.ResourcesType.B);
        resourcesSimu.C.Set(0, Resources.ResourcesType.C);
        resourcesSimu.D.Set(0, Resources.ResourcesType.D);
    }

    public bool Buy(Resources.ResourcesType type) {
        ResourceCollectionCost collec = new ResourceCollectionCost(type);
        bool toto = resourcesConf.Buy(collec);
        DebugDisplay();
        return toto;
    }

    public void BuyDef() {
        if (GameManager.GetInstance().Buy(Resources.ResourcesType.D)) {
            GameManager.GetInstance().Add(Resources.ResourcesType.D);
        }
    }

    public bool CanGenerate() {
        ResourceCollectionHeartCost collec = new ResourceCollectionHeartCost();
        return resourcesConf.CanBuy(collec);
    }

    public bool Generate() {
        ResourceCollectionHeartCost collec = new ResourceCollectionHeartCost();
        return resourcesConf.Buy(collec);
    }

    public bool BuyBoost() {
        ResourceCollectionBoostCost collec = new ResourceCollectionBoostCost();
        return resourcesConf.Buy(collec);
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

    public void LaunchAttack() {
        Debug.Log("attack");
        if (attackersD.GetPower(roundCount) > resourcesConf.D.count) {
            Debug.Log("perdu au tour " + roundCount);
            menuCanvas.SetActive(true);
            menuNavig.EndMenu();
        }
    }

    public void LaunchPreparation() {
        roundCount += 1;
    }

    public void LaunchReward() {
        resourcesConf.Add(resourcesSimu);
    }

    public void UpdateSimulation() {
        ResetSimulation();
        foreach (Organ org in organContainer.GetComponentsInChildren<Organ>()) {
            org.OnSimulateReward();
        }
    }

    public void GoToNextTurn() {
        LaunchAttack();
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

    public Resources GetResources(Resources.ResourcesType type) {
        return resourcesConf.GetResources(type);
    }

    public Resources GetSimulation(Resources.ResourcesType type) {
        return resourcesSimu.GetResources(type);
    }


    private void DebugDisplay() {
        Debug.Log("A = " + resourcesConf.A.count + " B = " + resourcesConf.B.count + " C = " + resourcesConf.C.count + " D = " + resourcesConf.D.count + " Attack = " + attackersD.GetPower(roundCount));
    }
}
