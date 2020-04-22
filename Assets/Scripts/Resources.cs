using UnityEngine;

public class Resources {

    public enum ResourcesType {
        A, B, C, D, MASTERNODE
    }

    public ResourcesType type = ResourcesType.A;
    public int count = 0;

    public Resources(int countValue, ResourcesType typeValue) {
        count = countValue;
        type = typeValue;
    }

    public void Set(int countValue, ResourcesType typeValue) {
        count = countValue;
        type = typeValue;
    }

    public void Add(int value) {
        count = count + value;
    }

    public bool CanBuy(int value) {
        return (count >= value);
    }

    public bool Buy(int value) {
        bool res = CanBuy(value);
        if (res) {
            count = count - value;
        }
        return res;
    }
}

public class ResourceCollection {
    public Resources A = new Resources(3, Resources.ResourcesType.A);
    public Resources B = new Resources(3, Resources.ResourcesType.B);
    public Resources C = new Resources(3, Resources.ResourcesType.C);
    public Resources D = new Resources(3, Resources.ResourcesType.D);

    public bool CanBuy(ResourceCollection resource2Buy) {
        bool resA = A.CanBuy(resource2Buy.A.count);
        bool resB = B.CanBuy(resource2Buy.B.count);
        bool resC = C.CanBuy(resource2Buy.C.count);
        bool resD = D.CanBuy(resource2Buy.D.count);
        return (resA & resB & resC & resD);
    }

    public bool Buy(ResourceCollection resource2Buy) {
        bool res = CanBuy(resource2Buy);
        if (res) {
            A.Buy(resource2Buy.A.count);
            B.Buy(resource2Buy.B.count);
            C.Buy(resource2Buy.C.count);
            D.Buy(resource2Buy.D.count);
        }
        return res;
    }

    public void Add(ResourceCollection resource2Add) {
        A.Add(resource2Add.A.count);
        B.Add(resource2Add.B.count);
        C.Add(resource2Add.C.count);
        D.Add(resource2Add.D.count);
    }

    public Resources GetResources(Resources.ResourcesType type) {
        Resources resources = D;
        if (type == Resources.ResourcesType.A) {
            resources = A;
        }
        else if (type == Resources.ResourcesType.B) {
            resources = B;
        }
        else if (type == Resources.ResourcesType.C) {
            resources = C;
        }
        return resources;
    }
}

public class ResourceCollectionReward : ResourceCollection {
    public ResourceCollectionReward(Resources.ResourcesType type) {

        if (type == Resources.ResourcesType.A) {
            A.count = 2;
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }

        if (type == Resources.ResourcesType.B) {
            A.count = 0;
            B.count = 2;
            C.count = 0;
            D.count = 0;
        }
    
        if (type == Resources.ResourcesType.C) {
            A.count = 0;
            B.count = 0;
            C.count = 1;
            D.count = 0;
        }

        if (type == Resources.ResourcesType.D) {
            A.count = 0;
            B.count = 0;
            C.count = 0;
            D.count = 1;
        }

        if (type == Resources.ResourcesType.MASTERNODE) {
            A.count = 14;
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }
    }
}

public class ResourceCollectionCost : ResourceCollection {
    public ResourceCollectionCost(Resources.ResourcesType type) {

        if (type == Resources.ResourcesType.A) {
            A.count = 3;
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }

        if (type == Resources.ResourcesType.B) {
            A.count = 5;
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }
    
        if (type == Resources.ResourcesType.C) {
            A.count = 10;
            B.count = 5;
            C.count = 0;
            D.count = 0;
        }

        if (type == Resources.ResourcesType.D) {
            int currentD = GameManager.GetInstance().GetResources(Resources.ResourcesType.D).count;
            A.count = (int) (Mathf.Pow(10, ((currentD+1)/4.5f)) * (Mathf.Pow(10, (1/4.5f)) - 1));
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }

        if (type == Resources.ResourcesType.MASTERNODE) {
            A.count = 21;
            B.count = 0;
            C.count = 0;
            D.count = 0;
        }
    }
}

public class ResourceCollectionBoostCost : ResourceCollection {
    public ResourceCollectionBoostCost() {
        A.count = 0;
        B.count = 1;
        C.count = 0;
        D.count = 0;
    }
}

public class ResourceCollectionHeartCost : ResourceCollection {
    public ResourceCollectionHeartCost() {
        A.count = 0;
        B.count = 12;
        C.count = 4;
        D.count = 0;
    }
}