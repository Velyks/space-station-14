﻿using GorgonLibrary;
using GorgonLibrary.Graphics;
using GorgonLibrary.InputDevices;
using SS14.Client.Interfaces.Resource;
using System;
using System.Drawing;

namespace SS14.Client.Services.UserInterface.Components
{
    internal class BlueprintButton : GuiComponent
    {
        #region Delegates

        public delegate void BlueprintButtonPressHandler(BlueprintButton sender);

        #endregion

        private readonly IResourceManager _resourceManager;

        public string Compo1;
        public string Compo1Name;

        public string Compo2;
        public string Compo2Name;
        public TextSprite Label;

        public string Result;
        public string ResultName;

        private Color _bgcol = Color.Transparent;
        private Sprite _icon;

        public BlueprintButton(string c1, string c1N, string c2, string c2N, string res, string resname,
                               IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            Compo1 = c1;
            Compo1Name = c1N;

            Compo2 = c2;
            Compo2Name = c2N;

            Result = res;
            ResultName = resname;

            _icon = _resourceManager.GetSprite("blueprint");

            Label = new TextSprite("blueprinttext", "", _resourceManager.GetFont("CALIBRI"))
                        {
                            Color = Color.GhostWhite,
                            ShadowColor = Color.DimGray,
                            ShadowOffset = new Vector2D(1, 1),
                            Shadowed = true
                        };

            Update(0);
        }

        public event BlueprintButtonPressHandler Clicked;

        public override void Update(float frameTime)
        {
            ClientArea = new Rectangle(Position,
                                       new Size((int) (Label.Width + _icon.Width),
                                                (int) Math.Max(Label.Height, _icon.Height)));
            Label.Position = new Point(Position.X + (int) _icon.Width, Position.Y);
            _icon.Position = new Vector2D(Position.X, Position.Y + (Label.Height/2f - _icon.Height/2f));
            Label.Text = Compo1Name + " + " + Compo2Name + " = " + ResultName;
        }

        public override void Render()
        {
            if (_bgcol != Color.Transparent)
                Gorgon.CurrentRenderTarget.FilledRectangle(ClientArea.X, ClientArea.Y, ClientArea.Width,
                                                           ClientArea.Height, _bgcol);
            _icon.Draw();
            Label.Draw();
        }

        public override void Dispose()
        {
            Label = null;
            _icon = null;
            Clicked = null;
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        public override bool MouseDown(MouseInputEventArgs e)
        {
            if (ClientArea.Contains(new Point((int) e.Position.X, (int) e.Position.Y)))
            {
                if (Clicked != null) Clicked(this);
                return true;
            }
            return false;
        }

        public override bool MouseUp(MouseInputEventArgs e)
        {
            return false;
        }

        public override void MouseMove(MouseInputEventArgs e)
        {
            _bgcol = ClientArea.Contains(new Point((int) e.Position.X, (int) e.Position.Y))
                         ? Color.SteelBlue
                         : Color.Transparent;
        }
    }
}