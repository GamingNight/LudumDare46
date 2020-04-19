﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganSettlementManager : MonoBehaviour {

    public enum Mode {
        IDLE, MENU, SETTLEMENT, DISABLED
    }

    public GameObject[] organPrefabs;
    public GameObject iconPrefab;
    public GameObject canvas;
    public GameObject organContainer;

    private bool[] unlockedOrganTable;
    private Mode mode;
    private Mode previousMode;
    private Dictionary<OrganSettlementIcon, Organ> iconMap;
    private Coroutine growIconCoroutine;
    private List<GameObject> organObjectList;

    void Start() {
        unlockedOrganTable = new bool[organPrefabs.Length];
        for (int i = 0; i < organPrefabs.Length; i++) {
            unlockedOrganTable[i] = organPrefabs[i].GetComponent<Organ>().unlockedAtStart;
        }
        mode = Mode.IDLE;
        iconMap = new Dictionary<OrganSettlementIcon, Organ>();
        organObjectList = new List<GameObject>();
    }

    public void UnlockOrgan(string organName) {
        bool found = false;
        int i = 0;
        while (!found && i < organPrefabs.Length) {
            if (organPrefabs[i].GetComponent<Organ>().organName == organName) {
                unlockedOrganTable[i] = true;
                found = true;
            }
            i++;
        }
    }

    void Update() {

        bool mouseButtonUp = Input.GetMouseButtonUp(0);
        if (mouseButtonUp) {
            Vector3 mouseWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                mouseWorldPosition = hit.point;
            }
            if (growIconCoroutine != null) {
                StopCoroutine(growIconCoroutine);
            }
            if (mode == Mode.IDLE) {
                ShowOrganSettlementIcons(mouseWorldPosition);
                mode = Mode.MENU;
            } else if (mode == Mode.MENU) {
                OrganSettlementIcon selectedIcon = GetSelectedIcon();
                if (selectedIcon != null) {
                    organObjectList.Add(InstantiateOrgan(new Vector3(mouseWorldPosition.x, 0.1f, mouseWorldPosition.z), iconMap[selectedIcon]));
                    mode = Mode.SETTLEMENT;
                } else {
                    mode = Mode.IDLE;
                }
                List<OrganSettlementIcon> iconList = new List<OrganSettlementIcon>(iconMap.Keys);
                for (int i = 0; i < iconList.Count; i++) {
                    Destroy(iconList[i].gameObject);
                }
                iconMap.Clear();
            } else if (mode == Mode.SETTLEMENT) {
                if (!organObjectList[organObjectList.Count - 1].GetComponent<Organ>().CollideWithOtherOrgan) {
                    SetSpriteSortingLayerName(organObjectList[organObjectList.Count - 1], "Default");
                    mode = Mode.IDLE;
                }
            }
        }

        if (mode == Mode.SETTLEMENT) {
            Vector3 mouseWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                mouseWorldPosition = hit.point;
            }
            GameObject lastOrganInstantiated = organObjectList[organObjectList.Count - 1];
            lastOrganInstantiated.transform.position = new Vector3(mouseWorldPosition.x, 0.1f, mouseWorldPosition.z);
            lastOrganInstantiated.transform.eulerAngles = new Vector3(90, 0, 0);
            if (lastOrganInstantiated.GetComponent<Organ>().CollideWithOtherOrgan) {
                lastOrganInstantiated.GetComponent<Organ>().SetToForbiddenColor();
            } else {
                lastOrganInstantiated.GetComponent<Organ>().RevertColor();
            }
        }
    }

    private GameObject[] GetUnlockedOrganPrefabs() {

        List<GameObject> unlockedOrganList = new List<GameObject>();
        for (int i = 0; i < unlockedOrganTable.Length; i++) {
            if (unlockedOrganTable[i]) {
                unlockedOrganList.Add(organPrefabs[i]);
            }
        }
        return unlockedOrganList.ToArray();
    }

    private void ShowOrganSettlementIcons(Vector3 mouseWorldPosition) {
        GameObject[] unlockedOrganPrefabs = GetUnlockedOrganPrefabs();
        iconMap.Clear();
        for (int i = 0; i < unlockedOrganPrefabs.Length; i++) {
            Organ organ = unlockedOrganPrefabs[i].GetComponent<Organ>();
            float angle = i * (2 * Mathf.PI / unlockedOrganPrefabs.Length);
            GameObject icon = Instantiate<GameObject>(iconPrefab);
            icon.transform.SetParent(canvas.transform, false);
            icon.GetComponent<Image>().sprite = organ.hudImage;
            icon.GetComponent<OrganSettlementIcon>().deselected = organ.hudImage;
            icon.GetComponent<OrganSettlementIcon>().selected = organ.hudImageSelected;
            RectTransform rectTransform = (RectTransform)icon.transform;
            rectTransform.position = new Vector3(mouseWorldPosition.x, 0.1f, mouseWorldPosition.z);
            rectTransform.eulerAngles = new Vector3(90f, 0, 0);
            rectTransform.localScale = new Vector2(0, 0);
            growIconCoroutine = StartCoroutine(GrowIconCoroutine(rectTransform, mouseWorldPosition, angle));
            iconMap.Add(icon.GetComponent<OrganSettlementIcon>(), organ);
        }
    }

    private IEnumerator GrowIconCoroutine(RectTransform iconTransform, Vector3 mouseWorldPosition, float angle) {

        float initX = mouseWorldPosition.x;
        float initZ = mouseWorldPosition.z;
        float targetX = mouseWorldPosition.x + Mathf.Cos(angle);
        float targetZ = mouseWorldPosition.z + Mathf.Sin(angle);
        float step = 0.05f;
        float totalTime = 0.25f;
        float currentTime = 0;
        while (currentTime < totalTime) {
            float xPos = Mathf.Lerp(initX, targetX, currentTime / totalTime);
            float zPos = Mathf.Lerp(initZ, targetZ, currentTime / totalTime);
            iconTransform.position = new Vector3(xPos, 0.1f, zPos);
            float scaleLerp = Mathf.Lerp(0, 1, currentTime / totalTime);
            iconTransform.localScale = new Vector2(scaleLerp, scaleLerp);
            yield return new WaitForSeconds(step);
            currentTime += step;
        }
    }


    private GameObject InstantiateOrgan(Vector3 position, Organ organPrefab) {
        GameObject organ = Instantiate<GameObject>(organPrefab.gameObject);
        organ.transform.SetParent(organContainer.transform);
        organ.transform.position = position;
        SetSpriteSortingLayerName(organ, "OrganToBeSettled");
        return organ;
    }

    private void SetSpriteSortingLayerName(GameObject organ, string name) {

        if (organ.GetComponent<SpriteRenderer>() != null) {
            organ.GetComponent<SpriteRenderer>().sortingLayerName = name;
        } else {
            foreach (SpriteRenderer renderer in organ.GetComponentsInChildren<SpriteRenderer>()) {
                renderer.sortingLayerName = name;
            }
        }
    }

    private OrganSettlementIcon GetSelectedIcon() {

        OrganSettlementIcon selected = null;
        bool found = false;
        int i = 0;
        List<OrganSettlementIcon> icons = new List<OrganSettlementIcon>(iconMap.Keys);
        while (!found && i < icons.Count) {
            if (icons[i].GetComponent<OrganSettlementIcon>().IsSelected) {
                selected = icons[i];
                found = true;
            }
            i++;
        }
        return selected;
    }

    public void DisableMouse() {
        previousMode = mode;
        mode = Mode.DISABLED;
    }

    public void EnableMouse() {
        mode = previousMode;
    }
}
