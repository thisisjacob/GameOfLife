using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GameOfLife.GameStatus.LifeRulesetFiles
{
	class LifeRulesetSerializer : ISerializable
	{
		public int[] NeighborsToGrow;
		public int[] NeighborsToLive;
		public int[] NeighborsToDie;

		public LifeRulesetSerializer()
		{

		}

		public LifeRulesetSerializer(LifeRuleset rules)
		{
			NeighborsToGrow = rules.GetGrowthArray();
			NeighborsToLive = rules.GetLivingArray();
			NeighborsToDie = rules.GetDeathArray();
		}

		public LifeRulesetSerializer(SerializationInfo info, StreamingContext context)
		{
			NeighborsToGrow = (int[])info.GetValue("NeighborsToGrow", typeof(int[]));
			NeighborsToLive = (int[])info.GetValue("NeighborsToLive", typeof(int[]));
			NeighborsToDie = (int[])info.GetValue("NeighborsToDie", typeof(int[]));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("NeighborsToGrow", NeighborsToGrow, typeof(int[]));
			info.AddValue("NeighborsToLive", NeighborsToLive, typeof(int[]));
			info.AddValue("NeighborsToDie", NeighborsToDie, typeof(int[]));
		}
	}
}
