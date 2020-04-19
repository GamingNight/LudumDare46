using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public enum ScreenPart {

        UP, UPRIGHT, RIGHT, DOWNRIGHT, DOWN, DOWNLEFT, LEFT, UPLEFT, NONE
    }

    public float speed = 10;
    public float movementDectectionLimit = 0.6f;
    private ScreenPart currentScreenPart;

    void Start() {
        currentScreenPart = ScreenPart.NONE;
    }

    void Update() {

        bool move = true;
        float horizontalEn = Input.GetAxisRaw("Horizontal");
        float horizontalFr = Input.GetAxisRaw("HorizontalFR");
        float verticalEn = Input.GetAxisRaw("Vertical");
        float verticalFr = Input.GetAxisRaw("VerticalFR");
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) {
            Vector3 mousePosition = Input.mousePosition;
            float mouseXPercent = mousePosition.x / Screen.width;
            float mouseYPercent = mousePosition.y / Screen.height;
            if (mouseYPercent > movementDectectionLimit) {
                if (mouseXPercent < (1 - movementDectectionLimit)) {
                    currentScreenPart = ScreenPart.UPLEFT;
                } else if (mouseXPercent > movementDectectionLimit) {
                    currentScreenPart = ScreenPart.UPRIGHT;
                } else {
                    currentScreenPart = ScreenPart.UP;
                }
            } else if (mouseYPercent < (1 - movementDectectionLimit)) {
                if (mouseXPercent < (1 - movementDectectionLimit)) {
                    currentScreenPart = ScreenPart.DOWNLEFT;
                } else if (mouseXPercent > movementDectectionLimit) {
                    currentScreenPart = ScreenPart.DOWNRIGHT;
                } else {
                    currentScreenPart = ScreenPart.DOWN;
                }
            } else {
                if (mouseXPercent < (1 - movementDectectionLimit)) {
                    currentScreenPart = ScreenPart.LEFT;
                } else if (mouseXPercent > movementDectectionLimit) {
                    currentScreenPart = ScreenPart.RIGHT;
                } else {
                    move = false;
                    currentScreenPart = ScreenPart.NONE;
                }
            }
        } else if (verticalEn == 1 || verticalFr == 1) {
            if (horizontalEn == -1 || horizontalFr == -1) {
                currentScreenPart = ScreenPart.UPLEFT;
            } else if (horizontalEn == 1 || horizontalFr == 1) {
                currentScreenPart = ScreenPart.UPRIGHT;
            } else {
                currentScreenPart = ScreenPart.UP;
            }
        } else if (verticalEn == -1 || verticalFr == -1) {
            if (horizontalEn == -1 || horizontalFr == -1) {
                currentScreenPart = ScreenPart.DOWNLEFT;
            } else if (horizontalEn == 1 || horizontalFr == 1) {
                currentScreenPart = ScreenPart.DOWNRIGHT;
            } else {
                currentScreenPart = ScreenPart.DOWN;
            }
        } else {
            if (horizontalEn == -1 || horizontalFr == -1) {
                currentScreenPart = ScreenPart.LEFT;
            } else if (horizontalEn == 1 || horizontalFr == 1) {
                currentScreenPart = ScreenPart.RIGHT;
            } else {
                move = false;
                currentScreenPart = ScreenPart.NONE;
            }
        }

        if (move) {
            Move();
        }
    }

    private void Move() {
        if (currentScreenPart == ScreenPart.UP || currentScreenPart == ScreenPart.UPRIGHT || currentScreenPart == ScreenPart.UPLEFT) {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, pos.z + speed * Time.deltaTime);
        } else if (currentScreenPart == ScreenPart.DOWN || currentScreenPart == ScreenPart.DOWNRIGHT || currentScreenPart == ScreenPart.DOWNLEFT) {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, pos.z - speed * Time.deltaTime);
        }

        if (currentScreenPart == ScreenPart.RIGHT || currentScreenPart == ScreenPart.UPRIGHT || currentScreenPart == ScreenPart.DOWNRIGHT) {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x + speed * Time.deltaTime, pos.y, pos.z);
        } else if (currentScreenPart == ScreenPart.LEFT || currentScreenPart == ScreenPart.DOWNLEFT || currentScreenPart == ScreenPart.UPLEFT) {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x - speed * Time.deltaTime, pos.y, pos.z);
        }
    }
}
