﻿namespace WpfDesktopUI.Library.Models
{
    public class Spell : Card
    {

        public string Category { get; } = "SPELL";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Spell" };

        public Spell(string name, string desc, string icon) : base(name, desc)
        {
            Icon = icon;
        }
    }
}