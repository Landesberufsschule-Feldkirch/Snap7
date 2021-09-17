using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace AutomatischesLagersystem.DreiD
{
    public class DreiDErstellen
    {
        private readonly MainWindow _mainWindow;
        private readonly int[] _abstaendeSteher = { 50, 1050, 3050, 4050 };

        public DreiDErstellen(MainWindow mw)
        {
            _mainWindow = mw;

            _mainWindow.ViewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

            _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.System] = _mainWindow.ViewPort3d.Children.Count;

            #region Bodenplatte

            var bodenplatte3d = new ModelVisual3D
            {
                Content = Display3d("SolidWorks/Bodenplatte.STL", Colors.Beige),
                Transform = new TranslateTransform3D(-1000, 0, 0)
            };
            _mainWindow.ViewPort3d.Children.Add(bodenplatte3d);

            _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.Bodenplatte] = _mainWindow.ViewPort3d.Children.Count;

            #endregion Bodenplatte

            #region Streben, ...

            for (var x = 0; x < 11; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    var profilSteher = new ModelVisual3D { Content = Display3d("SolidWorks/ProfilSteher.STL", (Color)ColorConverter.ConvertFromString("#FFD3D3D3")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(1000 * x, _abstaendeSteher[y], 200));
                    profilSteher.Transform = verschiebenUndDrehen;

                    _mainWindow.ViewPort3d.Children.Add(profilSteher);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    //var querVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Querverstrebung.STL", (Color)ColorConverter.ConvertFromString("#FF808080")) };
                    var querVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/QuerverstrebungRohre.STL", (Color)ColorConverter.ConvertFromString("#FF808080")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 270)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(1040 + 1000 * x, 50 + y * 4000, 590));
                    querVerstrebung.Transform = verschiebenUndDrehen;

                    _mainWindow.ViewPort3d.Children.Add(querVerstrebung);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    var laengsVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Laengsverstrebung.STL", (Color)ColorConverter.ConvertFromString("#FF949494")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + 1000 * x, 0 + 4000 * y, 2900));
                    laengsVerstrebung.Transform = verschiebenUndDrehen;

                    _mainWindow.ViewPort3d.Children.Add(laengsVerstrebung);
                }
            }

            for (var x = 0; x < 11; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    for (var z = 0; z < 5; z++)
                    {
                        var profilQuerstrebe = new ModelVisual3D { Content = Display3d("SolidWorks/ProfilQuerstrebe.STL", (Color)ColorConverter.ConvertFromString("#FFBEBEBE")) };
                        var verschiebenUndDrehen = new Transform3DGroup();
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180)));
                        verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), -90)));
                        verschiebenUndDrehen.Children.Add(new TranslateTransform3D(200 + 1000 * x, 50 + 3000 * y, 600 + 500 * z));
                        profilQuerstrebe.Transform = verschiebenUndDrehen;

                        _mainWindow.ViewPort3d.Children.Add(profilQuerstrebe);
                    }
                }
            }

            _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.Streben] = _mainWindow.ViewPort3d.Children.Count;

            #endregion Streben, ...

            #region Regalbediengeraet

            _mainWindow.BediengeraetStartpositionen[0] = new DreiDElemente(-200, 2550, 50, 90, 0, 270);      //RegalBediengerät
            _mainWindow.BediengeraetStartpositionen[1] = new DreiDElemente(-1650, 2900, 400, 0, 0, 270);     //Schlitten senkrecht
            _mainWindow.BediengeraetStartpositionen[2] = new DreiDElemente(-1120, 1500, 490, 0, 180, 0);     //Schlitten waagrecht Zwischenteil
            _mainWindow.BediengeraetStartpositionen[3] = new DreiDElemente(-1110, 2500, 450, 90, 0, 270);    //Schlitten waagrecht

            _mainWindow.ViewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/RegalBediengeraet.STL", Colors.CadetBlue) });
            _mainWindow.ViewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenSenkrecht.STL", Colors.Violet) });
            _mainWindow.ViewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenWaagrechtZwischenteil.STL", Colors.Green) });
            _mainWindow.ViewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenWaagrecht.STL", Colors.MistyRose) });

            _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.Regalbediengeraet] = _mainWindow.ViewPort3d.Children.Count;

            #endregion Regalbediengeraet

            AlleKistenPositionenBerechnen();
            AlleKistenHinzufeugen();
        }

        internal void EineEinzigeKisteAufDemRegalbediengeraet()
        {
            AlleKistenEntfernen();
            EineEinzigeKisteHinzufuegen(0, Colors.MediumVioletRed);
            _mainWindow.KisteLiegtAufDemRegalbediengeraet = true;
        }

        private void AlleKistenPositionenBerechnen()
        {
            var i = 0;
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    for (var z = 0; z < 5; z++)
                    {
                        _mainWindow.KistenAktuellePositionen[i] = new DreiDKisten();
                        _mainWindow.KistenStartPositionen[i] = new DreiDElemente(125 + 1000 * x, 200 + 2850 * y, 600 + 500 * z, 90, 0, 90);
                        i++;
                    }
                }
            }
        }

        internal void AlleKistenHinzufeugen()
        {
            var kistenFarben = new[] { Colors.Blue, Colors.Red, Colors.Azure, Colors.BlueViolet, Colors.Chartreuse };

            for (var i = 0; i < 100; i++) { EineEinzigeKisteHinzufuegen(i, kistenFarben[i % 5]); }

            _mainWindow.KisteLiegtAufDemRegalbediengeraet = false;
            _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.Kisten] = _mainWindow.ViewPort3d.Children.Count;
        }

        internal void EineEinzigeKisteHinzufuegen(int i, Color farbe)
        {
            var kisteType1 = new ModelVisual3D
            {
                Content = Display3d("SolidWorks/Kiste_Type_1.STL", farbe),
                Transform = _mainWindow.KistenStartPositionen[i].Transform(0, 0, 0)
            };

            var label = new BillboardTextVisual3D
            {
                Text = "Kiste " + i.ToString("D2"),
                Position = new Point3D(100, 200, 300)
            };
            kisteType1.Children.Add(label);

            _mainWindow.ViewPort3d.Children.Add(kisteType1);
        }

        public void AlleKistenEntfernen()
        {
            do
            {
                _mainWindow.ViewPort3d.Children.RemoveAt(_mainWindow.ViewPort3d.Children.Count - 1);
            }
            while (_mainWindow.ViewPort3d.Children.Count > _mainWindow.DreiDModelleIds[ViewModel.IdEintraege.Regalbediengeraet]);
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