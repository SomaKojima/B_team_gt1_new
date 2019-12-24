using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SI_PlayerSerializer
{
    //public static void Register()
    //{
    //    ExitGames.Client.Photon.PhotonPeer.RegisterType(typeof(SI_Player), (byte)'C', SerializeColor, DeserializeColor);
    //}

    //private static byte[] SerializeColor(object i_customobject)
    //{
    //    SI_Player player = (SI_Player)i_customobject;

    //    var bytes = new byte[4 * sizeof(float)];
    //    int index = 0;

    //    // placePoint
    //    foreach(int tmp in player.placePoint)
    //    {
    //        ExitGames.Client.Photon.Protocol.Serialize(tmp, bytes, ref index);
    //    }

    //    // itemCount
    //    foreach (int tmp in player.itemCount)
    //    {
    //        ExitGames.Client.Photon.Protocol.Serialize(tmp, bytes, ref index);
    //    }

    //    ExitGames.Client.Photon.Protocol.Serialize(player.id, bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Serialize(player.name, bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Serialize(player.isExcange, bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Serialize(player.ChangeFlag, bytes, ref index);

    //    return bytes;
    //}

    //private static object DeserializeColor(byte[] i_bytes)
    //{
    //    var player = new SI_Player();
    //    int index = 0;
    //    ExitGames.Client.Photon.Protocol.Deserialize(out color.r, i_bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Deserialize(out color.g, i_bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Deserialize(out color.b, i_bytes, ref index);
    //    ExitGames.Client.Photon.Protocol.Deserialize(out color.a, i_bytes, ref index);

    //    return color;
    //}
}
