namespace OldGameLogic.Model.Element.Vill
{
	public interface IVillBasicInfo {
		ulong ID { get; }
		string FirstName { get; }
		string LastName { get; }
		Coord Coord { get; }
	}
}