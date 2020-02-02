using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;
namespace LiveSplit.EagleIsland
{
    public class EagleIslandFactory : IComponentFactory {
        public string ComponentName { get { return "Eagle Island Autosplitter v" + this.Version.ToString(); } }
        public string Description { get { return "Autosplitter for Eagle Island"; } }
        public ComponentCategory Category { get { return ComponentCategory.Control; } }
        public IComponent Create(LiveSplitState state) { return new EagleIslandComponent(state); }
        public string UpdateName { get { return this.ComponentName; } }
		public string UpdateURL { get { return "https://raw.githubusercontent.com/rjr5838/LiveSplit.EagleIsland/master/"; } }
		public string XMLURL { get { return this.UpdateURL + "Components/LiveSplit.EagleIsland.Updates.xml"; } }
		public Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
    }
}