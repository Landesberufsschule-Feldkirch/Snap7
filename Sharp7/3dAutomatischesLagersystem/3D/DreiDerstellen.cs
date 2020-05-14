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
            var abstaendeSteher = new int[] { -50, -1050, -3050, -4050 };

            viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

            #region Bodenplatte
            var bodenplatte3d = new ModelVisual3D { Content = Display3d("SolidWorks/Bodenplatte.STL", Colors.Beige) };
            bodenplatte3d.Transform = new TranslateTransform3D(-1000, 0, 0);
            viewPort3d.Children.Add(bodenplatte3d);
            #endregion

            #region Streben, ...
            for (var x = 0; x < 11; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    var profilSteher = new ModelVisual3D { Content = Display3d("SolidWorks/ProfilSteher.STL", Colors.Green) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(1000 * x, 200, abstaendeSteher[y]));
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    profilSteher.Transform = verschiebenUndDrehen;

                    viewPort3d.Children.Add(profilSteher);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    var querVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Querverstrebung.STL", Colors.YellowGreen) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + 1000 * x, 350, -50 - y * 4000));
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    querVerstrebung.Transform = verschiebenUndDrehen;

                    viewPort3d.Children.Add(querVerstrebung);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    var laengsVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Laengsverstrebung.STL", Colors.Yellow) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(-2700, 0 + y * 4000, 50 + x * 1000));
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
                    laengsVerstrebung.Transform = verschiebenUndDrehen;

                    viewPort3d.Children.Add(laengsVerstrebung);
                }
            }

            for (var x = 0; x < 2; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    for (var z = 0; z < 11; z++)
                    {
                        var profilQuerstrebe = new ModelVisual3D { Content = Display3d("SolidWorks/ProfilQuerstrebe.STL", Colors.Orange) };
                        var verschiebenUndDrehen = new Transform3DGroup();
                        verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + 3000 * x, 350 + 500 * y, -150 + 1000 * z));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
                        profilQuerstrebe.Transform = verschiebenUndDrehen;

                        viewPort3d.Children.Add(profilQuerstrebe);
                    }
                }
            }
            #endregion

            #region Kisten
            var kistenFarben = new Color[] { Colors.Blue, Colors.Red, Colors.Azure, Colors.BlueViolet, Colors.Chartreuse }; 

            for (var x = 0; x < 2; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    for (var z = 0; z < 10; z++)
                    {
                        var kiste_Type_1 = new ModelVisual3D { Content = Display3d("SolidWorks/Kiste_Type_1.STL", kistenFarben[y]) };
                        var verschiebenUndDrehen = new Transform3DGroup();
                        verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + 3000 * x, 400 + 500 * y, 125 + 1000 * z));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90)));
                        kiste_Type_1.Transform = verschiebenUndDrehen;

                        viewPort3d.Children.Add(kiste_Type_1);
                    }
                }
            }

            #endregion


            #region Regalbediengeraet
            var regalBediengeraet = new ModelVisual3D { Content = Display3d("SolidWorks/RegalBediengeraet.STL", Colors.CadetBlue) };
            var transformRegalBediengeraet = new Transform3DGroup();
            transformRegalBediengeraet.Children.Add(new TranslateTransform3D(-2550, 50, -200));
            transformRegalBediengeraet.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
            transformRegalBediengeraet.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 270)));
            regalBediengeraet.Transform = transformRegalBediengeraet;
            viewPort3d.Children.Add(regalBediengeraet);
            #endregion


        }

        private Model3D Display3d(string model, Color farbe)
        {
            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter
                {
                    DefaultMaterial = new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(farbe))
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