using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Decks.Samples.Elements
{
    /// <summary>
    /// This is a sample from a space-based game that uses tiles to represent "terrain" in space.
    /// </summary>
    public class SpaceSector
    {
        public int Number { get; set; }
        public SpaceTerrain Terrain { get; set; }
        public List<ISectorContent> Contents { get; } = new List<ISectorContent>();

        public override string ToString()
        {
            var sb = new StringBuilder($"{Number} - {Terrain.Name}");

            switch (Contents.Count)
            {
                case 0:
                    sb.Append(" EMPTY");
                    break;
                case 1:
                    if (Contents.First().IsShown)
                    {
                        sb.Append($" ({Contents.First()})");
                    }
                    break;
                default:
                    sb.Append(" (");
                    sb.Append(string.Join(") (", Contents));
                    sb.Append(")");
                    break;
            }

            return sb.ToString();
        }

        public static void Initialize(IDeck<SpaceSector> deck)
        {
            Console.WriteLine("Adding Open Space ...");
            deck.Add(new SpaceSector { Number = 1, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 2, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 3, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 4, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 5, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 6, Terrain = SpaceTerrain.OpenSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector
            {
                Number = 7,
                Terrain = SpaceTerrain.OpenSpace,
                Contents = {
                    new Planet {
                        Satellites = {
                            new Satellite { Radius = 7, Size = SatelliteSize.Small }
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 8,
                Terrain = SpaceTerrain.OpenSpace,
                Contents = {
                    new Planet {
                        Satellites = {
                            new Satellite { Radius = 14, Size = SatelliteSize.Small }
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 9,
                Terrain = SpaceTerrain.OpenSpace,
                Contents = {
                    new Planet {
                        Satellites = {
                            new Satellite { Radius = 4, Size = SatelliteSize.Small },
                            new Satellite { Radius = 11, Size = SatelliteSize.Small },
                            new Satellite { Radius = 16, Size = SatelliteSize.Small }
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 10,
                Terrain = SpaceTerrain.OpenSpace,
                Contents = {
                    new Planet { Offset = 5 },
                    new Planet { Offset = 5 },
                }
            });

            Console.WriteLine("Adding Asteroids ...");
            deck.Add(new SpaceSector { Number = 11, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 12, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 13, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 14, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 15, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 16, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 17, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 18, Terrain = SpaceTerrain.Asteroids, Contents = { new Planet() } });

            Console.WriteLine("Adding Gas Giants ...");
            deck.Add(new SpaceSector
            {
                Number = 19,
                Terrain = SpaceTerrain.GasGiant,
                Contents = {
                    new Planet {
                        Satellites = {
                            new Satellite {Radius = 6, Size = SatelliteSize.Small},
                            new Satellite {Radius = 10, Size = SatelliteSize.Medium},
                            new Satellite {Radius = 20, Size = SatelliteSize.Small},
                        },
                        Diameter = 3,
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 20,
                Terrain = SpaceTerrain.GasGiant,
                Contents = {
                    new Planet {
                        Diameter = 5,
                        Satellites = {
                            new Satellite {Radius = 8, Size = SatelliteSize.Medium},
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 21,
                Terrain = SpaceTerrain.GasGiant,
                Contents = { new Planet { Diameter = 5 } }
            });
            deck.Add(new SpaceSector
            {
                Number = 22,
                Terrain = SpaceTerrain.GasGiant,
                Contents = { new Planet { Diameter = 7 } }
            });
            deck.Add(new SpaceSector
            {
                Number = 23,
                Terrain = SpaceTerrain.GasGiant,
                Contents = {
                    new Planet {
                        Diameter = 9,
                        Satellites = {
                            new Satellite {Radius = 15, Size = SatelliteSize.Large},
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 24,
                Terrain = SpaceTerrain.GasGiant,
                Contents = {
                    new Planet {
                        Diameter = 11,
                        Satellites = {
                            new Satellite {Radius = 13, Size = SatelliteSize.Large},
                            new Satellite {Radius = 19, Size = SatelliteSize.Small},
                        }
                    }
                }
            });
            deck.Add(new SpaceSector
            {
                Number = 25,
                Terrain = SpaceTerrain.GasGiant,
                Contents = { new Planet { Diameter = 11 } }
            });
            deck.Add(new SpaceSector
            {
                Number = 26,
                Terrain = SpaceTerrain.GasGiant,
                Contents = { new Planet { Diameter = 13 } }
            });

            Console.WriteLine("Adding Dust Clouds ...");
            deck.Add(new SpaceSector { Number = 27, Terrain = SpaceTerrain.DustCloud, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 28, Terrain = SpaceTerrain.DustCloud, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 29, Terrain = SpaceTerrain.DustCloud, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 30, Terrain = SpaceTerrain.DustCloud, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 31, Terrain = SpaceTerrain.DustCloud, Contents = { new Planet() } });
            deck.Add(new SpaceSector
            {
                Number = 32,
                Terrain = SpaceTerrain.DustCloud,
                Contents =
                {
                    new Planet
                    {
                        Satellites =
                        {
                            new Satellite { Radius = 12, Size = SatelliteSize.Small }
                        }
                    }
                }
            });

            Console.WriteLine("Adding Heat Zones ...");
            deck.Add(new SpaceSector { Number = 33, Terrain = SpaceTerrain.HeatZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 34, Terrain = SpaceTerrain.HeatZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 35, Terrain = SpaceTerrain.HeatZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 36, Terrain = SpaceTerrain.HeatZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 37, Terrain = SpaceTerrain.HeatZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector
            {
                Number = 38,
                Terrain = SpaceTerrain.HeatZone,
                Contents =
                {
                    new Planet
                    {
                        Satellites =
                        {
                            new Satellite { Radius = 4, Size = SatelliteSize.Small }
                        }
                    }
                }

            });

            Console.WriteLine("Adding Radiation Zones ...");
            deck.Add(new SpaceSector { Number = 39, Terrain = SpaceTerrain.RadiationZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 40, Terrain = SpaceTerrain.RadiationZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 41, Terrain = SpaceTerrain.RadiationZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 42, Terrain = SpaceTerrain.RadiationZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 43, Terrain = SpaceTerrain.RadiationZone, Contents = { new Planet() } });
            deck.Add(new SpaceSector
            {
                Number = 44,
                Terrain = SpaceTerrain.RadiationZone,
                Contents =
                {
                    new Planet
                    {
                        Satellites =
                        {
                            new Satellite { Radius = 17, Size = SatelliteSize.Small }
                        }
                    }
                }
            });

            Console.WriteLine("Adding Mine Fields ...");
            deck.Add(new SpaceSector { Number = 45, Terrain = SpaceTerrain.MineField, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 46, Terrain = SpaceTerrain.MineField, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 47, Terrain = SpaceTerrain.MineField, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 48, Terrain = SpaceTerrain.MineField, Contents = { new Planet() } });
            deck.Add(new SpaceSector
            {
                Number = 49,
                Terrain = SpaceTerrain.MineField,
                Contents =
                {
                    new Planet
                    {
                        Satellites =
                        {
                            new Satellite { Radius = 12, Size = SatelliteSize.Small }
                        }
                    }
                }
            });

            Console.WriteLine("Adding Black Holes ...");
            deck.Add(new SpaceSector { Number = 50, Terrain = SpaceTerrain.BlackHole, Contents = { new BlackHole() } });
            deck.Add(new SpaceSector { Number = 51, Terrain = SpaceTerrain.BlackHole, Contents = { new BlackHole() } });
            deck.Add(new SpaceSector { Number = 52, Terrain = SpaceTerrain.BlackHole, Contents = { new BlackHole() } });
            deck.Add(new SpaceSector { Number = 53, Terrain = SpaceTerrain.BlackHole, Contents = { new BlackHole() } });
            deck.Add(new SpaceSector { Number = 54, Terrain = SpaceTerrain.BlackHole, Contents = { new BlackHole() } });

            Console.WriteLine("Adding Nebulae ...");
            deck.Add(new SpaceSector
            {
                Number = 55,
                Terrain = SpaceTerrain.Nebula,
                Contents = { new Planet { HasAtomosphere = false } }
            });
            deck.Add(new SpaceSector
            {
                Number = 56,
                Terrain = SpaceTerrain.Nebula,
                Contents = { new Planet { HasAtomosphere = false } }
            });
            deck.Add(new SpaceSector
            {
                Number = 57,
                Terrain = SpaceTerrain.Nebula,
                Contents = { new Planet { HasAtomosphere = false } }
            });
            deck.Add(new SpaceSector
            {
                Number = 58,
                Terrain = SpaceTerrain.Nebula,
                Contents = { new Planet { HasAtomosphere = false } }
            });
            deck.Add(new SpaceSector
            {
                Number = 59,
                Terrain = SpaceTerrain.Nebula,
                Contents = { new Planet { HasAtomosphere = false } }
            });

            Console.WriteLine("Adding Voids ...");
            deck.Add(new SpaceSector { Number = 60, Terrain = SpaceTerrain.Void });
            deck.Add(new SpaceSector { Number = 61, Terrain = SpaceTerrain.Void });
            deck.Add(new SpaceSector { Number = 62, Terrain = SpaceTerrain.Void });
            deck.Add(new SpaceSector { Number = 63, Terrain = SpaceTerrain.Void });
            deck.Add(new SpaceSector { Number = 64, Terrain = SpaceTerrain.Void });

            Console.WriteLine("Adding unusual ...");
            deck.Add(new SpaceSector { Number = 65, Terrain = SpaceTerrain.BlackHoleMania, Contents = { new BlackHole() } });
            deck.Add(new SpaceSector { Number = 66, Terrain = SpaceTerrain.DragSpace, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 67, Terrain = SpaceTerrain.SubspaceCurrent, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 68, Terrain = SpaceTerrain.WebClusters, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 69, Terrain = SpaceTerrain.WrapAround, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 70, Terrain = SpaceTerrain.Cluster, Contents = { new Planet() } });

            Console.WriteLine("Adding central zones ...");
            deck.Add(new SpaceSector { Number = 71, Terrain = SpaceTerrain.Arena, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 72, Terrain = SpaceTerrain.Pass });
            deck.Add(new SpaceSector { Number = 73, Terrain = SpaceTerrain.Cage });
            deck.Add(new SpaceSector
            {
                Number = 74,
                Terrain = SpaceTerrain.Confluence,
                Contents = {
                    new Planet
                    {
                        Diameter = 11,
                        Satellites =
                        {
                            new Satellite { Radius = 9, Size = SatelliteSize.Medium },
                            new Satellite { Radius = 12, Size = SatelliteSize.Small },
                            new Satellite { Radius = 16, Size = SatelliteSize.Large },
                            new Satellite { Radius = 22, Size = SatelliteSize.Small },
                        }
                    }
                }
            });
            deck.Add(new SpaceSector { Number = 75, Terrain = SpaceTerrain.Monsters, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 76, Terrain = SpaceTerrain.Mystery });
            deck.Add(new SpaceSector { Number = 77, Terrain = SpaceTerrain.NeutronStar, Contents = { new NeutronStar() } });
            deck.Add(new SpaceSector { Number = 78, Terrain = SpaceTerrain.Racetrack });
            deck.Add(new SpaceSector { Number = 79, Terrain = SpaceTerrain.Ringworld, Contents = { new Planet() } });
            deck.Add(new SpaceSector { Number = 80, Terrain = SpaceTerrain.WhiteHole, Contents = { new WhiteHole() } });

        }
    }
    public interface ISectorContent { bool IsShown { get; } }

    public class BlackHole : ISectorContent
    {
        public bool IsShown { get { return true; } }

        public override string ToString()
        {
            return "BH";
        }
    }

    public class NeutronStar : ISectorContent
    {
        public bool IsShown { get { return true; } }

        public override string ToString()
        {
            return "NS";
        }
    }

    public class WhiteHole : ISectorContent
    {
        public bool IsShown { get { return true; } }

        public override string ToString()
        {
            return "WH";
        }
    }

    public class Planet : ISectorContent
        {
        public byte Offset { get; set; } = 0;
        public bool HasAtomosphere { get; set; } = true;
        public byte Diameter { get; set; } = 1;
        public List<Satellite> Satellites { get; } = new List<Satellite>();

        public bool IsShown { get { return !IsDefault; } }
        public bool IsDefault
        {
            get
            {
                return Offset == 0 && HasAtomosphere && Diameter == 1 && !Satellites.Any();
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            if(Offset != 0)
            {
                if (sb.Length > 0) { sb.Append(" "); }
                sb.Append($"Off{Offset}");
            }

            if (!HasAtomosphere)
            {
                if (sb.Length > 0) { sb.Append(" "); }
                sb.Append("*gasp*");
            }

            if (Diameter > 1)
            {
                if (sb.Length > 0) { sb.Append(" "); }
                sb.Append($"Dia{Diameter}");
            }

            if (Satellites.Count > 0)
            {
                if(sb.Length > 0) { sb.Append(" "); }
                sb.Append($"[{string.Join(",", Satellites)}]");
            }
            return sb.ToString();


        }
    }

    public class Satellite
    {
        public byte Radius { get; set; }
        public SatelliteSize Size { get; set; }
        public override string ToString()
        {
            return $"{Radius}{(char)Size}";
        }
    }

    public enum SatelliteSize : ushort
    {
        Small = (ushort)'s',
        Medium = (ushort)'m',
        Large = (ushort)'l'
    }
}
