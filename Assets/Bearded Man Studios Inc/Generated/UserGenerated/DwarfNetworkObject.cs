using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15]")]
	public partial class DwarfNetworkObject : NetworkObject
	{
		public const int IDENTITY = 3;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _netPosition;
		public event FieldEvent<Vector3> netPositionChanged;
		public InterpolateVector3 netPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 netPosition
		{
			get { return _netPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_netPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_netPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetnetPositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_netPosition(ulong timestep)
		{
			if (netPositionChanged != null) netPositionChanged(_netPosition, timestep);
			if (fieldAltered != null) fieldAltered("netPosition", _netPosition, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			netPositionInterpolation.current = netPositionInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _netPosition);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_netPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			netPositionInterpolation.current = _netPosition;
			netPositionInterpolation.target = _netPosition;
			RunChange_netPosition(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _netPosition);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (netPositionInterpolation.Enabled)
				{
					netPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					netPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_netPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_netPosition(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (netPositionInterpolation.Enabled && !netPositionInterpolation.current.UnityNear(netPositionInterpolation.target, 0.0015f))
			{
				_netPosition = (Vector3)netPositionInterpolation.Interpolate();
				//RunChange_netPosition(netPositionInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public DwarfNetworkObject() : base() { Initialize(); }
		public DwarfNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public DwarfNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
