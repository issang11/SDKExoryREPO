using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
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
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The LaneClear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["laneclear"]) &&
                Vars.Menu["spells"]["q"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        m.HasBuff("RyzeE") &&
                        m.IsValidTarget(Vars.Q.Range) &&
                        Vars.GetRealHealth(m) <
                                (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                {
                    Vars.Q.Cast(minion);
                }
            }

            /// <summary>
            ///     The LaneClear E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["laneclear"]) &&
                Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in Targets.Minions.Where(m => m.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.CastOnUnit(minion.HasBuff("RyzeE")
                        ? minion
                        : Targets.Minions[0]);
                }
            }

            foreach (var minion in Targets.JungleMinions)
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any(m => !m.HasBuff("RyzeE")))
                {
                    if (Vars.E.IsReady() &&
                        minion.IsValidTarget(Vars.E.Range) &&
                        !GameObjects.JungleSmall.Contains(minion) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["jungleclear"]) &&
                        Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.E.CastOnUnit(minion);
                    }
                }
                else
                {
                    /// <summary>
                    ///     The JungleClear Q Logic.
                    /// </summary>
                    if (Vars.Q.IsReady() &&
                        minion.IsValidTarget(Vars.Q.Range) &&
                        GameObjects.Player.ManaPercent >
                            ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["jungleclear"]) &&
                        Vars.Menu["spells"]["q"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                    {
                        Vars.Q.Cast(minion.ServerPosition);
                    }
                }
            }
        }
    }
}