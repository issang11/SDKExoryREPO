using System;
using ExorAIO.Utilities;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Nunu
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
            if (!Targets.Target.IsValidTarget() ||
                Bools.HasAnyImmunity(Targets.Target))
            {
                return;
            }

            if (!Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget(Vars.AARange))
            {
                /// <summary>
                ///     The E Combo Logic.
                /// </summary>
                if (Vars.E.IsReady() &&
                    Targets.Target.IsValidTarget(Vars.E.Range) &&
                    Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
                {
                    Vars.E.CastOnUnit(Targets.Target);
                }
            }
        }
    }
}