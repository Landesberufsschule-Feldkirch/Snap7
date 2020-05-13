using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace AutomatischesLagersystem._3D
{
    public class DreiDerstellen
    {

        public DreiDerstellen(HelixToolkit.Wpf.HelixViewport3D viewPort3d)
        {
            viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

            var bodenplatte3d = new ModelVisual3D
            {
                Content = Display3d("SolidWorks/Bodenplatte.STL", Colors.Beige, 0, 0, 0)
            };


            var bonsai3d = new ModelVisual3D
            {
                Content = Display3d("SolidWorks/Bonsai_Pot.STL", Colors.Red, 0, 0, 0)
            };

           

            viewPort3d.Children.Add(bodenplatte3d);  
            viewPort3d.Children.Add(bonsai3d); 
        }


        private Model3D Display3d(string model, Color farbe, double x, double y, double z)
        {
            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter
                {
                    DefaultMaterial = new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(farbe))
                };

                device = import.Load(model);
                device.Transform = new TranslateTransform3D(x, y, z);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}