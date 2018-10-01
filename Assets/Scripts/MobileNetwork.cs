using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour {

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRoom("testing");
    }

    public override void OnJoinedRoom()
    {
        GetComponent<MobileShooter>().Activate();
    }


}
