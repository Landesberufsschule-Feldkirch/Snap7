namespace Tiefgarage
{
    public partial class MainWindow
    {
        public const double B1_Pos_1 = 200.0;
        public const double B1_Pos_2 = 240.0;
        public const double B2_Pos_1 = 280.0;
        public const double B2_Pos_2 = 320.0;

        public void LichtschrankenStatusBerechnen(double y_Pos, Rolle rolle)
        {
            if ((y_Pos < B1_Pos_1) || (y_Pos > B2_Pos_2))
            {
                Pegel_B1 = true;
                Pegel_B2 = true;
            }
            else
            {
                switch (rolle)
                {
                    case Rolle.Auto:
                        if ((y_Pos > B1_Pos_1) && (y_Pos < B1_Pos_2))
                        {
                            Pegel_B1 = false;
                            Pegel_B2 = true;
                        }
                        if ((y_Pos > B1_Pos_2) && (y_Pos < B2_Pos_1))
                        {
                            Pegel_B1 = false;
                            Pegel_B2 = false;
                        }
                        if ((y_Pos > B2_Pos_1) && (y_Pos < B2_Pos_2))
                        {
                            Pegel_B1 = true;
                            Pegel_B2 = false;
                        }
                        break;

                    case Rolle.Person:
                        if ((y_Pos > B1_Pos_1) && (y_Pos < B1_Pos_2))
                        {
                            Pegel_B1 = false;
                            Pegel_B2 = true;
                        }
                        if ((y_Pos > B1_Pos_2) && (y_Pos < B2_Pos_1))
                        {
                            Pegel_B1 = true;
                            Pegel_B2 = true;
                        }
                        if ((y_Pos > B2_Pos_1) && (y_Pos < B2_Pos_2))
                        {
                            Pegel_B1 = true;
                            Pegel_B2 = false;
                        }
                        break;

                    default:
                        Pegel_B1 = true;
                        Pegel_B2 = true;
                        break;
                }
            }


        }

    }
}