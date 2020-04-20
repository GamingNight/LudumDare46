using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour {

    private class LineRelation {
        public GameObject organ1;
        public GameObject organ2;
        public LineRenderer lineRenderer;

        private int buildTurn = 0;

        public LineRelation(GameObject organ1, GameObject organ2, LineRenderer lineRenderer) {
            this.organ1 = organ1;
            this.organ2 = organ2;
            this.lineRenderer = lineRenderer;
            this.buildTurn = GameManager.GetInstance().roundCount;
        }

        public bool TestEqual(GameObject otherOrgan1, GameObject otherOrgan2) {
            return ((organ1 == otherOrgan1 && organ2 == otherOrgan2) || (organ1 == otherOrgan2 && organ2 == otherOrgan1));
        }

        public static LineRelation FindRelation(GameObject obj1, GameObject obj2) {

            LineRelation res = null;
            bool found = false;
            int i = 0;
            while (!found && i < lineRelationList.Count) {
                if (lineRelationList[i].TestEqual(obj1, obj2)) {
                    res = lineRelationList[i];
                    found = true;
                }
                i++;
            }
            return res;
        }

        void Start() {
            buildTurn = GameManager.GetInstance().roundCount;
        }

        public int GetBuildTurn() {
            return buildTurn;
        }
    }

    public GameObject lineRendrerPrefab;

    private static List<LineRelation> lineRelationList;

    void Start() {

        if (lineRelationList == null) {
            lineRelationList = new List<LineRelation>();
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "LineDrawer") {
            if (LineRelation.FindRelation(gameObject, other.gameObject) == null) {
                GameObject newLineRendererObject = Instantiate<GameObject>(lineRendrerPrefab);
                newLineRendererObject.transform.SetParent(gameObject.transform);
                newLineRendererObject.transform.position = gameObject.transform.position;
                LineRenderer newLineRenderer = newLineRendererObject.GetComponent<LineRenderer>();
                newLineRenderer.SetPosition(0, transform.position);
                newLineRenderer.SetPosition(1, other.gameObject.transform.position);
                lineRelationList.Add(new LineRelation(gameObject, other.gameObject, newLineRenderer));
            }
        }
    }

    void OnTriggerStay(Collider other) {

        if (other.gameObject.tag == "LineDrawer") {
            LineRelation lineRelation = LineRelation.FindRelation(gameObject, other.gameObject);
            if (lineRelation != null) {
                lineRelation.lineRenderer.SetPosition(0, transform.position);
                lineRelation.lineRenderer.SetPosition(1, other.gameObject.transform.position);
            }
        }
    }

    void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "LineDrawer") {
            LineRelation lineRelation = LineRelation.FindRelation(gameObject, other.gameObject);
            if (lineRelation != null) {
                LineRenderer lineRenderer = lineRelation.lineRenderer;
                lineRelationList.Remove(lineRelation);
                Destroy(lineRenderer.gameObject);
            }
        }
    }

    public static void ClearRelations() {
        if (lineRelationList == null) {
            return;
        }

        foreach (LineRelation relation in lineRelationList) {
            Destroy(relation.lineRenderer);
        }
        lineRelationList.Clear();
    }

    public static void ClearOrganRelations(int turnNumber) {
        if (lineRelationList == null) {
            return;
        }

        foreach (LineRelation relation in lineRelationList) {
            if (relation.GetBuildTurn() == turnNumber)
            {
                Destroy(relation.lineRenderer);
            }
        }
    }
}
