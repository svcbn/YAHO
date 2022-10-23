using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera_BH : MonoBehaviour
{
    // ī�޶� ������ ��ġ
    public Transform seatPos;
    public Transform editorPos;
    public Transform monitorPos;

    // ī�޶� �ӵ�
    public float cameraSpeed = 2f;

    // ī�޶� �����̴��� üũ�Ұ�
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 123������ ���� ����(�ӽ�)
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

    // ī�޶� �������� �̵�
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
