﻿using Asterion.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public class MenuLabel : MenuControl
    {
        public string Text { get; set; } = "";

        public int MaxLength { get; set; } = 0;

        internal override void Render()
        {
            if (string.IsNullOrEmpty(Text)) return;

            string realText = Text;
            if (MaxLength > 0) realText = Text.Substring(0, Math.Min(realText.Length, MaxLength));

            byte[] textBytes = Encoding.ASCII.GetBytes(realText);

            for (int i = 0; i < textBytes.Length; i++)
            {
                if ((textBytes[i] < 32) || (textBytes[i] > 126)) textBytes[i] = 32;

                Tile charTile = new Tile(Tile + textBytes[i] - 32, Color, Tilemap);

                Page.Menus.DrawTile(Position.X + i, Position.Y, charTile);
            }
        }
    }
}