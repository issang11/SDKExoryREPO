using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Jinx
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
        public static void Combo(EventArgs args)
        {
            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.W.Range) &&
                        t.CountEnemyHeroesInRange(225f) >=
                            Vars.Menu["spells"]["r"]["aoe"].GetValue<MenuSliderButton>().SValue))
                {
                    Vars.R.Cast(target.ServerPosition);
                }
            }

            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                Targets.Target.CountEnemyHeroesInRange(Vars.E.Width) >= 2 &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(
                    Targets.Target.ServerPosition, GameObjects.Player.Distance(Targets.Target) + Targets.Target.BoundingRadius));
            }

            if (GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(Vars.PowPow.Range)))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !GameObjects.Player.IsUnderEnemyTurret() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.W.GetPrediction(Targets.Target).CollisionObjects.Any())
                {
                    Vars.W.Cast(Vars.W.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }
    }
}