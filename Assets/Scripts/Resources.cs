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
