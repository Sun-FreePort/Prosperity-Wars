using System.Collections.Generic;
using System.Linq;
using Lean.Localization;
using Nashet.UnityUIUtils;

namespace Nashet.EconomicSimulation
{
    public class InventionsPanelTable : UITableNew<KeyValuePair<Invention, bool>>
    {
        protected override IEnumerable<KeyValuePair<Invention, bool>> ContentSelector()
        {
            return Game.Player.Science.AllAvailableInventions().OrderBy(x => x.Value).ThenBy(x => x.Key.Cost.get());
        }

        protected override void AddRow(KeyValuePair<Invention, bool> invention, int number)
        {
            // Adding invention name
            AddCell(invention.Key.ToString(), invention.Key);
            ////Adding possibleStatues
            if (invention.Value)
                AddCell("Invented", invention.Key);
            else
                AddCell("Uninvented", invention.Key);
            ////Adding invention price
            AddCell(invention.Key.Cost.ToString(), invention.Key);
        }

        protected override void AddHeader()
        {
            // Adding invention name
            AddCell(LeanLocalization.GetTranslationText("inventions_panel/invention"));
            ////Adding possibleStatues
            AddCell(LeanLocalization.GetTranslationText("inventions_panel/status"));
            ////Adding invention price
            AddCell(LeanLocalization.GetTranslationText("inventions_panel/science_points"));
        }
    }
}