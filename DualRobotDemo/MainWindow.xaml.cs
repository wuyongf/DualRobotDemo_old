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

namespace DualRobotDemo
{
    /// <summary>
    /// Dual Robot Example Code
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // (1) Connection
            DualRobotLib.Core core = new Core();
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);
            // core.Connect(Model.CR15, "192.168.3.125", 60008);
            // core.Connect(Model.CR7, "192.168.3.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            double[] Pos_Cr7_CalliBase = { 825.25, 6.38, 134.67, -2.11, 46.69, -0.40 };
            double[] Pos_Cr15_CalliBase = { 1223.50, -1.77, -304.52, 1.61, 44.25, -177.78 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data
            float[] cal_pin_tcp_cr7 = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            float[] cal_pin_tcp_cr15 = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            float[] cal_pin_length_cr7 = { 0.0f, 0.0f, 450.0f, 0.0f, 0.0f, 0.0f };
            float[] cal_pin_length_cr15 = { 0.0f, 0.0f, 450.0f, 0.0f, 0.0f, 0.0f };
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 50.0f, 0.0f, 0.0f, 0.0f };
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 50.0f, 0.0f, 0.0f, 0.0f };
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);
            Console.WriteLine("tcp_cr7: " + tcp_cr7);
            Console.WriteLine("tcp_cr15: " + tcp_cr15);

            // (4) Robot Initialization
            // examples: tcp data
            //float[] tcp_cr7 = { -61.7107f, 0, 193.7107f, 0, -45, 0 };
            //float[] tcp_cr15 = { 0, 0, 140, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // todo: (5 - Optional) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 0.0f, 0.0f, 50.0f, 0.0f, 0.0f, 0.0f };
            float station_cal_pin_length = 50.0f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);


            float[] antenna_offset_station = { 0.0f, 0.0f, 50.0f, 0.0f, 0.0f, 0.0f };
            float station_offset = 300; // 0-300
            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);

            // (6) 
            // c. examples.
            double[] param = { 430, 100, 10, 250, 90, 45 };
            core.SceneParamInit(SceneName.Scene2, param);
            // d.
            core.SceneRobotInit(SceneName.Scene2);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // step5: Scene2
            core.Scene2(MovementType.QuickCheck);
        }

        void CancelScene(ref DualRobotLib.Core core)
        {
            Thread.Sleep(20000);

            core.CancelCurrentScene();

            Thread.Sleep(5000);

            double[] param = { 250, 299.9, 229.9, 14, 11, 0 };
            core.SceneParamInit(SceneName.Scene1C, param);
            // d.
            core.SceneRobotInit(SceneName.Scene1C);
            // e.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);
            
            core.Scene1C(MovementType.QuickCheck);
        }
    }
}
