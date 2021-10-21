using System;
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
            

            _mainWindow.ViewPort3d.Camera = new PerspectiveCamera
            {
                Position = new Point3D(3, 3, 5),
                LookDirection = new Vector3D(-3, -3, -5),
                UpDirection = new Vector3D(0, 1, 0),
                FarPlaneDistance = 5000000
            };

            var garagentorStationaer = new ModelVisual3D
            {
                Content = Display3d("DreiDModelle/garagentorStationaer.STL", Colors.OrangeRed)
            };
            _mainWindow.ViewPort3d.Children.Add(garagentorStationaer);

            var garagentorBeweglich = new ModelVisual3D
            {
                Content = Display3d("DreiDModelle/garagentorBeweglich.STL", Colors.YellowGreen)
            };
            _mainWindow.ViewPort3d.Children.Add(garagentorBeweglich);

            var volvoLkw3d = new ModelVisual3D
            {
                Content = Display3d("DreiDModelle/volvoLkw.STL", Colors.CadetBlue)
            };
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