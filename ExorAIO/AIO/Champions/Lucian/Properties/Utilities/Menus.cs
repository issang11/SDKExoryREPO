using System.Linq;
using System.Windows.Forms;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using Menu = LeagueSharp.SDKEx.UI.Menu;

namespace ExorAIO.Champions.Lucian
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",       true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal",   true));
                    Vars.QMenu.Add(new MenuSliderButton("laneclear",   "LaneClear / if Mana >= x%",   50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("jungleclear", "JungleClear / if Mana >= x%", 50, 0, 99, true));
                    {
                        /// <summary>
                        ///     Sets the Extended Q menu.
                        /// </summary>
                        Vars.Q2Menu = new Menu("extended", "Use Extended Q in:", true);
                        {
                            Vars.Q2Menu.Add(new MenuBool("exkillsteal", "KillSteal",  true));
                            Vars.Q2Menu.Add(new MenuBool("excombo",     "Combo Mode", true));
                            Vars.Q2Menu.Add(new MenuSliderButton("mixed",       "Mixed Mode / if Mana >= %",     50, 0, 99, true));
                            Vars.Q2Menu.Add(new MenuSliderButton("exlaneclear", "LaneClear Mode / if Mana >= %", 50, 0, 99, true));
                        }
                        Vars.QMenu.Add(Vars.Q2Menu);

                        /// <summary>
                        ///     Sets the Whitelist menu for the Q.
                        /// </summary>
                        if (GameObjects.EnemyHeroes.Any())
                        {
                            Vars.WhiteListMenu = new Menu("whitelist", "Extended Harass: Whitelist", true);
                            {
                                Vars.WhiteListMenu.Add(new MenuSeparator("extendedsep", "Note: The Whitelist only works for Mixed and LaneClear."));

                                foreach (var target in GameObjects.EnemyHeroes)
                                {
                                    Vars.WhiteListMenu.Add(
                                        new MenuBool(
                                            target.ChampionName.ToLower(),
                                            $"Harass: {target.ChampionName}",
                                            true));
                                }
                            }
                            Vars.QMenu.Add(Vars.WhiteListMenu);
                        }
                    }
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",     "Combo",       true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal",   true));
                    Vars.WMenu.Add(new MenuSliderButton("buildings",   "Buildings / if Mana >= x%",   50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("laneclear",   "LaneClear / if Mana >= x%",   50, 0, 99, true));
                    Vars.WMenu.Add(new MenuSliderButton("jungleclear", "JungleClear / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",          true));
                    Vars.EMenu.Add(new MenuBool("engager",   "Engager",        true));
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.EMenu.Add(new MenuSliderButton("buildings",   "Buildings / if Mana >= x%",   50, 0, 99, true));
                    Vars.EMenu.Add(new MenuSliderButton("laneclear",   "LaneClear / if Mana >= x%",   50, 0, 99, true));
                    Vars.EMenu.Add(new MenuSliderButton("jungleclear", "JungleClear / if Mana >= x%", 50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("bool", "Semi-Automatic R", true));
                    Vars.RMenu.Add(
                        new MenuKeyBind("key", "Key:", Keys.T, KeyBindType.Press));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("qe", "Q Extended Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}