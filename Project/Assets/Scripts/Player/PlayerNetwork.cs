using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<PlayerNetworkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);
    // private readonly NetworkVariable<Vector2> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    // private readonly NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);
    //add rotation for head too

    // Update is called once per frame
    void Update()
    {
        if (IsOwner) {
            _netState.Value = new PlayerNetworkData() {
                Position = transform.position,
                Rotation = transform.GetChild(1).transform.rotation.eulerAngles
            };
        }
        else {
            transform.position = _netState.Value.Position;   // need to interpolate here
            transform.GetChild(1).transform.rotation = Quaternion.Euler(
                0, 0, _netState.Value.Rotation.z);  // and here
        }
    }

    struct PlayerNetworkData : INetworkSerializable {
        internal Vector2 Position;
        private float _zRot;

        internal Vector3 Rotation {
            get => new Vector3(0, 0, _zRot);
            set => _zRot = value.z;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref Position);

            serializer.SerializeValue(ref _zRot);
        }
    }
}
