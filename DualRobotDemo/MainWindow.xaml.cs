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
            // core.Connect(Model.CR15, "192.168.0.125", 60008);
            // core.Connect(Model.CR7, "192.168.0.124", 60008);

            // (2) Get Calibrated Co-Frame Data
            // % cal-2: current error: 0.2mm
            double[] Pos_Cr7_CalliBase = { 778.281, -38.520, -336.517, 0.810, 0.687, -92.389 };
            double[] Pos_Cr15_CalliBase = { 1252.171, 24.657, -770.478, 0.269, 0.423, 89.225 };
            core.RobotBaseCalibrationInit(Pos_Cr7_CalliBase, Pos_Cr15_CalliBase);

            // (3) Get Tool Antenna TCP Data 
            float[] origin_wpr_cr15 = { -17.024f, 87.760f, -17.928f };
            float[] default_wpr_cr15 = { -91.714f, 0.772f, -90.444f };

            var cal_wpr_cr15 = core.GetToolFixtureWPR(SceneName.Scene2_Sim, Model.CR15, origin_wpr_cr15, default_wpr_cr15);

            float[] cal_pin_tcp_cr15 = { -1.831f, 0.474f, 299.920f, cal_wpr_cr15[0], cal_wpr_cr15[1], cal_wpr_cr15[2] };
            float cal_pin_length_cr15 = 45.33f + 5.02f;
            var fixture_tcp_cr15 = core.GetToolFixtureTCP(Model.CR15, cal_pin_tcp_cr15, cal_pin_length_cr15);

            // offset
            float[] antenna_offset_cr15 = { 0.0f, 0.0f, 50.35f, 0.0f, 0.0f, 0.0f };
            var tcp_cr15 = core.GetToolAntennaTCP(Model.CR15, fixture_tcp_cr15, antenna_offset_cr15);

            // (4) Robot Initialization
            core.SetTCP(Model.CR15, tcp_cr15);
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // (5) Set Station Antenna TCP (Calibrated by Cr7 with Cal. tool) (Cal. tool + UF: 0)
            float[] station_cal_pin_tcp_cr7 = { 832.066f, -102.571f, 277.335f, 0.953f, 0.102f, -92.282f }; // switch to tool:5 user frame:0
            float station_cal_pin_length = 41.05f;
            var station_center_zero_tcp = core.GetStationCenterZeroTCP(station_cal_pin_tcp_cr7, station_cal_pin_length);

            float[] antenna_offset_station = { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f };
            float station_offset = 0; // 0-290

            var station_antenna_tcp_cr7 = core.GetStationAntennaTCP(station_center_zero_tcp, antenna_offset_station, station_offset);

            core.SetStationAntennaTCP_Cr7(station_antenna_tcp_cr7);


            // (6) Align LiftTable Height with station_center_zero_tcp
            double lift_table_align_error = 1.025;
            double stage34_fixture_height = 0;

            // (7) Scene Initialization
            // a. examples.
            double[] param = { 250, 180, 10, 180, 90, 140, lift_table_align_error, stage34_fixture_height };
            core.SceneParamInit(SceneName.Scene1A_Sim, param);
            // b.
            core.SceneRobotInit(SceneName.Scene1A_Sim);
            // c.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // (8) Execute Scene2_Sim

            // stage - 1
            core.Scene1A_Sim(MovementType.QuickCheck, MovementStage.One);

            // stage - 2
            core.SceneRobotInit(SceneName.Scene1A_Sim);
            core.Scene1A_Sim(MovementType.QuickCheck, MovementStage.Two);
        }  
    }
}
