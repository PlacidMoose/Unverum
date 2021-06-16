﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

namespace Unverum
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public Mod _mod;
        public string directory = null;
        public EditWindow(Mod mod)
        {
            InitializeComponent();
            if (mod != null)
            {
                _mod = mod;
                NameBox.Text = _mod.name;
                Title = $"Edit {_mod.name}";
            }
            else
                Title = $"Create Name of New Mod for {Global.config.CurrentGame}";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mod != null)
                EditName();
            else
                CreateName();
        }
        private void CreateName()
        {
            var newDirectory = $"{Global.assemblyLocation}{Global.s}Mods{Global.s}{Global.config.CurrentGame}{Global.s}{NameBox.Text}";
            if (!Directory.Exists(newDirectory))
            {
                directory = newDirectory;
                Close();
            }
            else
                Global.logger.WriteLine($"{newDirectory} already exists", LoggerType.Error);
        }
        private void EditName()
        {
            if (!NameBox.Text.Equals(_mod.name, StringComparison.InvariantCultureIgnoreCase))
            {
                var oldDirectory = $"{Global.assemblyLocation}{Global.s}Mods{Global.s}{Global.config.CurrentGame}{Global.s}{_mod.name}";
                var newDirectory = $"{Global.assemblyLocation}{Global.s}Mods{Global.s}{Global.config.CurrentGame}{Global.s}{NameBox.Text}";
                if (!Directory.Exists(newDirectory))
                {
                    try
                    {
                        Directory.Move(oldDirectory, newDirectory);
                    }
                    catch (Exception ex)
                    {
                        Global.logger.WriteLine($"Couldn't rename {oldDirectory} to {newDirectory} ({ex.Message})", LoggerType.Error);
                    }
                }
                else
                    Global.logger.WriteLine($"{newDirectory} already exists", LoggerType.Error);
            }
            Close();
        }
    }
}
