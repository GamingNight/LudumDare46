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
        count += value;
    }

    public bool Use(int value) {
        if (value > count) {
            return false;
        }
        count -= value;
        return true;
    }
}

public class ResourceConf {
    public Resources A = new Resources(0, Resources.ResourcesType.A);
    public Resources B = new Resources(0, Resources.ResourcesType.B);
    public Resources C = new Resources(0, Resources.ResourcesType.C);
    public Resources D = new Resources(0, Resources.ResourcesType.D);
    }
