using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviourPunCallbacks
{
    #region 메인화면 component

    // 아이디 InputField
    public InputField inputID;

    // 비밀번호 IF
    public InputField inputPW;

    // 로그인 버튼
    public Button btnLogIn;

    // 비밀번호 찾기 버튼
    public Button btnFindPW;

    // 아이디 찾기 버튼
    public Button btnFindID;

    // 회원가입 버튼
    public Button btnSignUp;

    // 회원가입 패널
    public GameObject panelSignUp;
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
