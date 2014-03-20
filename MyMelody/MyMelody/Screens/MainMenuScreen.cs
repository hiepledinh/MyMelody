#region File Description
//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;
#endregion

namespace MyMelody
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        Texture2D playIcon, exitIcon;
        #region Initialization


        public MainMenuScreen(ContentManager cont)
            : base("Main Menu", cont, 80)
        {
            EnabledGestures = GestureType.Tap | GestureType.VerticalDrag;
            // Create our menu entries.
        }


        public override void LoadContent()
        {
            playIcon = Content.Load<Texture2D>("Pictures/play");
            exitIcon = Content.Load<Texture2D>("Pictures/exit");

            MenuEntry playGameMenuEntry = new MenuEntry("", playIcon);
            MenuEntry optionsMenuEntry = new MenuEntry("", exitIcon);

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            base.LoadContent();
        }

        #endregion

        #region Handle Input
        public override void HandleInput(InputState input)
        {

            base.HandleInput(input);
            foreach (GestureSample gesture in input.Gestures)
            {
                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                        Point tapLocation = new Point((int)gesture.Position.X, (int)gesture.Position.Y);
                        for (int i = 0; i < MenuEntries.Count; i++)
                        {
                            MenuEntry menuEntry = MenuEntries[i];

                            if (GetMenuEntryHitBounds(menuEntry).Contains(tapLocation))
                            {
                                OnSelectEntry(i, PlayerIndex.One);
                            }
                        }
                        break;

                    case GestureType.VerticalDrag:
                        if (gesture.Delta.Y > 0)
                        {
                            foreach (MenuEntry menu in MenuEntries)
                            {
                                menu.PositionY += gesture.Delta.Y;
                            }
                        }
                        if (gesture.Delta.Y < 0)
                        {
                            foreach (MenuEntry menu in MenuEntries)
                            {
                                menu.PositionY += gesture.Delta.Y;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(Content), e.PlayerIndex);
        }


        /// <summary>
        /// When the user cancels the main menu, we exit the game.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
