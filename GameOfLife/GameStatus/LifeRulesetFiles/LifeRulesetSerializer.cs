using System.Runtime.Serialization;

namespace GameOfLife.GameStatus.LifeRulesetFiles
{
	// A class for serializing and deserializing LifeRuleset
	// When serializing, the LifeRuleset passed constructor should be used to provide its data to the serialization process
	// When deserializing, the SerializationInfo and StreamingContex constructors will be called

	public class LifeRulesetSerializer : ISerializable
	{
		public int[] NeighborsToGrow;
		public int[] NeighborsToLive;
		public int[] NeighborsToDie;

		// An empty constructor is required when there is a Deserialization
		public LifeRulesetSerializer()
		{

		}

		// For serializing
		public LifeRulesetSerializer(LifeRuleset rules)
		{
			NeighborsToGrow = rules.GetGrowthArray();
			NeighborsToLive = rules.GetLivingArray();
			NeighborsToDie = rules.GetDeathArray();
		}

		// For deserializing and creating an object of LifeRulesetSerializer
		public LifeRulesetSerializer(SerializationInfo info, StreamingContext context)
		{
			NeighborsToGrow = (int[])info.GetValue("NeighborsToGrow", typeof(int[]));
			NeighborsToLive = (int[])info.GetValue("NeighborsToLive", typeof(int[]));
			NeighborsToDie = (int[])info.GetValue("NeighborsToDie", typeof(int[]));
		}

		// For passing information to the serialization process
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("NeighborsToGrow", NeighborsToGrow, typeof(int[]));
			info.AddValue("NeighborsToLive", NeighborsToLive, typeof(int[]));
			info.AddValue("NeighborsToDie", NeighborsToDie, typeof(int[]));
		}

		// Creates a LifeRuleset from a deserialized object
		public LifeRuleset ConvertToLifeRuleset()
		{
			return new LifeRuleset(NeighborsToGrow, NeighborsToLive, NeighborsToDie);
		}
	}
}
