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
            DualRobotLib.Core core = new Core();
            // core.Connect(Model.CR15, "127.0.0.1", 9021);
            // core.Connect(Model.CR7, "127.0.0.1", 60008);
            core.Connect(Model.CR15, "192.168.0.125", 60008);
            core.Connect(Model.CR7, "192.168.0.124", 60008);

            core.Connect(Model.LiftTable, "192.168.0.119", 50000, "COM4");
            core.Connect(Model.Motor, "COM3");

            core.MotorInit();
            core.LiftTableInit();

            // (2) Get Calibrated Co-Frame Data
            // % cal-2: current error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 
            float[] origin_wpr_cr7 = { -9.082f, 89.472f, -9.403f };
            float[] default_wpr_cr7 = { 89.529f, -1.760f, 88.218f };

            float[] origin_wpr_cr15 = { -17.024f, 87.760f, -17.928f };
            float[] default_wpr_cr15 = { -91.714f, 0.772f, -90.444f };

            var cal_wpr_cr7 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR7, origin_wpr_cr7, default_wpr_cr7);
            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr7 = { -120.609f, 180.878f, 134.808f, cal_wpr_cr7[0], cal_wpr_cr7[1], cal_wpr_cr7[2] };
            float[] cal_pin_tcp_cr15 = { -1.831f, 0.474f, 299.920f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr7 = 50.25f;
            float cal_pin_length_cr15 = 50.35f;
            var fixture_tcp_cr7 = core.GetToolFixtureTCP(Model.CR7, cal_pin_tcp_cr7, cal_pin_length_cr7);
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset-1: cr7: 6.27f;  cr15:6.24f;
            // offset-2: cr7: 16.05f; cr15: 16.03f;
            float[] antenna_offset_cr7 = { 0.0f, 0.0f, 50.25f, 0.0f, 0.0f, 0.0f };
            //float[] antenna_offset_cr15 = { 0.0f, 0.0f, 50.35f, 0.0f, 0.0f, 0.0f };//Scene 1A Testing Cr15 Cal Pin = 50.35
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 5f, 0.0f, 0.0f, 0.0f };//Scene 1A Testing Cr15 Cal Pin = 5.00
            var tcp_cr7 = core.GetToolAntennaTCP(Model.CR7, fixture_tcp_cr7, antenna_offset_cr7);
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);

            // (4) Robot Initialization
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetTCP(Model.CR7, tcp_cr7);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // (5) Set Station Antenna TCP (Cr7) (Cal. tool + UF: 0) *** need to update***
            float[] station_cal_pin_tcp_cr7 = { 828.640f, -106.108f, 148.415f, 0.612f, 0.673f, -92.367f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 50.25f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            //float[] antenna_offset_station = { 0.0f, 0.0f, 50.35f, 0.0f, 0.0f, 0.0f };//Scene 1A Testing Station Cal Pin = 50.35
            float[] antenna_offset_station = { 0.0f, 0.0f, 5.00f, 0.0f, 0.0f, 0.0f };//Scene 1A Testing Station Cal Plate = 5.00
            float station_offset = 0;

            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);
            Console.WriteLine("station_antenna_tcp_cr7: " + station_antenna_tcp_cr7);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);

            // (6) Align LiftTable Height with station_center_zero_tcp
            double lift_table_align_error = 1.025;
            double stage34_fixture_height = 0; // *** 127 ->0
            double antenna_height = antenna_offset_station[2];

            // (7) Scene Initialization
            // a. examples.
            //double[] param = { 160, 180, 10, 180, 90, 13, lift_table_align_error, stage34_fixture_height, antenna_height };//Scene 1A Testing without station fixture offset

            double[] param = { 140, 180, 10, 180, 90, 140, lift_table_align_error, stage34_fixture_height, antenna_height }; //Scene 1A Testing with station fixture offset
            core.SceneParamInit(SceneName.Scene1A, param);
            // b.
            core.SceneRobotInit(SceneName.Scene1A);
            // c.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // (8) Execute Scene1A
            //
            core.Scene1A(MovementType.QuickCheck, MovementStage.One);

            core.SceneRobotInit(SceneName.Scene1A);
            core.Scene1A(MovementType.QuickCheck, MovementStage.Two);
        }  
    }
}
