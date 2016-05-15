using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Veigar
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void LastHit(EventArgs args)
        {
            /// <summary>
            ///     The Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["lasthit"]) &&
                Vars.Menu["spells"]["q"]["lasthit"].GetValue<MenuSliderButton>().BValue)
            {
                if (Vars.Q.GetLineFarmLocation(Targets.Minions.Where(
                    m =>
                        Vars.GetRealHealth(m) <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).ToList(), Vars.Q.Width).MinionsHit >= 1)
                {
                    Vars.Q.Cast(Vars.Q.GetLineFarmLocation(Targets.Minions.Where(
                        m =>
                            Vars.GetRealHealth(m) <
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).ToList(), Vars.Q.Width).Position);
                }
            }
        }
    }
}