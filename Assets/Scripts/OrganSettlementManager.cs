﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganSettlementManager : MonoBehaviour {

    public enum Mode {
        IDLE, MENU
    }

    public GameObject[] organPrefabs;
    public GameObject iconPrefab;
    public GameObject canvas;

    private bool[] unlockedOrganTable;
    private Mode mode;
    private Dictionary<OrganSettlementIcon, Organ> iconMap;

    void Start() {
        unlockedOrganTable = new bool[organPrefabs.Length];
        for (int i = 0; i < organPrefabs.Length; i++) {
            unlockedOrganTable[i] = organPrefabs[i].GetComponent<Organ>().unlockedAtStart;
        }
        mode = Mode.IDLE;
        iconMap = new Dictionary<OrganSettlementIcon, Organ>();
    }

    void Update() {

        bool mouseButtonUp = Input.GetMouseButtonUp(0);
        if (mouseButtonUp) {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            if (mode == Mode.IDLE) {
                ShowOrganSettlementIcons(mouseWorldPosition);
                mode = Mode.MENU;
            } else if (mode == Mode.MENU) {
                OrganSettlementIcon selectedIcon = GetSelectedIcon();
                if (selectedIcon != null) {
                    InstantiateOrgan(new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0), iconMap[selectedIcon]);
                }
                List<OrganSettlementIcon> iconList = new List<OrganSettlementIcon>(iconMap.Keys);
                for (int i = 0; i < iconList.Count; i++) {
                    Destroy(iconList[i].gameObject);
                }
                iconMap.Clear();
                mode = Mode.IDLE;
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
            rectTransform.position = new Vector3(mouseWorldPosition.x + Mathf.Cos(angle), mouseWorldPosition.y + Mathf.Sin(angle), 0);
            iconMap.Add(icon.GetComponent<OrganSettlementIcon>(), organ);
        }
    }


    public void InstantiateOrgan(Vector3 position, Organ organPrefab) {
        GameObject organ = Instantiate<GameObject>(organPrefab.gameObject);
        organ.transform.position = position;
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
}
