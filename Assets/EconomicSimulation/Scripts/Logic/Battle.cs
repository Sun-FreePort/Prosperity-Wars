using System.Text;
using Lean.Localization;
using Nashet.UnityUIUtils;

namespace Nashet.EconomicSimulation
{
    //[MenuItem("Tools/MyTool/Do It in C#")]
    public class BattleResult
    {
        private readonly Staff attacker, defender;

        //Army attackerArmy, attackerLoss, defenderArmy, defenderLoss;
        private int attackerArmy, attackerLoss, defenderArmy, defenderLoss;

        private bool result;
        private Province place;
        private StringBuilder sb = new StringBuilder();
        private string attackerBonus; private string defenderBonus;

        //public BattleResult(Country attacker, Country defender, Army attackerArmy, Army attackerLoss, Army defenderArmy, Army defenderLoss, bool result)
        public BattleResult(Staff attacker, Staff defender, int attackerArmy, int attackerLoss, int defenderArmy, int defenderLoss,
            Province place, bool result, string attackerBonus, string defenderBonus)
        {
            this.attacker = attacker;
            this.defender = defender;
            //this.attackerArmy = new Army(attackerArmy); this.attackerLoss = new Army(attackerLoss); this.defenderArmy = new Army(defenderArmy); this.defenderLoss = new Army(defenderLoss);
            this.attackerArmy = attackerArmy; this.attackerLoss = attackerLoss; this.defenderArmy = defenderArmy; this.defenderLoss = defenderLoss;
            this.result = result;
            this.place = place;
            this.defenderBonus = defenderBonus;
            this.attackerBonus = attackerBonus;
            //Game.allBattles.Add(this);
        }

        public bool isAttackerWon()
        {
            return result;
        }

        public bool isDefenderWon()
        {
            return !result;
        }

        public void createMessage()
        {
            sb.Clear();

            if (attacker.IsHuman && isAttackerWon())
            {
                //.Append(" owned by ").Append(place.Country)
                var message = LeanLocalization.GetTranslationText("battle/player_attack_won");
                sb.Append(string.Format(message,
                    defender, place, attackerArmy, attackerBonus,
                    defenderArmy, defenderBonus, attackerLoss, place));
                
                MessageSystem.Instance.NewMessage(LeanLocalization.GetTranslationText("battle/player_won_title"), sb.ToString(), LeanLocalization.GetTranslationText("fine"), false, place.provinceMesh.Position);
            }
            else if (defender.IsHuman && isDefenderWon())
            {
                var message = LeanLocalization.GetTranslationText("battle/player_defend_won");
                sb.Append(string.Format(message,
                    place, attacker, attackerArmy, attackerBonus,
                    defenderArmy, defenderBonus, defenderLoss));
                
                MessageSystem.Instance.NewMessage(LeanLocalization.GetTranslationText("battle/player_won_title"), sb.ToString(), LeanLocalization.GetTranslationText("fine"), true, place.provinceMesh.Position);
            }
            else if (attacker.IsHuman && isDefenderWon())
            {
                var message = LeanLocalization.GetTranslationText("battle/player_attack_loss");
                sb.Append(string.Format(message,
                    defender, place, attackerArmy, attackerBonus
                    , defenderArmy, defenderBonus, defenderLoss));

                MessageSystem.Instance.NewMessage(LeanLocalization.GetTranslationText("battle/player_lost_title"), sb.ToString(), LeanLocalization.GetTranslationText("fine"), false, place.provinceMesh.Position);
            }
            else if (defender.IsHuman && isAttackerWon())
            {
                var message = LeanLocalization.GetTranslationText("battle/player_defend_loss_1");
                sb.Append(string.Format(message,
                    place, attacker, attackerArmy, attackerBonus, defenderArmy, defenderBonus, attackerLoss));
                
                var movement = attacker as Movement;
                if (movement == null)
                    sb.Append(string.Format(LeanLocalization.GetTranslationText("battle/player_defend_loss_2"), place));
                else
                    sb.Append(string.Format(LeanLocalization.GetTranslationText("battle/player_defend_loss_3"), movement.getGoal()));
                
                MessageSystem.Instance.NewMessage(LeanLocalization.GetTranslationText("battle/player_lost_title"), sb.ToString(), LeanLocalization.GetTranslationText("not_fine"), false, place.provinceMesh.Position);
            }
        }

        public Staff getDefender()
        {
            return defender;
        }

        public Staff getAttacker()
        {
            return attacker;
        }
    }
}