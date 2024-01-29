using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineMenuManager : MonoBehaviourPunCallbacks
{

    // �{�^������������}�b�`���O�J�n
    // �����_���}�b�`���O�ŒN���̕���������΃}�b�`���O����
    // �������Ȃ���Ύ����ō��
    // �������Q���ɂȂ�΃V�[����J��
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
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        inRoom = true;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    // �����ɂQ�l�Ȃ�V�[����ς���
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
