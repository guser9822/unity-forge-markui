using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class DwarfNetworkObject : NetworkObject
	{
		public const int IDENTITY = 3;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private int _dwarfLevel;
		public event FieldEvent<int> dwarfLevelChanged;
		public Interpolated<int> dwarfLevelInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int dwarfLevel
		{
			get { return _dwarfLevel; }
			set
			{
				// Don't do anything if the value is the same
				if (_dwarfLevel == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_dwarfLevel = value;
				hasDirtyFields = true;
			}
		}

		public void SetdwarfLevelDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_dwarfLevel(ulong timestep)
		{
			if (dwarfLevelChanged != null) dwarfLevelChanged(_dwarfLevel, timestep);
			if (fieldAltered != null) fieldAltered("dwarfLevel", _dwarfLevel, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			dwarfLevelInterpolation.current = dwarfLevelInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _dwarfLevel);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_dwarfLevel = UnityObjectMapper.Instance.Map<int>(payload);
			dwarfLevelInterpolation.current = _dwarfLevel;
			dwarfLevelInterpolation.target = _dwarfLevel;
			RunChange_dwarfLevel(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _dwarfLevel);

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
				if (dwarfLevelInterpolation.Enabled)
				{
					dwarfLevelInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					dwarfLevelInterpolation.Timestep = timestep;
				}
				else
				{
					_dwarfLevel = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_dwarfLevel(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (dwarfLevelInterpolation.Enabled && !dwarfLevelInterpolation.current.UnityNear(dwarfLevelInterpolation.target, 0.0015f))
			{
				_dwarfLevel = (int)dwarfLevelInterpolation.Interpolate();
				//RunChange_dwarfLevel(dwarfLevelInterpolation.Timestep);
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
