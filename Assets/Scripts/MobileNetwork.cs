using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour {

    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // TODO-2.a: the same as 1.b
    //   and join a room


    //public override void OnJoinedRoom()
    //{
    //    GetComponent<MobileShooter>().Activate();
    //}


}
