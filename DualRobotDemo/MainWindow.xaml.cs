using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DualRobotLib;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace DualRobotDemo
{
    /// <summary>
    /// Dual Robot Example Code
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

        }

        public void ListAllTCPs(ref DualRobotLib.Core core)
        {
            var cal_pos = core.GetRobotBaseCalibrationPose(); // relation between cr7 and cr15. formula: T_cr15 = T_cal * T_cr7
            var lttt_tcp = core.GetStationAntennaTCP_Cr7(); // record in cr7 base coordinate
            var cr7_tcp = core.GetCurPos(Model.CR7);        // record in cr7 base coordinate
            var cr15_tcp = core.GetCurPos(Model.CR15);      // record in cr15 base coordinate

            Console.WriteLine("cal_pos = " + string.Join(",", cal_pos));
            Console.WriteLine("lttt_tcp = " + string.Join(",", lttt_tcp));
            Console.WriteLine("cr7_tcp = " + string.Join(",", cr7_tcp));
            Console.WriteLine("cr15_tcp = " + string.Join(",", cr15_tcp));

            
            var dis1 = core.GetTcpDistance(SceneName.Scene1A_Sim, Model.LiftTable, Model.CR15); // get distance between lttt_tcp and cr15_tcp
            var dis2 = core.GetTcpDistance(SceneName.Scene1A_Sim, Model.LiftTable, Model.CR7);  // get distance between lttt_tcp and cr7_tcp
            var dis3 = core.GetTcpDistance(SceneName.Scene1A_Sim, Model.CR15, Model.CR7);       // get distance between cr15_tcp and cr7_tcp

            Console.WriteLine("distance between lttt_tcp and cr15_tcp: " + dis1);
            Console.WriteLine("distance between lttt_tcp and cr7_tcp: " + dis2);
            Console.WriteLine("distance between cr15_tcp and cr7_tcp: " + dis3);
        }
    }
}
