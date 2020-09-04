/*
==========================================================================
This file is part of Asterion Engine, an OpenGL/OpenTK 1-bit graphic
engine by @akaAgar (https://github.com/akaAgar/one-bit-of-engine)
Asterion Engine is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
Asterion Engine is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with Asterion Engine. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using Asterion.Core;
using Asterion.Input;
using Asterion.OpenGL;
using Asterion.UI.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Asterion.UI
{
    /// <summary>
    /// Describes an UI Page. Basically the Asterion.UI equivalent to a Windows Form.
    /// </summary>
    public class UIPage
    {
        /// <summary>
        /// (Private) A list of controls on this form.
        /// </summary>
        private List<UIControl> Controls = new List<UIControl>();

        /// <summary>
        /// The background tile to draw on this page where no controls appears.
        /// </summary>
        public int BackgroundTile { get { return BackgroundTile_; } set { BackgroundTile_ = value; UI.Invalidate(); } }
        private int BackgroundTile_ = 0;

        /// <summary>
        /// The color of the <see cref="BackgroundTile"/>.
        /// </summary>
        public RGBColor BackgroundColor { get { return BackgroundColor_; } set { BackgroundColor_ = value; UI.Invalidate(); } }
        private RGBColor BackgroundColor_ = RGBColor.Black;

        /// <summary>
        /// The tilemap from which to load the <see cref="BackgroundTile"/>.
        /// </summary>
        public int BackgroundTilemap { get { return BackgroundTilemap_; } set { BackgroundTilemap_ = value; UI.Invalidate(); } }
        private int BackgroundTilemap_ = 0;

        /// <summary>
        /// The <see cref="UIEnvironment"/> this UIPage belongs to.
        /// </summary>
        public UIEnvironment UI { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UIPage() { }

        /// <summary>
        /// (Private) Adds a new <see cref="UIControl"/> on the page.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="UIControl"/> to create</typeparam>
        /// <returns>The new <see cref="UIControl"/></returns>
        private T AddControl<T>(int x, int y, RGBColor color, int tilemap) where T : UIControl, new()
        {
            T control = new T();
            control.Initialize(this);
            Controls.Add(control);

            control.Position = new Position(x, y);
            control.Color = color;
            control.Tilemap = tilemap;

            UI.Invalidate();
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UILabel"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="text">Text</param>
        /// <param name="fontTile">The tile to use for this control's font. Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.</param>
        /// <param name="color">Color</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns>An UILabel control</returns>
        protected UILabel AddLabel(int x, int y, string text, int fontTile, RGBColor color, int tilemap = 0)
        {
            UILabel control = AddControl<UILabel>(x, y, color, tilemap);
            control.Text = text;
            control.FontTile = fontTile;
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UIInputBox"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="text">Text</param>
        /// <param name="fontTile">The tile to use for this control's font. Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.</param>
        /// <param name="color">Color</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns>An <see cref="UIInputBox"/> control</returns>
        protected UIInputBox AddInputBox(int x, int y, string text, int fontTile, RGBColor color, int tilemap = 0)
        {
            UIInputBox control = AddControl<UIInputBox>(x, y, color, tilemap);
            control.Text = text;
            control.FontTile = fontTile;
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UIFrame"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the frame</param>
        /// <param name="height">Height of the frame</param>
        /// <param name="frameTile">The tile to use for the frame. Frame tile must follow one another on the tilemap (but can be on multiple rows) in this order: upper-left corner, upper-right corner, lower-left corner, lower-right corner, top border, left border, bottom border, right border</param>
        /// <param name="color">Color</param>
        /// <param name="fillTile">The tile to use as a background for the frame, or null if none</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns>An UIFrame control</returns>
        protected UIFrame AddFrame(int x, int y, int width, int height, int frameTile, RGBColor color, int? fillTile = null, int tilemap = 0)
        {
            UIFrame control = AddControl<UIFrame>(x, y, color, tilemap);
            control.FrameTile = frameTile;
            control.Size = new Dimension(width, height);
            control.FillTile = fillTile;
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UIImage"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the frame</param>
        /// <param name="height">Height of the frame</param>
        /// <param name="tile">The tile to draw, or the upper-left tile to draw for composite images</param>
        /// <param name="color">Color</param>
        /// <param name="composite">Is this image a composite image, made multiple adjacent tiles on the tilemap?</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns>An <see cref="UIImage"/> control</returns>
        protected UIImage AddImage(int x, int y, int width, int height, int tile, RGBColor color, bool composite = false, int tilemap = 0)
        {
            UIImage control = AddControl<UIImage>(x, y, color, tilemap);
            control.Tile = tile;
            control.Size = new Dimension(width, height);
            control.Composite = composite;
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UITextBox"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the textbox</param>
        /// <param name="height">Height of the textbox</param>
        /// <param name="text">Text</param>
        /// <param name="fontTile">The tile to use for this control's font. Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.</param>
        /// <param name="color">Color</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns>An UITextBox control</returns>
        protected UITextBox AddTextBox(int x, int y, int width, int height, string text, int fontTile, RGBColor color, int tilemap = 0)
        {
            UITextBox control = AddControl<UITextBox>(x, y, color, tilemap);
            control.Size = new Dimension(width, height);
            control.FontTile = fontTile;
            control.Text = text;
            return control;
        }

        /// <summary>
        /// (Protected) Adds a new <see cref="UIMenu"/> control on the page.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="fontTile">The tile to use for this control's font. Font tiles must follow one another on the tilemap (but can be on multiple rows) and provide all the ASCII characters in the 32 (white space) to 126 (~) range.</param>
        /// <param name="color">Color</param>
        /// <param name="tilemap">Font tile tilemap</param>
        /// <returns></returns>
        protected UIMenu AddMenu(int x, int y, int fontTile, RGBColor color, int tilemap = 0)
        {
            UIMenu control = AddControl<UIMenu>(x, y, color, tilemap);
            control.FontTile = fontTile;
            return control;
        }

        /// <summary>
        /// (Protected) Removes a control from the page. The control will be destroyed and should not be used anymore after it has been removed.
        /// </summary>
        /// <param name="control">The control to remove.</param>
        protected void RemoveControl(UIControl control)
        {
            if (!Controls.Contains(control)) return;
            Controls.Remove(control);
            control.Destroy();
            UI.Invalidate();
        }

        /// <summary>
        /// (Internal) Called just after the page the page is created. Calls the overridable method <see cref="OnInitialize(object[])"/>.
        /// </summary>
        /// <param name="ui">The <see cref="UIEnvironment"/> this page belongs to</param>
        /// <param name="parameters">A series of parameters to pass to <see cref="OnInitialize(object[])"/></param>
        internal void Initialize(UIEnvironment ui, object[] parameters)
        {
            UI = ui;
            OnInitialize(parameters);
            UI.Invalidate();
        }

        /// <summary>
        /// (Protected) Called just after the page is created. Put your control creation logic here.
        /// UIPage.OnInitialize does nothing, so there's not need to call base.OnInitialize.
        /// </summary>
        /// <param name="parameters"></param>
        protected virtual void OnInitialize(object[] parameters) {  }

        /// <summary>
        /// (Protected) Called whenever an input event is while when this page is displayed.
        /// UIPage.OnKeyDown does nothing, so there's not need to call base.OnKeyDown.
        /// </summary>
        /// <param name="key">The key or gamepad button that raised the event</param>
        /// <param name="modifiers">Which modifier keys are down?</param>
        /// <param name="gamepadIndex">Index of the gamepad that raised the event, if the key is a gamepad button, or -1 if it was a keyboard key</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        protected virtual void OnInputEvent(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat) { }

        /// <summary>
        /// (Internal) Called whenever an input event is raised while this page is displayed.
        /// </summary>
        /// <param name="key">The key or gamepad button that raised the event</param>
        /// <param name="modifiers">Which modifier keys are down?</param>
        /// <param name="gamepadIndex">Index of the gamepad that raised the event, if the key is a gamepad button, or -1 if it was a keyboard key</param>
        /// <param name="isRepeat">Is this a "repeated key press" event, automatically generated while the used holds the key down?</param>
        internal void OnInputEventInternal(KeyCode key, ModifierKeys modifiers, int gamepadIndex, bool isRepeat)
        {
            foreach (UIControl c in Controls)
            {
                if (!c.InputEnabled) continue;
                if ((gamepadIndex >= 0) && (!c.AllowedGamepads.Contains(gamepadIndex))) continue;
                c.OnInputEvent(key, modifiers, gamepadIndex, isRepeat);
            }

            OnInputEvent(key, modifiers, gamepadIndex, isRepeat);
        }

        /// <summary>
        /// (Internal) Draws the tiles of all controls on this page.
        /// </summary>
        /// <param name="vbo">The UI VBO on which the tiles must be drawn</param>
        internal void DrawTiles(VBO vbo)
        {
            int x, y;
            for (x = 0; x < vbo.Columns; x++)
                for (y = 0; y < vbo.Rows; y++)
                    vbo.UpdateTileData(x, y, BackgroundTile_, BackgroundColor_, BackgroundTilemap_);
            
            Controls = Controls.OrderBy(c => c.ZOrder).ToList(); // Order controls by Z-Order

            foreach (UIControl control in Controls)
                control.UpdateVBOTiles(vbo);
        }

        /// <summary>
        /// (Protected) Called just before the page is closed.
        /// UIPage.OnClose does nothing, so there's not need to call base.OnClose.
        /// </summary>
        protected virtual void OnClose() { }

        /// <summary>
        /// (Internal) Called just before the page is closed.
        /// </summary>
        internal void Destroy()
        {
            OnClose();
        }
    }
}
