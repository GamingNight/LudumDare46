using UnityEngine;

public class Resources {

    public enum ResourcesType {
        A, B, C, D
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

    public bool Buy(ResourceCollection resource2Buy) {
        bool resA = A.CanBuy(resource2Buy.A.count);
        bool resB = B.CanBuy(resource2Buy.B.count);
        bool resC = C.CanBuy(resource2Buy.C.count);
        bool resD = D.CanBuy(resource2Buy.D.count);
        bool res = (resA & resB & resC & resD);
        if (res) {
            A.Buy(resource2Buy.A.count);
            B.Buy(resource2Buy.B.count);
            C.Buy(resource2Buy.C.count);
            D.Buy(resource2Buy.D.count);
        }
        return res;
    }
}
