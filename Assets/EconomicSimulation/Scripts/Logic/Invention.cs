using Nashet.Conditions;
using Nashet.UnityUIUtils;
using Nashet.Utils;
using Nashet.ValueSpace;
using System.Collections.Generic;
using Lean.Localization;

namespace Nashet.EconomicSimulation
{
    public class Invention : Name, IClickable
    {
        protected static readonly List<Invention> allInventions = new List<Invention>();
        protected readonly string description;
        public Value Cost { get; protected set; }

        /// <summary>ICanInvent scope</summary>
        public ConditionsList InventedPreviousTechs { get; protected set; } = new ConditionsList();
        public Condition Invented { get; protected set; }

        public static readonly Invention
            Farming = new Invention("inventions/farming",
                "inventions/farming_tips",
                new Value(100f)),
            Banking = new Invention("inventions/banking",
                "inventions/banking_tips",
                new Value(100f)),

            Manufactures = new Invention("inventions/manufactures",
                "inventions/manufactures_tips",
                new Value(80f)),
            JohnKayFlyingshuttle = new Invention("inventions/flying_shuttle",
                "inventions/flying_shuttle_tips",
                new Value(60f)),
            Mining = new Invention("inventions/mining",
                "inventions/mining_tips",
                new Value(100f)),
            //religion = new InventionType("Religion", "Allows clerics, gives loyalty boost", new Value(100f)),
            Metal = new Invention("inventions/metal",
                "inventions/metal_tips",
                new Value(100f)),
            // Add here capitalism and link it to serfdom
            IndividualRights = new Invention("inventions/classical_liberalism",
                "inventions/classical_liberalism_tips",
                new Value(80f)),
            Keynesianism = new Invention("inventions/keynesianism",
                "inventions/keynesianism_tips",
                new Value(80f), IndividualRights),

            Collectivism = new Invention("inventions/collectivism",
                "inventions/collectivism_tips",
                new Value(100f)),
            SteamPower = new Invention("inventions/steam_power",
                "inventions/steam_power_tips",
                new Value(100f), Metal, Manufactures),

            Welfare = new Invention("inventions/welfare",
                "inventions/welfare_tips",
                new Value(90f)),
            Gunpowder = new Invention("inventions/gunpowder_tips",
                "inventions/gunpowder_tips_tips",
                new Value(100f), Metal),
            Firearms = new Invention("inventions/hand_cannons",
                "inventions/hand_cannons_tips",
                new Value(200f), Gunpowder),

            CombustionEngine = new Invention("inventions/combustion_engine",
                "inventions/combustion_engine_tips",
                new Value(400f), SteamPower),

            Tanks = new Invention("inventions/tanks",
                "inventions/tanks_tips",
                new Value(800f), CombustionEngine),
            Airplanes = new Invention("inventions/airplanes",
                "inventions/airplanes_tips",
                new Value(1200f), CombustionEngine),
            ProfessionalArmy = new Invention("inventions/professional_army",
                "inventions/professional_army_tips",
                new Value(200f)),
            Domestication = new Invention("inventions/domestication",
                "inventions/domestication_tips",
                new Value(100f)),

            Electronics = new Invention("inventions/electronics",
                "inventions/electronics_tips",
                new Value(1000f), Airplanes),
            Tobacco = new Invention("inventions/tobacco",
                "inventions/tobacco_tips",
                new Value(100f)),
            //Coal = new Invention("inventions/coal",
            //"inventions/coal_tips",
            //new Value(100f), Metal),
            Universities = new Invention("inventions/universities",
                "inventions/universities_tips",
                new Value(150f));



        protected Invention(string name, string description, Value cost, params Invention[] requiredInventions) : base(name)
        {
            this.description = LeanLocalization.GetTranslationText(description);
            this.Cost = cost;
            allInventions.Add(this);
            if (requiredInventions != null)
                foreach (var item in requiredInventions)
                {
                    InventedPreviousTechs.add(new Condition(x => (x as IInventor).Science.IsInvented(item), item.ShortName + " aren't invented", true));
                }
            Invented = new Condition(x => (x as IInventor).Science.IsInvented(this), "Invented " + name, true);
        }

        public static IEnumerable<Invention> All
        {
            get
            {
                foreach (var item in allInventions)
                {
                    yield return item;
                }
            }
        }

        public bool CanInvent(IInventor inventor)
        {
            return InventedPreviousTechs.isAllTrue(inventor);
        }

        public override string FullName
        {
            get { return description; }
        }

        public void OnClicked()
        {
            Game.Player.events.RiseClickedOn(new InventionEventArgs(this));
            //MainCamera.inventionsPanel.selectInvention(this);
            //MainCamera.inventionsPanel.Refresh();
        }
    }
}