namespace LiveSplit.EagleIsland
{
	public class EagleIslandSplit {
		public string Field { get; set; }
		public string LevelEnum { get; set; }
		public bool ShouldSplit { get; set; }

		public EagleIslandSplit(string field, string value, bool shouldSplit = true) {
			this.Field = field;
			this.LevelEnum = value;
			this.ShouldSplit = shouldSplit;
		}

		public override string ToString() {
			return Field + " = " + LevelEnum + (ShouldSplit ? " Split" : "");
		}
	}
}