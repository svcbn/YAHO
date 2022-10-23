using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera_BH : MonoBehaviour
{
    // 카메라 움직일 위치
    public Transform seatPos;
    public Transform editorPos;
    public Transform monitorPos;

    // 카메라 속도
    public float cameraSpeed = 2f;

    // 카메라 움직이는지 체크할값
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 123번으로 시점 변경(임시)
        if(Input.GetKeyDown(KeyCode.Alpha1) && !isMoving)
        {
            StartCoroutine(moveCam(seatPos));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isMoving)
        {
            StartCoroutine(moveCam(editorPos));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isMoving)
        {
            StartCoroutine(moveCam(monitorPos));
        }
    }

    // 카메라 지점으로 이동
    IEnumerator moveCam(Transform mode)
    {
        isMoving = true;

        while (Vector3.Distance(Camera.main.transform.position, mode.position) > 0.02f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, mode.position, Time.deltaTime * cameraSpeed);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, mode.rotation, Time.deltaTime * cameraSpeed);
            yield return null;
        }

        isMoving = false;
    }


}
