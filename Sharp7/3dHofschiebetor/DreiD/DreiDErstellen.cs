﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace _3dHofschiebetor.DreiD
{
    public class DreiDErstellen
    {
        private readonly MainWindow _mainWindow;

        public DreiDErstellen(MainWindow mw)
        {
            _mainWindow = mw;

            _mainWindow.ViewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            
            var garagentorStationaer = new ModelVisual3D { Content = Display3d("DreiDModelle/garagentorStationaer.STL", Colors.Brown) };
            _mainWindow.ViewPort3d.Children.Add(garagentorStationaer);

            var garagentorBeweglich = new ModelVisual3D { Content = Display3d("DreiDModelle/garagentorBeweglich.STL", Colors.YellowGreen) };
            _mainWindow.ViewPort3d.Children.Add(garagentorBeweglich);

            var volvoLkw3d = new ModelVisual3D {Content = Display3d("DreiDModelle/volvoLkw.STL", Colors.CadetBlue)};
            _mainWindow.ViewPort3d.Children.Add(volvoLkw3d);
        }

        private Model3D Display3d(string model, Color farbe)
        {
            Model3D device = null;
            try
            {
                var import = new ModelImporter
                {
                    DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(farbe))
                };

                device = import.Load(model);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}