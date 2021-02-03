using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Stress
    {
        private Sec Sec;
        private ElmForces Moment;
       
        public Stress(Sec Sec, ElmForces Moment)
        {
            this.Sec = Sec;
            this.Moment = Moment;
        }



        public string Element
        {
            get { return Moment.Element; }

        }

        public int Node
        {
            get { return Moment.Node; }

        }

        public double Station
        {
            get { return Moment.Station;  }
        }

        // Stress due to only Steel
        public double S1_top
        {
            get { return -Moment.DC1 / Sec.SU1 * 1000000; }
        }

        public double S1_bot
        {
            get { return Moment.DC1 / Sec.SL1 * 1000000; }
        }

        // Stress due to Bottom concrete
        public double S2_top
        {
            get { return -Moment.DC2 / Sec.SU1 * 1000000; }
        }

        public double S2_bot
        {
            get { return Moment.DC2 / Sec.SL1 * 1000000; }
        }

        public double S3_top_short
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.DC3 / Sec.SU1 * 1000000 : -Moment.DC3 / Sec.SU2s * 1000000;
            }
        }

        public double S3_bot_short
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.DC3 / Sec.SL1 * 1000000 : Moment.DC3 / Sec.SL2s * 1000000;
            }
        }

        public double S3_top_long
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.DC3 / Sec.SU1 * 1000000 : -Moment.DC3 / Sec.SU2l * 1000000;
            }
        }

        public double S3_bot_long
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.DC3 / Sec.SL1 * 1000000 : Moment.DC3 / Sec.SL2l * 1000000;
            }
        }



        // Stress due to DC4 (longtime)

        public double S4_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.DC4 / Sec.SU3l * 1000000 : -Moment.DC4 / Sec.SU4l * 1000000;
            }
        }

        public double S4_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.DC4 / Sec.SL3l * 1000000 : Moment.DC4 / Sec.SL4l * 1000000;
            }
        }

        // Stress due to DW (longtime)

        public double Sw_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.DW / Sec.SU3l * 1000000 : -Moment.DW / Sec.SU4l * 1000000;
            }
        }

        public double Sw_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.DW / Sec.SL3l * 1000000 : Moment.DW / Sec.SL4l * 1000000;
            }
        }


        // Stress due to LL (Short time)
        public double Slmax_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.LLmax / Sec.SU3s * 1000000 : -Moment.LLmax / Sec.SU4s * 1000000;
            }
        }

        public double Slmax_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.LLmax / Sec.SL3s * 1000000 : Moment.LLmax / Sec.SL4s * 1000000;
            }
        }

        public double Slmin_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.LLmin / Sec.SU3s * 1000000 : -Moment.LLmin / Sec.SU4s * 1000000;
            }
        }

        public double Slmin_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.LLmin / Sec.SL3s * 1000000 : Moment.LLmin / Sec.SL4s * 1000000;
            }
        }




        //Constructibility
        public double Sc_top
        {
            get { return 1.25 * (S1_top + S2_top + S3_top_short); }
        }

        public double Sc_bot
        {
            get { return 1.25 * (S1_bot + S2_bot + S3_bot_short) ; }
        }

        //Ultimate limit state
        public double Su_top
        {
            get { return 1.25 * (S1_top + S2_top + S3_top_long + S4_top) + 1.5 * Sw_top + 1.8 * (Moment.DC1 >= 0 ? Slmax_top : Slmin_top); }
        }

        public double Su_bot
        {
            get { return 1.25 * (S1_bot + S2_bot + S3_bot_long + S4_bot) + 1.5 * Sw_bot + 1.8 * (Moment.DC1 >= 0 ? Slmax_bot : Slmin_bot); }
        }

        //Service I limit state

        public double Ss1_top
        {
            get { return 1.00 * (S1_top + S2_top + S3_top_long + S4_top) + 1.0 * Sw_top + 1.0 * (Moment.DC1 >= 0 ? Slmax_top : Slmin_top); }
        }

        public double Ss1_bot
        {
            get { return 1.00 * (S1_bot + S2_bot + S3_bot_long + S4_bot) + 1.0 * Sw_bot + 1.0 * (Moment.DC1 >= 0 ? Slmax_bot : Slmin_bot); }
        }


        //Service II limit state

        public double Ss2_top
        {
            get { return 1.00 * (S1_top + S2_top + S3_top_long + S4_top) + 1.0 * Sw_top + 1.3 * (Moment.DC1 >= 0 ? Slmax_top : Slmin_top); }
        }

        public double Ss2_bot
        {
            get { return 1.00 * (S1_bot + S2_bot + S3_bot_long + S4_bot) + 1.0 * Sw_bot + 1.3 * (Moment.DC1 >= 0 ? Slmax_bot : Slmin_bot); }
        }

        ////Fatigue limit state (for Max and Min liveload)
        public double Sfmax_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.LLfmax / Sec.SU3s  * 1000000 : -Moment.LLfmax / Sec.SU4s  * 1000000;
            }
        }

        public double Sfmax_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.LLfmax / Sec.SL3s  * 1000000 : Moment.LLfmax / Sec.SL4s  * 1000000;
            }
        }

        public double Sfmin_top
        {
            get
            {
                return Moment.DC1 >= 0 ? -Moment.LLfmin / Sec.SU3s  * 1000000 : -Moment.LLfmin / Sec.SU4s  * 1000000;
            }
        }

        public double Sfmin_bot
        {
            get
            {
                return Moment.DC1 >= 0 ? Moment.LLfmin / Sec.SL3s  * 1000000 : Moment.LLfmin / Sec.SL4s  * 1000000;
            }
        }

        public double Sf_top
        {
            get { return (Moment.DC1 >= 0 ? Sfmax_top : Sfmin_top) * 0.75; }
        }

        public double Sf_bot
        {
            get { return (Moment.DC1 >= 0 ? Sfmax_bot : Sfmin_bot) * 0.75; }
        }


    }
}
