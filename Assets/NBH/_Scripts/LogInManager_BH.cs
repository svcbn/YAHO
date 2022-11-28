using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class SignInData
{
    public string memberId;
    public string memberPw;
}

[System.Serializable]
public class MemberData
{
    public string memberId;
    public string memberPw;
    public string name;
    public string phone;
    public string email;
    public string address;
    public UserFace face;
}

[System.Serializable]
public class GetJsonData
{
    public int status;
    public string message;
    public string data;
}

public class GetFaceData
{
    public int status;
    public string message;
    public FaceData data;
}

[System.Serializable]
public class FaceData
{
    public string faceType;
    public string embd;
    public bool checkFace;
}

[System.Serializable]
public class UserFace
{
    public string front;
    public string left;
    public string right;
}

[System.Serializable]
public class LogInUserInfo : GetJsonData
{
    public new UserToken data;
}

[System.Serializable]
public class UserToken
{
    public string grantType;
    public string accessToken;
    public int accessTokenExpiresIn;
    public int memberNo;
}

public class GetUserData
{
    public MemberData data;
}

public class LogInManager_BH : MonoBehaviourPunCallbacks
{
    [Header("Main")]
    // �����г�
    public GameObject panelMain;
    #region ����ȭ�� component

    // ���̵� InputField
    public InputField inputID;

    // ��й�ȣ IF
    public InputField inputPW;

    // �α��� ��ư
    public Button btnLogIn;

    // ���̵� ã�� ��ư
    //public Button btnFindID;

    // ��й�ȣ ã�� ��ư
    //public Button btnFindPW;

    // ȸ������ ��ư
    public Button btnSignUp;

    // �α��ο� ����
    string _logInMemberId;
    string _logInMemberPw;

    // �ε��ִϸ��̼�
    public GameObject imgLoading;

    #endregion

    //[Header("FindID")]
    // ���̵� ã�� �г�
    //public GameObject panelFindID;
    #region ���̵� ã�� component

    // ���̵� ã�� �̸�
    //public InputField inputFindIDName;

    // ���̵� ã�� �̸���
    //public InputField inputFindIDEmail;

    // ���̵� ã�� ��ư
    //public Button btnFindIDSubmit;

    // ���̵� ã�� ���ư���
    //public Button btnFindIDReturn;

    #endregion

    //[Header("FindPW")]
    // ��й�ȣ ã�� �г�
    //public GameObject panelFindPW;
    #region ��й�ȣ ã�� component

    // ��й�ȣ ã�� ���̵�
    //public InputField inputFindPWID;

    // ��й�ȣ ã�� �̸�
    //public InputField inputFindPWName;

    // ��й�ȣ ã�� �̸���
    //public InputField inputFindPWEmail;

    // ��й�ȣ ã�� ��ư
    //public Button btnFindPWSubmit;

    // ��й�ȣ ã�� ���ư���
    //public Button btnFindPWReturn;

    #endregion

    [Header("SignUp")]
    // ȸ������ �г�
    public GameObject panelSignUp;
    #region ȸ������ component

    [Header(">ID")]
    // ȸ������ ���̵�
    public InputField inputSignUpID;

    // ȸ������ ���̵� �˻� ��ư
    //public Button btnSignUpIDCheck;

    // ȸ������ ���̵� �˻� ����
    public GameObject imageSignUpIDGood;

    // ȸ������ ���̵� �˻� ����
    public GameObject imageSignUpIDBad;

    // ȸ������ ���̵� �˻� ���� �ؽ�Ʈ
    public GameObject txtSignUpIDBad;

    [Header(">PW")]
    // ȸ������ ��й�ȣ
    public InputField inputSignUpPW;

    // ȸ������ ��й�ȣ ����
    public GameObject imageSignUpPWGood;

    // ȸ������ ��й�ȣ ����
    public GameObject imageSignUpPWBad;

    // ȸ������ ��й�ȣ ���� �ؽ�Ʈ
    public GameObject txtSignUpPWBad;

    // ȸ������ ��й�ȣ Ȯ��
    public InputField inputSignUpPWCheck;

    // ȸ������ ��й�ȣ �˻� ����
    public GameObject imageSignUpPWCheckGood;

    // ȸ������ ��й�ȣ �˻� ����
    public GameObject imageSignUpPWCheckBad;

    // ȸ������ ��й�ȣ Ȯ�� �ؽ�Ʈ
    public GameObject txtSignUpPWCheckBad;

    [Header("Face")]
    // ���� ���
    public Button btnFaceFront;
    public Button btnFaceLeft;
    public Button btnFaceRight;

    [Header("TakePicture")]
    public GameObject panelFace;
    public Text txtFaceType;
    public WebcamHandler_BH webcam;
    //public Button btnTakePicture;
    //public Button btnRetake;
    public Button btnSendPicture;
    public Button btnTakePictureReturn;

    enum FaceType
    {
        FRONT,
        LEFT,
        RIGHT
    };

    FaceType faceType;

    [Header(">")]
    // ȸ������ �̸�
    public InputField inputSignUpName;

    // ȸ������ �̸���
    public InputField inputSignUpEmail;

    // ȸ������ �ڵ�����ȣ
    public InputField inputSignUpPhone;

    // ȸ������ �ּ�
    public InputField inputSignUpAdress;

    // ȸ������ ��ư
    public Button btnSignUpSubmit;

    // ȸ������ ���ư��� ��ư
    public Button btnSignUpReturn;

    // ȸ�����Կ� ����
    string _memberId;
    string _memberPw;
    string _memberPwCheck;
    string _name;
    string _phone;
    string _email;
    string _address;
    string _faceFront;
    string _faceLeft;
    string _faceRight;

    #endregion

    [Header("EndSignUp")]
    // ȸ������ �Ϸ� �г�
    public GameObject panelNotice;
    // ȸ������ �Ϸ� ��ư
    public Button btnEndSignUp;

    int _memberNo;

    public GameObject canvasDebug;

    private void Awake()
    {
        DontDestroyOnLoad(canvasDebug);

    }

    void Start()
    {

        //panelFindID.SetActive(false);
        //panelFindPW.SetActive(false);
        panelSignUp.SetActive(false);
        panelFace.SetActive(false);
        panelNotice.SetActive(false);

        #region ��ư listener

        // �α��� ��ư
        btnLogIn.onClick.AddListener(OnbtnLogInClicked);

        // ���̵� ã�� ��ư
        //btnFindID.onClick.AddListener(OnbtnFindIDClicked);

        // ��й�ȣ ã�� ��ư
        //btnFindPW.onClick.AddListener(OnbtnFindPWClicked);

        // ȸ������ ��ư
        btnSignUp.onClick.AddListener(OnbtnSignUpClicked);

        // ���̵� ã�� ���� ��ư
        //btnFindIDSubmit.onClick.AddListener();

        // ���̵� ã�⿡�� ���ư���
        //btnFindIDReturn.onClick.AddListener(OnbtnFindIDReturnClicked);

        // ��й�ȣ ã�� ���� ��ư
        //btnFindPWSubmit.onClick.AddListener();

        // ��й�ȣ ã�⿡�� ���ư���
        //btnFindPWReturn.onClick.AddListener(OnbtnFindPWReturnClicked);

        // ȸ������ ���̵� �˻� ��ư
        //btnSignUpIDCheck.onClick.AddListener(OnbtnSignUpIDCheckClicked);

        // ȸ������ ���� ��ư

        btnSignUpSubmit.onClick.AddListener(OnbtnSignUpSubmitClicked);

        // ȸ������ ���� ���ư���
        btnSignUpReturn.onClick.AddListener(OnbtnSignUpReturnClicked);

        // ȸ������ �Ϸ� ���� �α������� ���ư���
        btnEndSignUp.onClick.AddListener(OnBtnNoticeSubmitClicked);

        btnFaceFront.onClick.AddListener(OnBtnFaceFrontClicked);
        btnFaceLeft.onClick.AddListener(OnBtnFaceLeftClicked);
        btnFaceRight.onClick.AddListener(OnBtnFaceRightClicked);
        //btnTakePicture.onClick.AddListener(OnBtnTakePictureClicked);
        //btnRetake.onClick.AddListener(OnBtnRetakeClicked);
        btnSendPicture.onClick.AddListener(OnBtnSendPictureClicked);
        btnTakePictureReturn.onClick.AddListener(OnBtnTakePictureReturnClicked);

        #endregion


        #region InputField Listener

        // ����ȭ�� inputfield
        inputID.onEndEdit.AddListener(OnEndEditInputID);
        inputPW.onEndEdit.AddListener(OnEndEditInputPW);
        inputPW.onSubmit.AddListener(OnEndSubmitInputPW);

        // ȸ������ inputfield
        inputSignUpID.onEndEdit.AddListener(OnEndEditInputSignUpID);
        inputSignUpPW.onEndEdit.AddListener(OnEndEditInputSignUpPW);
        inputSignUpPWCheck.onEndEdit.AddListener(OnEndEditInputSignUpPWCorrect);
        inputSignUpName.onEndEdit.AddListener(OnEndEditInputSignUpName);
        //inputSignUpPhone.onValueChanged.AddListener(OnValueChangedInputSignUpPhone);
        inputSignUpPhone.onEndEdit.AddListener(OnEndEditInputSignUpPhone);
        inputSignUpEmail.onEndEdit.AddListener(OnEndEditInputSignUpEmail);
        inputSignUpAdress.onEndEdit.AddListener(OnEndEditInputSignUpAdress);

        #endregion
    }

    private void Update()
    {
        if (panelFace.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                webcam.StopWebCam();
                CaptureImg();
                if (sendTexture != null)
                {
                    btnSendPicture.interactable = true;
                }
            }
        }
    }

    #region ��ư��

    // �α��� ��ư �������� ȣ��
    void OnbtnLogInClicked()
    {
        print("�α��� ��ư ����");

        LogIn();
    }

    // ���̵� ã�� �������� ȣ��
    //void OnbtnFindIDClicked()
    //{
    //    print("���̵� ã��");

    //    panelFindID.SetActive(true);
    //}

    // ���̵� ã�⿡�� ���ư��� ��ư �������� ȣ��
    //void OnbtnFindIDReturnClicked()
    //{
    //    print("���ư���");

    //    panelFindID.SetActive(false);
    //}

    // ��й�ȣ ã�� �������� ȣ��
    //void OnbtnFindPWClicked()
    //{
    //    print("��й�ȣ ã��");

    //    panelFindPW.SetActive(true);
    //}

    // ��й�ȣ ã�⿡�� ���ư��� ��ư �������� ȣ��
    //void OnbtnFindPWReturnClicked()
    //{
    //    print("���ư���");

    //    panelFindPW.SetActive(false);
    //}

    // ȸ������ �������� ȣ��
    void OnbtnSignUpClicked()
    {
        print("ȸ������");
        StartCoroutine(WindowClose(panelMain));
        StartCoroutine(WindowPopUp(panelSignUp));

    }

    // ȸ�����Կ��� ���ư��� ��ư �������� ȣ��
    void OnbtnSignUpReturnClicked()
    {
        print("���ư���");
        StartCoroutine(WindowClose(panelSignUp));
        StartCoroutine(WindowPopUp(panelMain));

    }

    void OnbtnSignUpSubmitClicked()
    {
        if (_memberId != null && signUpIdCheck && _memberPw != null && signUpPwCheck && _name != null && _email != null && _phone != null && _address != null && isFaceFrontDone && isFaceLeftDone && isFaceRightDone)
        {
            SignUp();
        }
        else
        {
            panelNotice.GetComponentInChildren<Text>().text = "�׸��� �ٽ� Ȯ�����ּ���!";
            StartCoroutine(WindowPopUp(panelNotice));
        }
    }

    bool signUpSuccess = false;
    void OnBtnNoticeSubmitClicked()
    {
        StartCoroutine(WindowClose(panelNotice));
        if (signUpSuccess)
        {
            StartCoroutine(WindowClose(panelSignUp));
            StartCoroutine(WindowPopUp(panelMain));
            signUpSuccess = false;
        }
    }

    bool isFaceFrontDone = false;
    void OnBtnFaceFrontClicked()
    {
        if (!isFaceFrontDone)
        {
            btnSendPicture.interactable = false;
            faceType = FaceType.FRONT;
            sendTexture = null;
            txtFaceType.text = "�����̽��ٸ� ���� ���� ������ �Կ����ּ���";
            panelFace.SetActive(true);
            webcam.StartWebCam();
        }
    }

    bool isFaceLeftDone = false;
    void OnBtnFaceLeftClicked()
    {
        if (!isFaceLeftDone)
        {
            btnSendPicture.interactable = false;
            faceType = FaceType.LEFT;
            sendTexture = null;
            txtFaceType.text = "�����̽��ٸ� ���� ������ ������ �Կ����ּ���";
            panelFace.SetActive(true);
            webcam.StartWebCam();
        }
    }

    bool isFaceRightDone = false;
    void OnBtnFaceRightClicked()
    {
        if (!isFaceRightDone)
        {
            btnSendPicture.interactable = false;
            faceType = FaceType.RIGHT;
            sendTexture = null;
            txtFaceType.text = "�����̽��ٸ� ���� ������ ������ �Կ����ּ���";
            panelFace.SetActive(true);
            webcam.StartWebCam();
        }
    }

    //void OnBtnTakePictureClicked()
    //{
    //    webcam.StopWebCam();
    //    CaptureImg();
    //    if(sendTexture != null)
    //    {
    //        btnSendPicture.interactable = true;
    //    }
    //}

    //void OnBtnRetakeClicked()
    //{
    //    webcam.StartWebCam();
    //}

    void OnBtnSendPictureClicked()
    {
        SendFaceImage();
    }

    void OnBtnTakePictureReturnClicked()
    {
        webcam.StopWebCam();
        panelFace.SetActive(false);
    }

    #endregion

    #region ��ǲ�ʵ�
    void OnEndEditInputID(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberId = s;
        }
    }

    void OnEndEditInputPW(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberPw = s;
        }
    }

    void OnEndSubmitInputPW(string s)
    {
        if (s.Length > 0)
        {
            _logInMemberPw = s;
            LogIn();
        }
    }

    void OnEndEditInputSignUpID(string s)
    {
        if (s.Length > 0)
        {
            _memberId = s;
            IdCheck();
        }
    }

    void OnEndEditInputSignUpPW(string s)
    {
        imageSignUpPWGood.SetActive(false);
        imageSignUpPWBad.SetActive(false);

        if (s.Length > 0)
        {
            if (s.Length > 9 && s.Length < 17)
            {
                imageSignUpPWGood.SetActive(true);
                txtSignUpPWBad.SetActive(false);
                _memberPw = s;
            }
            else
            {
                imageSignUpPWBad.SetActive(true);
                txtSignUpPWBad.GetComponent<Text>().color = Color.red;
                txtSignUpPWBad.SetActive(true);
            }
        }
    }

    bool signUpPwCheck = false;
    void OnEndEditInputSignUpPWCorrect(string s)
    {
        imageSignUpPWCheckGood.SetActive(false);
        imageSignUpPWCheckBad.SetActive(false);
        txtSignUpPWCheckBad.SetActive(false);
        if (s.Length > 0)
        {
            if (s == _memberPw)
            {
                imageSignUpPWCheckGood.SetActive(true);
                _memberPwCheck = s;
                signUpPwCheck = true;

            }
            else
            {
                imageSignUpPWCheckBad.SetActive(true);
                txtSignUpPWCheckBad.SetActive(true);
                signUpPwCheck = false;
            }
        }
    }

    void OnEndEditInputSignUpName(string s)
    {
        if (s.Length > 0)
        {
            _name = s;
        }
    }

    //void OnValueChangedInputSignUpPhone(string s)
    //{
    //    _phone = s;

    //    //if(s.Length == 3 && s.Length < 4)
    //    //{
    //    //    inputSignUpPhone.text += "-";
    //    //    inputSignUpPhone.MoveTextEnd(false);
    //    //    _phone += "-";
    //    //}
    //}

    void OnEndEditInputSignUpPhone(string s)
    {
        string phoneNumHyphen;

        if (s.Length == 11)
        {
            Regex regex = new Regex(@"01{1}[016789]{1}[0-9]{7,8}");
            Match m = regex.Match(s);

            phoneNumHyphen = s.Insert(3, "-");
            phoneNumHyphen = phoneNumHyphen.Insert(8, "-");
            
            _phone = phoneNumHyphen;
            inputSignUpPhone.text = _phone;
        }

    }

    void OnEndEditInputSignUpEmail(string s)
    {
        if (s.Length > 0)
        {
            _email = s;
        }
    }

    void OnEndEditInputSignUpAdress(string s)
    {
        if (s.Length > 0)
        {
            _address = s;
        }
    }

    #endregion

    #region â�ִϸ��̼�
    IEnumerator WindowPopUp(GameObject window)
    {
        float scale = 0f;
        window.transform.localScale = Vector3.one * scale;
        window.SetActive(true);

        while (scale < 1f)
        {
            scale = Mathf.Lerp(scale, 1.05f, 10 * Time.deltaTime);
            window.transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }

    IEnumerator WindowClose(GameObject window)
    {
        float scale = 1f;
        window.transform.localScale = Vector3.one * scale;

        while (scale > 0f)
        {
            scale = Mathf.LerpUnclamped(scale, -0.05f, 10 * Time.deltaTime);
            window.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        window.SetActive(false);
    }

    #endregion


    #region WebRequest
    void LogIn()
    {
        imgLoading.SetActive(true);

        SignInData data = new SignInData
        {
            memberId = _logInMemberId,
            memberPw = _logInMemberPw
        };

        HttpRequester requester = new HttpRequester
        {
            url = "http://43.201.58.81:8088/members/login",
            requestType = RequestType.POST,
            postData = JsonUtility.ToJson(data),
            onComplete = OnCompleteSignIn,
            onFailed = OnFailedSignIn
        };

        WebRequester_BH.instance.SendRequest(requester);
    }

    void SignUp()
    {
        UserFace userFace = new UserFace
        {
            front = _faceFront,
            left = _faceLeft,
            right = _faceRight
        };

        MemberData data = new MemberData
        {
            memberId = _memberId,
            memberPw = _memberPw,
            name = _name,
            phone = _phone,
            email = _email,
            address = _address,
            face = userFace
        };

        HttpRequester requester = new HttpRequester
        {
            url = "http://43.201.58.81:8088/members",
            requestType = RequestType.POST,
            postData = JsonUtility.ToJson(data),
            onComplete = OnCompleteSignUp,
            onFailed = OnFailedSignUp
        };

        WebRequester_BH.instance.SendRequest(requester);
    }

    bool signUpIdCheck = false;
    void IdCheck()
    {
        string Id = _memberId;
        HttpRequester requester = new HttpRequester
        {
            url = "http://43.201.58.81:8088/members/checkId/" + Id,
            requestType = RequestType.GET,
            onComplete = OnCompleteIdCheck
        };

        WebRequester_BH.instance.SendRequest(requester);
    }

    void Identify(int memberNo)
    {
        HttpRequester requester = new HttpRequester
        {
            url = "http://43.201.58.81:8088/members/auth/" + memberNo,
            requestType = RequestType.GET,
            onComplete = OnCompleteIdentify
        };

        WebRequester_BH.instance.SendRequest(requester);
    }

    void ClockIn(int _memberNo)
    {
        UserToken data = new UserToken
        {
            memberNo = _memberNo
        };

        HttpRequester requester = new HttpRequester
        {
            url = "http://43.201.58.81:8088/commutingManagement",
            requestType = RequestType.POST,
            postData = JsonUtility.ToJson(data),
            onComplete = OnCompleteClockIn
        };

        WebRequester_BH.instance.SendRequest(requester);
        
    }

    #endregion

    #region WebDownloadHandler

    public void OnCompleteSignIn(DownloadHandler handler)
    {
        LogInUserInfo jsonData = JsonUtility.FromJson<LogInUserInfo>(handler.text);
        

        if(jsonData.status == 200)
        {
            ClockIn(jsonData.data.memberNo);
            Identify(jsonData.data.memberNo);
            UserInformation_BH.instance.MemberNo = jsonData.data.memberNo;
            UserInformation_BH.instance.AccessToken = jsonData.data.accessToken;
        }
        else
        {
            imgLoading.SetActive(false);
            Debug.Log("�α��� ����");
        }
    }

    public void OnFailedSignIn()
    {
        imgLoading.SetActive(false);

        Debug.Log("�α��� ����");
        panelNotice.GetComponentInChildren<Text>().text = "ID�� PW�� Ȯ�����ּ���!";
        StartCoroutine(WindowPopUp(panelNotice));
    }

    public void OnCompleteSignUp(DownloadHandler handler)
    {

        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);
        if(jsonData.status == 400)
        {
            Debug.Log("ȸ������ ����");
            panelNotice.GetComponentInChildren<Text>().text = jsonData.message;
            StartCoroutine(WindowPopUp(panelNotice));
        }
        else if(jsonData.status == 200 && signUpIdCheck && signUpPwCheck)
        {
            Debug.Log("ȸ������ ����");
            panelNotice.GetComponentInChildren<Text>().text = "ȸ������ ����!";
            StartCoroutine(WindowPopUp(panelNotice));
            signUpSuccess = true;
        }
        else
        {
            Debug.Log("ȸ������ ����");
            panelNotice.GetComponentInChildren<Text>().text = "";
            StartCoroutine(WindowPopUp(panelNotice));
        }

    }

    public void OnFailedSignUp()
    {
        Debug.Log("ȸ������ ����");
        panelNotice.GetComponentInChildren<Text>().text = "����";
        StartCoroutine(WindowPopUp(panelNotice));
    }

    public void OnCompleteIdCheck(DownloadHandler handler)
    {
        imageSignUpIDGood.SetActive(false);
        imageSignUpIDBad.SetActive(false);
        txtSignUpIDBad.SetActive(false);

        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);
        if(jsonData.data == "true")
        {
            imageSignUpIDBad.SetActive(true);
            txtSignUpIDBad.SetActive(true);
            signUpIdCheck = false;

        }
        else
        {
            imageSignUpIDGood.SetActive(true);
            signUpIdCheck = true;
        }
    }

    public void OnCompleteIdentify(DownloadHandler handler)
    {
        GetUserData userData = new GetUserData();
        
        userData = JsonUtility.FromJson<GetUserData>(handler.text);

        Debug.Log(userData);
        PhotonNetwork.NickName = userData.data.name;

        UserInformation_BH.instance.MemberId = userData.data.memberId;
        UserInformation_BH.instance.Name = userData.data.name;
        
        ConnectMasterServer();
    }

    public void OnCompleteFaceCheck(DownloadHandler handler)
    {
        imgLoading.SetActive(false);
        GetFaceData faceData = JsonUtility.FromJson<GetFaceData>(handler.text);
        Debug.Log(faceData.data.faceType + " : " + faceData.data.embd);

        if(faceData.data.embd == "")
        {
            //���Կ�
            panelNotice.GetComponentInChildren<Text>().text = "�ٽ� �õ����ּ���";
            StartCoroutine(WindowPopUp(panelNotice));
            webcam.StartWebCam();
        }
        else
        {
            switch (faceData.data.faceType)
            {
                case "front":
                    _faceFront = faceData.data.embd;
                    break;
                case "left":
                    _faceLeft = faceData.data.embd;
                    break;
                case "right":
                    _faceRight = faceData.data.embd;
                    break;
            }

            panelFace.SetActive(false);

            switch (faceData.data.faceType)
            {
                case "front":
                    FaceCheckBtnDone(btnFaceFront);
                    isFaceFrontDone = true;
                    break;
                case "left":
                    FaceCheckBtnDone(btnFaceLeft);
                    isFaceLeftDone = true;
                    break;
                case "right":
                    FaceCheckBtnDone(btnFaceRight);
                    isFaceRightDone = true;
                    break;
            }
        }
    }

    void OnCompleteClockIn(DownloadHandler handler)
    {
        GetJsonData jsonData = JsonUtility.FromJson<GetJsonData>(handler.text);

        UserInformation_BH.instance.CommutingManagementNo = int.Parse(jsonData.data);
    }

    #endregion



    public void ConnectMasterServer()
    {
        // setting inspector ���� �ص� �� 
        PhotonNetwork.GameVersion = "1";

        //NameServer ����,(AppID, GameVersion, ����)
        PhotonNetwork.ConnectUsingSettings();
    }

    // ������ ������ ���� ����,  ���� �κ� ����ų� ������ �� ���� ����
    public override void OnConnected()
    {
        base.OnConnected();
        print("�����ͼ��� ���� ����");
    }

    // �����ͼ����� ����, �κ���� �� ������ ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("�����ͼ��� ���� �Ϸ�");

        //�⺻ �κ� ���� 
        PhotonNetwork.JoinLobby();
    }

    // �κ� ���� ������ ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("�κ� ���� ����");
        PhotonNetwork.LoadLevel(1);
    }


    Texture originMesh;
    byte[] sendTexture;

    void CaptureImg()
    {
        originMesh = GameObject.Find("Screen").GetComponent<RawImage>().mainTexture;
        sendTexture = Texture2Byte(originMesh);
    }

    byte[] Texture2Byte(Texture texture)
    {
        Texture2D texture2D = new Texture2D((int)(texture.width), (int)(texture.height));

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture2D.width, texture2D.height, 32);
        Graphics.Blit(texture, renderTexture);
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        
        Color[] pixels = texture2D.GetPixels();
        RenderTexture.active = currentRT;

        byte[] bArray = texture2D.EncodeToPNG(); //texture2D.GetRawTextureData();
        return bArray;
    }

    void SendFaceImage()
    {
        string type = "";
        switch (faceType)
        {
            case FaceType.FRONT:
                type = "front";
                break;
            case FaceType.LEFT:
                type = "left";
                break;
            case FaceType.RIGHT:
                type = "right";
                break;
        }

        StartCoroutine(WebRequester_BH.instance.UploadPNG(sendTexture, type));
        imgLoading.SetActive(true);

    }

    void FaceCheckBtnDone(Button button)
    {
        button.interactable = false;
        button.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 1);
    }
}
