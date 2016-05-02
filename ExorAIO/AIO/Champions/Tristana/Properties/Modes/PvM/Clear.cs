using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Tristana
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Clear(EventArgs args)
        {
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The Clear Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.IsWindingUp &&
                (Targets.Minions.Any() || Targets.JungleMinions.Any()) &&
                Vars.Menu["spells"]["q"]["clear"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            /// <summary>
            ///     The Clear E Logics.
            /// </summary>
            if (Vars.E.IsReady())
            {
                /// <summary>
                ///     The JungleClear E Logic.
                /// </summary>
                if (Targets.JungleMinions.Any() &&
                    GameObjects.Player.ManaPercent > 
                        Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().SValue +
                        (int)(GameObjects.Player.Spellbook.GetSpell(Vars.E.Slot).ManaCost / GameObjects.Player.MaxMana * 100) &&
                    Vars.Menu["spells"]["e"]["jungleclear"].GetValue<MenuSliderButton>().BValue)
                {
                    Vars.E.CastOnUnit(Targets.JungleMinions[0]);
                }

                /// <summary>
                ///     The LaneClear E Logics.
                /// </summary>
                else
                {
                    /// <summary>
                    ///     The Aggressive LaneClear E Logic.
                    /// </summary>
                    if (GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(Vars.W.Range)) &&
                        GameObjects.Player.ManaPercent > 
                            Vars.Menu["spells"]["e"]["harass"].GetValue<MenuSliderButton>().SValue +
                            (int)(GameObjects.Player.Spellbook.GetSpell(Vars.E.Slot).ManaCost / GameObjects.Player.MaxMana * 100) &&
                        Vars.Menu["spells"]["e"]["harass"].GetValue<MenuSliderButton>().BValue)
                    {
                        foreach (var minion in Targets.Minions.Where(
                            m =>
                                m.CountEnemyHeroesInRange(150f) > 0 &&
                                m.Health < GameObjects.Player.GetAutoAttackDamage(m)))
                        {
                            Vars.E.CastOnUnit(minion);
                        }
                    }
                    else
                    {
                        /// <summary>
                        ///     The Normal LaneClear E Logic.
                        /// </summary>
                        if (Targets.Minions.Any() &&
                            GameObjects.Player.ManaPercent > 
                                Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().SValue +
                                (int)(GameObjects.Player.Spellbook.GetSpell(Vars.E.Slot).ManaCost / GameObjects.Player.MaxMana * 100) &&
                            Vars.Menu["spells"]["e"]["laneclear"].GetValue<MenuSliderButton>().BValue)
                        {
                            if (Targets.Minions.Count(m => m.Distance(Targets.Minions[0]) < 150f) >= 3)
                            {
                                Vars.E.CastOnUnit(Targets.Minions[0]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void BuildingClear(EventArgs args)
        {
            if (Variables.Orbwalker.GetTarget() as Obj_AI_Turret == null)
            {
                return;
            }

            /// <summary>
            ///     The E BuildingClear Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.ManaPercent > 
                    Vars.Menu["spells"]["e"]["buildings"].GetValue<MenuSliderButton>().SValue +
                    (int)(GameObjects.Player.Spellbook.GetSpell(Vars.E.Slot).ManaCost / GameObjects.Player.MaxMana * 100) &&
                Vars.Menu["spells"]["e"]["buildings"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.CastOnUnit(Variables.Orbwalker.GetTarget() as Obj_AI_Turret);
            }
        }
    }
}