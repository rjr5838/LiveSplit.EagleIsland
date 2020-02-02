using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace LiveSplit.EagleIsland
{

	public class EagleIslandComponent : IComponent {
		public string ComponentName { get { return "Eagle Island Autosplitter"; } }
		public TimerModel Model { get; set; }
		private String eagleLogPath = "_EagleIsland.log";
		public IDictionary<string, Action> ContextMenuControls { get { return null; } }
		private EagleIslandMemory mem;
		private EagleIslandSettings settings;
		private int currentSplit = 0;
		private int lastRoomType = 0;
		private int currentLevel = 0;
		private bool hasLog = false;
		private int lastLogCheck = 0;
		internal static List<string> keys = new List<string>() { "IntroActive", "RoomType", "CoordX", "CoordY", "HubEvent", "Level", "OrnisFrozen", "PosX", "PosY"};
		private Dictionary<string, string> currentValues = new Dictionary<string, string>();

		public EagleIslandComponent(LiveSplitState state, bool shown = false)
		{
			try
			{
				mem = new EagleIslandMemory();
				settings = new EagleIslandSettings(this);
				foreach (string key in keys)
				{
					currentValues[key] = "";
				}
				if (state != null)
				{
					Model = new TimerModel() { CurrentState = state };
					state.OnReset += OnReset;
					state.OnPause += OnPause;
					state.OnResume += OnResume;
					state.OnStart += OnStart;
					state.OnSplit += OnSplit;
					state.OnUndoSplit += OnUndoSplit;
					state.OnSkipSplit += OnSkipSplit;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public void GetValues()
		{
			if (!mem.HookProcess()) { return; }

			LogValues();

			if (Model != null && currentSplit < settings.Splits.Count)
			{
				bool shouldSplit = false;
				EagleIslandSplit split = settings.Splits[currentSplit];
				if (split.Field == "Start Game")
				{
					shouldSplit = mem.GetIntroPanelsActive();
				}
				else
				{
					Levels level;
					switch (split.Field)
					{
						case "Greenwood Glade":
						case "Gemshine Mines":
						case "Smuggleway Bay":
						case "Hollow Hestia":
						case "Falcon's Furnace":
						case "Tomb of the Raven":
							currentLevel = mem.GetLevel();
							level = (Levels)Enum.Parse(typeof(Levels), split.Value);
							shouldSplit = currentLevel == (int)level && lastRoomType == 4 && mem.GetRoomType() == 0 && mem.GetCoordX() == 0 && mem.GetCoordY() == 0;
							lastRoomType = mem.GetRoomType();
							break;
						case "Woodberry Warren":
						case "Sanctuary Swamp":
						case "Pinnacle Peak":
							currentLevel = mem.GetLevel();
							level = (Levels)Enum.Parse(typeof(Levels), split.Value);
							shouldSplit = currentLevel == (int)level && lastRoomType == 2 && mem.GetRoomType() == 0 && mem.GetCoordX() == 0 && mem.GetCoordY() == 0;
							lastRoomType = mem.GetRoomType();
							break;
						case "Armaura 1":
							shouldSplit = mem.GetHubEvent() == 16 && mem.GetPosX() == 1158672630 && mem.GetPosY() == 1173225472;
							break;
						case "Armaura 2":
							shouldSplit = mem.GetHubEvent() == 15 && mem.GetPosX() == 1154211840 && mem.GetPosY() == 1167802368;
							break;
						case "Ornis":
							shouldSplit = mem.GetOrnisFrozen() && mem.GetHubEvent() == 4;
							break;
					}

					
				}
				
				HandleSplit(shouldSplit, split);
			}
		}

		private void HandleSplit(bool shouldSplit, EagleIslandSplit split, bool shouldReset = false)
		{
			if (currentSplit > 0 && shouldReset)
			{
				Model.Reset();
			}
			else if (shouldSplit)
			{
				if (split.ShouldSplit)
				{
					if (Model.CurrentState.CurrentPhase == TimerPhase.NotRunning)
					{
						Model.Start();
					}
					else
					{
						Model.Split();
					}
				}
				else
				{
					currentSplit++;
				}
			}
		}

		public void Update(IInvalidator invalidator, LiveSplitState lvstate, float width, float height, LayoutMode mode)
		{
			try
			{
				IList<ILayoutComponent> components = lvstate.Layout.LayoutComponents;
				for (int i = components.Count - 1; i >= 0; i--)
				{
					ILayoutComponent component = components[i];
					if (component.Component is EagleIslandComponent && invalidator == null && width == 0 && height == 0)
					{
						components.Remove(component);
					}
				}


				GetValues();
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
		}

		public void OnReset(object sender, TimerPhase e)
		{
			currentSplit = 0;
			WriteLog("---------Reset----------------------------------");
		}
		public void OnResume(object sender, EventArgs e)
		{
			WriteLog("---------Resumed--------------------------------");
		}
		public void OnPause(object sender, EventArgs e)
		{
			WriteLog("---------Paused---------------------------------");
		}
		public void OnStart(object sender, EventArgs e)
		{
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------New Game-------------------------------");
		}

		public void OnSplit(object sender, EventArgs e)
		{
			currentSplit++;
			Model.CurrentState.IsGameTimePaused = true;
			WriteLog("---------Split----------------------------------");
		}

		public void OnUndoSplit(object sender, EventArgs e)
		{
			while (currentSplit > 0 && !settings.Splits[--currentSplit].ShouldSplit) { }
			while (currentSplit > 0 && !settings.Splits[currentSplit - 1].ShouldSplit)
			{
				currentSplit--;
			}
			WriteLog("---------Undo Split-----------------------------");
		}
		public void OnSkipSplit(object sender, EventArgs e)
		{
			while (currentSplit < settings.Splits.Count && !settings.Splits[currentSplit].ShouldSplit)
			{
				currentSplit++;
			}
			currentSplit++;
			WriteLog("---------Skip Split-----------------------------");
		}

		private void WriteLog(string data)
		{
			if (hasLog || !Console.IsOutputRedirected)
			{
				if (Console.IsOutputRedirected)
				{
					using (StreamWriter wr = new StreamWriter(eagleLogPath, true))
					{
						wr.WriteLine(data);
					}
				}
				else
				{
					Debug.WriteLine(data);
				}
			}
		}

		private void LogValues()
		{
			if (lastLogCheck == 0)
			{
				hasLog = File.Exists(eagleLogPath);
				lastLogCheck = 300;
			}
			lastLogCheck--;
			if (hasLog)
			{
				string prev = "", curr = "";
				foreach (string key in keys)
				{
					prev = currentValues[key];
					switch (key)
					{
						case "IntroActive": curr = mem.GetIntroPanelsActive().ToString(); break;
						case "RoomType": curr = mem.GetRoomType().ToString(); break;
						case "CoordX": curr = mem.GetCoordX().ToString(); break;
						case "CoordY": curr = mem.GetCoordY().ToString(); break;
						case "HubEvent": curr = mem.GetHubEvent().ToString(); break;
						case "Level": curr = mem.GetLevel().ToString(); break;
						case "OrnisFrozen": curr = mem.GetOrnisFrozen().ToString(); break;
						case "PosX": curr = mem.GetPosX().ToString(); break;
						case "PosY": curr = mem.GetPosY().ToString(); break;
					}

					if (curr == null) { curr = string.Empty; }
					if (!prev.Equals(curr))
					{
						WriteLog(DateTime.Now.ToString(@"HH\:mm\:ss.fff") + (Model != null ? " | " + Model.CurrentState.CurrentTime.RealTime.Value.ToString("G").Substring(3, 11) : "") + ": " + key + ": ".PadRight(30 - key.Length < 0 ? 0 : 30 - key.Length, ' ') + (prev.Length > 25 ? prev : prev.PadLeft(25, ' ')) + " -> " + curr);

						currentValues[key] = curr;
					}
				}
			}
		}

		public Control GetSettingsControl(LayoutMode mode) { return settings; }
		public void SetSettings(XmlNode doc)
		{
			settings.SetSettings(doc);
		}
		public XmlNode GetSettings(XmlDocument document) { return settings.UpdateSettings(document); }
		public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
		{
		}
		public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
		{
		}
		public float HorizontalWidth => 0;
		public float VerticalHeight => 0;
		public float MinimumHeight => 0;
		public float MinimumWidth => 0;
		public float PaddingTop => 0;
		public float PaddingLeft => 0;
		public float PaddingBottom => 0;
		public float PaddingRight => 0;
		public void Dispose() { }
	}
}