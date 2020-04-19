using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager INSTANCE;

    public static GameManager GetInstance() {

        return INSTANCE;
    }

    private ResourceCollection resourcesConf = new ResourceCollection();
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
        resourcesConf.B.Set(10, Resources.ResourcesType.B);
        resourcesConf.C.Set(10, Resources.ResourcesType.C);
        resourcesConf.D.Set(1, Resources.ResourcesType.D);
    }

    public bool Buy(Resources.ResourcesType type) {
        ResourceCollectionCost collec = new ResourceCollectionCost(type);
        bool toto = resourcesConf.Buy(collec);
        DebugDisplay();
        return toto;
    }

    public void Add(Resources.ResourcesType type) {
        ResourceCollectionReward collec = new ResourceCollectionReward(type);
        resourcesConf.Add(collec);
    }

    public void LaunchAttack() {
        Debug.Log("attack");
        if (attackersD.GetPower(roundCount) > resourcesConf.D.count) {
            Debug.Log("perdu au tour " + roundCount);
            menuCanvas.SetActive(true);
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
        DebugDisplay();
        Debug.Log("tour " + roundCount);
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


    private void DebugDisplay() {
        Debug.Log("A = " + resourcesConf.A.count + " B = " + resourcesConf.B.count + " C = " + resourcesConf.C.count + " D = " + resourcesConf.D.count + " Attack = " + attackersD.GetPower(roundCount));
    }
}
