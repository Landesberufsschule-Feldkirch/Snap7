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
        private readonly MainWindow mainWindow;
        readonly int[] abstaendeSteher = new int[] { 50, 1050, 3050, 4050 };
        public DreiDErstellen(MainWindow mw)
        {
            mainWindow = mw;

            mainWindow.viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

            mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.System] = mainWindow.viewPort3d.Children.Count;

            #region Bodenplatte
            var bodenplatte3d = new ModelVisual3D { Content = Display3d("SolidWorks/Bodenplatte.STL", Colors.Beige) };
            bodenplatte3d.Transform = new TranslateTransform3D(-1000, 0, 0);
            mainWindow.viewPort3d.Children.Add(bodenplatte3d);

            mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.Bodenplatte] = mainWindow.viewPort3d.Children.Count;
            #endregion

            #region Streben, ...
            for (var x = 0; x < 11; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    var profilSteher = new ModelVisual3D { Content = Display3d("SolidWorks/ProfilSteher.STL", (Color)ColorConverter.ConvertFromString("#FFD3D3D3")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(1000 * x, abstaendeSteher[y], 200));
                    profilSteher.Transform = verschiebenUndDrehen;

                    mainWindow.viewPort3d.Children.Add(profilSteher);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    var querVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Querverstrebung.STL", (Color)ColorConverter.ConvertFromString("#FF808080")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + 1000 * x, 50 + y * 4000, 550));
                    querVerstrebung.Transform = verschiebenUndDrehen;

                    mainWindow.viewPort3d.Children.Add(querVerstrebung);
                }
            }

            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    var laengsVerstrebung = new ModelVisual3D { Content = Display3d("SolidWorks/Laengsverstrebung.STL", (Color)ColorConverter.ConvertFromString("#FF949494")) };
                    var verschiebenUndDrehen = new Transform3DGroup();
                    verschiebenUndDrehen.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90)));
                    verschiebenUndDrehen.Children.Add(new TranslateTransform3D(50 + x * 1000, 0 + y * 4000, 2900));
                    laengsVerstrebung.Transform = verschiebenUndDrehen;

                    mainWindow.viewPort3d.Children.Add(laengsVerstrebung);
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

                        mainWindow.viewPort3d.Children.Add(profilQuerstrebe);
                    }
                }
            }

            mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.Streben] = mainWindow.viewPort3d.Children.Count;
            #endregion

            #region Regalbediengeraet
            mainWindow.BediengeraetStartpositionen[0] = new DreiDElemente(-200, 2550, 50, 90, 0, 270);      //RegalBediengerät
            mainWindow.BediengeraetStartpositionen[1] = new DreiDElemente(-1650, 2900, 400, 0, 0, 270);     //Schlitten senkrecht
            mainWindow.BediengeraetStartpositionen[2] = new DreiDElemente(-1120, 1500, 490, 0, 180, 0);     //Schlitten waagrecht Zwischenteil
            mainWindow.BediengeraetStartpositionen[3] = new DreiDElemente(-1110, 2500, 450, 90, 0, 270);    //Schlitten waagrecht

            mainWindow.viewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/RegalBediengeraet.STL", Colors.CadetBlue) });
            mainWindow.viewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenSenkrecht.STL", Colors.Violet) });
            mainWindow.viewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenWaagrechtZwischenteil.STL", Colors.Green) });
            mainWindow.viewPort3d.Children.Add(new ModelVisual3D { Content = Display3d("SolidWorks/SchlittenWaagrecht.STL", Colors.MistyRose) });

            mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.Regalbediengeraet] = mainWindow.viewPort3d.Children.Count;
            #endregion

            AlleKistenPositionenBerechnen();
            AlleKistenHinzufeugen();
        }

        internal void EineEinzigeKisteAufDemRegalbediengeraet()
        {
            AlleKistenEntfernen();
            EineEinzigeKisteHinzufuegen(0, Colors.MediumVioletRed);
            mainWindow.KisteLiegtAufDemRegalbediengeraet = true;
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
                        mainWindow.KistenAktuellePositionen[i] = new DreiDKisten();
                        mainWindow.KistenStartPositionen[i] = new DreiDElemente(125 + 1000 * x, 200 + 2850 * y, 600 + 500 * z, 90, 0, 90);
                        i++;
                    }
                }
            }
        }

        internal void AlleKistenHinzufeugen()
        {
            var kistenFarben = new Color[] { Colors.Blue, Colors.Red, Colors.Azure, Colors.BlueViolet, Colors.Chartreuse };

            for (var i = 0; i < 100; i++) { EineEinzigeKisteHinzufuegen(i, kistenFarben[i % 5]); }

            mainWindow.KisteLiegtAufDemRegalbediengeraet = false;
            mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.Kisten] = mainWindow.viewPort3d.Children.Count;
        }

        internal void EineEinzigeKisteHinzufuegen(int i, System.Windows.Media.Color farbe)
        {
            var kiste_Type_1 = new ModelVisual3D { Content = Display3d("SolidWorks/Kiste_Type_1.STL", farbe) };
            kiste_Type_1.Transform = mainWindow.KistenStartPositionen[i].Transform(0, 0, 0);

            BillboardTextVisual3D label = new BillboardTextVisual3D
            {
                Text = "Kiste " + i.ToString("D2"),
                Position = new Point3D(100, 200, 300)
            };
            kiste_Type_1.Children.Add(label);

            mainWindow.viewPort3d.Children.Add(kiste_Type_1);
        }

        public void AlleKistenEntfernen()
        {
            do
            {
                mainWindow.viewPort3d.Children.RemoveAt(mainWindow.viewPort3d.Children.Count - 1);
            }
            while (mainWindow.viewPort3d.Children.Count > mainWindow.DreiDModelleIds[ViewModel.VisuAnzeigen.IdEintraege.Regalbediengeraet]);
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