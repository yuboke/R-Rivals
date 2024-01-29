using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineMenuManager : MonoBehaviourPunCallbacks
{

    // ボタンを押したらマッチング開始
    // ランダムマッチングで誰かの部屋があればマッチング成功
    // 部屋がなければ自分で作る
    // 部屋が２名になればシーンを遷移
    [SerializeField] GameObject loadingAnim;
    [SerializeField] GameObject matchingButton;
    [SerializeField] GameObject matchingText;
    bool inRoom;
    bool isMatching;

    public void OnMatchingButton()
    {
        loadingAnim.SetActive(true);
        matchingButton.SetActive(false);
        matchingText.SetActive(true);
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        inRoom = true;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    // 部屋に２人ならシーンを変える
    private void Update()
    {
        if (isMatching)
        {
            return;
        }
        if (inRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMatching = true;
                SceneManager.LoadScene("Game");
            }
        }
    }
}
