using System;
using System.ComponentModel;

namespace Decks.Samples.Elements
{
    public class SpaceTerrain
    {
        public static SpaceTerrain OpenSpace = new SpaceTerrain
        {
            Name = "Open Space",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain Asteroids = new SpaceTerrain
        {
            Name = "Asteroid Field",
            CanHavePopulationCenter = true,
            CanHaveMineralResource = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain GasGiant = new SpaceTerrain
        {
            Name = "Gas Giant",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain DustCloud = new SpaceTerrain
        {
            Name = "Dust Cloud",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain HeatZone = new SpaceTerrain
        {
            Name = "Heat Zone",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain RadiationZone = new SpaceTerrain
        {
            Name = "Radiation Zone",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain MineField = new SpaceTerrain
        {
            Name = "Mine Field",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All
        };
        public static SpaceTerrain BlackHole = new SpaceTerrain
        {
            Name = "Black Hole",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain Nebula = new SpaceTerrain
        {
            Name = "Nebula",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.ScientificOutpost,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain Void = new SpaceTerrain
        {
            Name = "Interstellar Void",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Economy = TerrainEconomy.Random,
            Band = TerrainBand.Vacant,
        };
        public static SpaceTerrain BlackHoleMania = new SpaceTerrain
        {
            Name = "Black Hole Mania",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain DragSpace = new SpaceTerrain
        {
            Name = "Drag Space",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain SubspaceCurrent = new SpaceTerrain
        {
            Name = "Subspace Current",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain WebClusters = new SpaceTerrain
        {
            Name = "Web Clusters",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain WrapAround = new SpaceTerrain
        {
            Name = "Wrap Around",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain Cluster = new SpaceTerrain
        {
            Name = "Star Cluster",
            CanHaveMineralResource = true,
            CanHavePopulationCenter = true,
            AllowedBases = BaseTypes.All,
            Band = TerrainBand.Unusual,
        };
        public static SpaceTerrain Arena = new SpaceTerrain
        {
            Name = "Arena",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Pass = new SpaceTerrain
        {
            Name = "Pass",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Cage = new SpaceTerrain
        {
            Name = "Cage",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Confluence = new SpaceTerrain
        {
            Name = "Confluence of Everything",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Monsters = new SpaceTerrain
        {
            Name = "Monsters",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Mystery = new SpaceTerrain
        {
            Name = "Mystery Zone",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain NeutronStar = new SpaceTerrain
        {
            Name = "Neutron Star",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Racetrack = new SpaceTerrain
        {
            Name = "Racetrack",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain Ringworld = new SpaceTerrain
        {
            Name = "Ringworld",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };
        public static SpaceTerrain WhiteHole = new SpaceTerrain
        {
            Name = "White Hole",
            CanHaveMineralResource = false,
            CanHavePopulationCenter = false,
            AllowedBases = BaseTypes.None,
            Band = TerrainBand.Central,
        };


        #region Instance Info
        private SpaceTerrain() { }
        public string Name { get; private set; }
        public bool CanHavePopulationCenter { get; private set; }
        public bool CanHaveMineralResource { get; private set; }
        public BaseTypes AllowedBases { get; private set; }
        public TerrainEconomy Economy { get; private set; }
        public TerrainBand Band { get; private set; }
        #endregion

    }

    public enum TerrainBand
    {
        Common,
        Unusual,
        Central,
        Vacant,
    }

    public enum TerrainEconomy
    {
        Normal =  0,
        Central = 1,
        Random = 2
    }

    [Flags]
    public enum BaseTypes
    {
        None = 0,
        ScientificOutpost = 1 << 0,
        MiningStation = 1 << 1,
        MilitaryBase = 1 << 2,
        All = ScientificOutpost | MiningStation | MilitaryBase
    }
}