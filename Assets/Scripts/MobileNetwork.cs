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
        //PhotonNetwork.Instantiate("PhoneCube", new Vector3(0, 0, 0), Quaternion.identity, 0);
        GetComponent<MobileShooter>().Activate();
    }


}
