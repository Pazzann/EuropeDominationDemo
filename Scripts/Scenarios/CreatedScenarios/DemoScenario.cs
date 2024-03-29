using System;
using System.Collections.Generic;
using EuropeDominationDemo.Scripts.Enums;
using EuropeDominationDemo.Scripts.Math;
using EuropeDominationDemo.Scripts.Scenarios.Buildings;
using Godot;

namespace EuropeDominationDemo.Scripts.Scenarios.CreatedScenarios;

public class DemoScenario : Scenario
{
	public override Dictionary<int, CountryData> Countries { get; }
	public sealed override List<ArmyUnitData> ArmyUnits { get; set; }
	public sealed override ProvinceData[] Map { get; set; }
	public sealed override DateTime Date { get; set; }
	public sealed override Image MapTexture { get; set; }

	public DemoScenario(Image mapTexture)
	{
		MapTexture = mapTexture;
		
		Date = new DateTime(1444, 11, 12);
		
		Countries = new Dictionary<int, CountryData>()
		{
			{ 0, new CountryData(0, "Great Britain", new Vector3(0.0f, 1.0f, 0.0f), Modifiers.DefaultModifiers())},
			{ 1, new CountryData(1, "France", new Vector3(0.0f, 0.0f, 1.0f), Modifiers.DefaultModifiers()) },
			{ 2, new CountryData(2, "Sweden", new Vector3(1.0f, 0.0f, 0.0f), Modifiers.DefaultModifiers())}
		};

		Map = new ProvinceData[14]
		{
			new ProvinceData(0, 0, "London", Terrain.Coast, Good.Iron, 1, new float[] {0, 2}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(1,  0, "London", Terrain.Field, Good.Iron, 2, new float[] {0, 1}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(2,  0, "London", Terrain.Field, Good.Iron, 1, new float[] {2, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(3,  0, "London", Terrain.Forest, Good.Iron, 1, new float[] {0, 3}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(4,  0, "London", Terrain.Plain, Good.Iron, 1, new float[] {4, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(5,  0, "Paris", Terrain.Plain, Good.Iron, 2, new float[] {0, 4}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(6,  1, "Lorem", Terrain.Plain, Good.Iron, 2, new float[] {3, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(7,  1, "FlashBang", Terrain.Plain, Good.Wheat, 1, new float[] {1, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(8,  1, "CommunistPigs", Terrain.Plain, Good.Wheat, 2, new float[] {0, 5}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(9,  2, "Berlin", Terrain.Mountains, Good.Wheat, 1, new float[] {5, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(10, 2, "LibertarianTown", Terrain.Mountains, Good.Wheat, 1, new float[] {6, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(11, 2, "Liberty", Terrain.Mountains, Good.Wheat, 1, new float[] {0, 6}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(12,  2, "NY", Terrain.Coast, Good.Wheat, 1, new float[] {7, 0}, new List<Building>(), Modifiers.DefaultModifiers()),
			new ProvinceData(13, 2, "Los Angeles", Terrain.Coast, Good.Wheat, 1, new float[] {0, 7}, new List<Building>(), Modifiers.DefaultModifiers()),
		};

		ArmyUnits = new List<ArmyUnitData>() { 
			new ArmyUnitData("George Floyd",1, 3, Modifiers.DefaultModifiers()), 
			new ArmyUnitData("Idk",1, 5, Modifiers.DefaultModifiers()) ,
			new ArmyUnitData("FunnyName",1, 6, Modifiers.DefaultModifiers()) ,
			new ArmyUnitData("Length",1, 7, Modifiers.DefaultModifiers()) ,
		};
		
		Map = GameMath.CalculateBorderProvinces(Map, MapTexture);
		var centers = GameMath.CalculateCenterOfProvinceWeight(MapTexture, Map.Length);
		for (var i = 0; i < Map.Length; i++)
		{
			Map[i].CenterOfWeight = centers[i];
		}
	}
}
