using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
namespace LiveSplit.EagleIsland
{
	public partial class EagleIslandMemory {


		private static Encoding enc = Encoding.ASCII;
		private static ProgramPointer data = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "8BF06A008BCE33D2E8????????8D15????????E8????????6A64", 15));
		private static ProgramPointer quill = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "8985????????8985????????A1????????83", 13));
		private static ProgramPointer intro = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "A3????????C605????????0133D289", 7));
		private static ProgramPointer level = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.Steam, "8BF18BFA8935????????56B9", 6));
		private static bool ornisWasFrozen = false;
		public Process Program { get; set; }
		public bool IsHooked { get; set; } = false;
		private DateTime lastHooked;

		public EagleIslandMemory() {
			lastHooked = DateTime.MinValue;
		}

		public bool GetIntroPanelsActive()
		{
			// IntroPanels.active
			return intro.Read<bool>(Program, 0x0);
		}
				
		public int GetRoomType()
		{
			//quill.room.section.roomType
			return quill.Read<int>(Program, 0x0, 0x10, 0x28, 0x34);
		}

		public int GetCoordX()
		{
			//quill.room.coordX
			return quill.Read<int>(Program, 0x0, 0x10, 0x124);
		}

		public int GetCoordY()
		{
			//quill.room.coordY
			return quill.Read<int>(Program, 0x0, 0x10, 0x128);
		}

		public int GetHubEvent()
		{
			//quill.room.hubevent
			return quill.Read<int>(Program, 0x0, 0x10, 0x15C);
		}

		public int GetLevel()
		{
			// map.level
			return level.Read<int>(Program, 0x0);
		}

		public bool GetOrnisFrozen()
		{
			if (ornisWasFrozen)
			{
				return ornisWasFrozen;
			}

			// quill.room.tiles.items[0].frozenCounter
			ornisWasFrozen = GetHubEvent() == 18 && quill.Read<int>(Program, 0x0, 0x10, 0x64, 0x4, 0x8, 0x74) > 0;
			return ornisWasFrozen;
		}

		public string GetRawEnemyName()
		{
			// quill.room.enemies.items[0].rawEnemyName.stringLength
			int stringLength = quill.Read<int>(Program, 0x0, 0x10, 0xB8, 0x4, 0x8, 0x14, 0x4);
			// quill.room.enemies.items[0].rawEnemyName.firstChar
			byte[] stringBytes = quill.ReadBytes(Program, stringLength, 0x0, 0x10, 0xB8, 0x4, 0x8, 0x14, 0x8);

			return enc.GetString(stringBytes);
		}
		public string GetEnemyName()
		{
			// quill.room.enemies.items[0].enemyName.stringLength
			int stringLength = quill.Read<int>(Program, 0x0, 0x10, 0xB8, 0x4, 0x8, 0x18, 0x4);
			// quill.room.enemies.items[0].enemyName.firstChar
			byte[] stringBytes = quill.ReadBytes(Program, stringLength, 0x0, 0x10, 0xB8, 0x4, 0x8, 0x18, 0x8);

			return enc.GetString(stringBytes);
		}

		public int GetPosX()
		{
			// quill.posX
			return quill.Read<int>(Program, 0x0, 0xD4);
		}

		public int GetPosY()
		{
			// quill.posY
			return quill.Read<int>(Program, 0x0, 0xD8);
		}
		
		public bool HookProcess() {
			IsHooked = Program != null && !Program.HasExited;
			if (!IsHooked && DateTime.Now > lastHooked.AddSeconds(1)) {
				lastHooked = DateTime.Now;
				Process[] processes = Process.GetProcessesByName("EagleIsland");
				Program = processes.Length == 0 ? null : processes[0];
				if (Program != null) {
					MemoryReader.Update64Bit(Program);
				}
			}

			return IsHooked;
		}
		public void Dispose() {
			if (Program != null) {
				Program.Dispose();
			}
		}
	}

	public enum PointerVersion
	{
		Steam
	}
	public enum AutoDeref
	{
		None,
		Single,
		Double
	}
	public class ProgramSignature
	{
		public PointerVersion Version { get; set; }
		public string Signature { get; set; }
		public int Offset { get; set; }
		public ProgramSignature(PointerVersion version, string signature, int offset)
		{
			Version = version;
			Signature = signature;
			Offset = offset;
		}
		public override string ToString()
		{
			return Version.ToString() + " - " + Signature;
		}
	}
	public class ProgramPointer
	{
		private int lastID;
		private DateTime lastTry;
		private ProgramSignature[] signatures;
		private int[] offsets;
		public IntPtr Pointer { get; private set; }
		public PointerVersion Version { get; private set; }
		public AutoDeref AutoDeref { get; private set; }

		public ProgramPointer(AutoDeref autoDeref, params ProgramSignature[] signatures)
		{
			AutoDeref = autoDeref;
			this.signatures = signatures;
			lastID = -1;
			lastTry = DateTime.MinValue;
		}
		public ProgramPointer(AutoDeref autoDeref, params int[] offsets)
		{
			AutoDeref = autoDeref;
			this.offsets = offsets;
			lastID = -1;
			lastTry = DateTime.MinValue;
		}

		public T Read<T>(Process program, params int[] offsets) where T : struct
		{
			GetPointer(program);
			return program.Read<T>(Pointer, offsets);
		}
		public string Read(Process program, params int[] offsets)
		{
			GetPointer(program);
			return program.Read((IntPtr)program.Read<uint>(Pointer, offsets));
		}
		public byte[] ReadBytes(Process program, int length, params int[] offsets)
		{
			GetPointer(program);
			return program.Read(Pointer, length, offsets);
		}
		public void Write<T>(Process program, T value, params int[] offsets) where T : struct
		{
			GetPointer(program);
			program.Write<T>(Pointer, value, offsets);
		}
		public void Write(Process program, byte[] value, params int[] offsets)
		{
			GetPointer(program);
			program.Write(Pointer, value, offsets);
		}
		public IntPtr GetPointer(Process program)
		{
			if (program == null)
			{
				Pointer = IntPtr.Zero;
				lastID = -1;
				return Pointer;
			}
			else if (program.Id != lastID)
			{
				Pointer = IntPtr.Zero;
				lastID = program.Id;
			}

			if (Pointer == IntPtr.Zero && DateTime.Now > lastTry.AddSeconds(1))
			{
				lastTry = DateTime.Now;

				Pointer = GetVersionedFunctionPointer(program);
				if (Pointer != IntPtr.Zero)
				{
					if (AutoDeref != AutoDeref.None)
					{
						Pointer = (IntPtr)program.Read<uint>(Pointer);
						if (AutoDeref == AutoDeref.Double)
						{
							if (MemoryReader.is64Bit)
							{
								Pointer = (IntPtr)program.Read<ulong>(Pointer);
							}
							else
							{
								Pointer = (IntPtr)program.Read<uint>(Pointer);
							}
						}
					}
				}
			}
			return Pointer;
		}
		private IntPtr GetVersionedFunctionPointer(Process program)
		{
			if (signatures != null)
			{
				MemorySearcher searcher = new MemorySearcher();
				searcher.MemoryFilter = delegate (MemInfo info) {
					return (info.State & 0x1000) != 0 && (info.Protect & 0x40) != 0 && (info.Protect & 0x100) == 0;
				};
				for (int i = 0; i < signatures.Length; i++)
				{
					ProgramSignature signature = signatures[i];

					IntPtr ptr = searcher.FindSignature(program, signature.Signature);
					if (ptr != IntPtr.Zero)
					{
						Version = signature.Version;
						return ptr + signature.Offset;
					}
				}
			}
			else
			{
				IntPtr ptr = (IntPtr)program.Read<uint>(program.MainModule.BaseAddress, offsets);
				if (ptr != IntPtr.Zero)
				{
					return ptr;
				}
			}

			return IntPtr.Zero;
		}
	}
}