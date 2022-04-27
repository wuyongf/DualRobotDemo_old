using DualRobotLib;

namespace DualRobotDemo
{
    internal class DualRobot_ExampleCode
    {
        public void CR7_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            // core.Connect(Model.CR7, "192.168.3.124", 60008);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr7 = { -55, -140, 183, 0, 0, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR7, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR7);

            // 2.4.1 Set Single Point
            float[] pos_cr7 = { 50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7);

            // 2.4.2 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.3 Set Single Point
            float[] pos_cr7_2 = { -50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_2);

            // 2.4.4 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.5 Set Single Point
            float[] pos_cr7_3 = { 0, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_3);

            // 2.4.6 Move
            core.MoveSinglePoint(Model.CR7);
        }

        public void CR15_BasicMovement_Demo()
        {
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR7, "192.168.3.125", 60008);

            // 2. movement test

            // 2.1 define the tcp
            float[] tcp_cr7 = { -55, -140, 183, 0, 0, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 2.2 define the tcp speed
            core.SetSpeed(Model.CR7, 100);

            // 2.3 define the user frame
            core.SetUserFrame(Model.CR7);

            // 2.4.1 Set Single Point
            float[] pos_cr7 = { 50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7);

            // 2.4.2 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.3 Set Single Point
            float[] pos_cr7_2 = { -50, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_2);

            // 2.4.4 Move
            core.MoveSinglePoint(Model.CR7);

            // 2.4.5 Set Single Point
            float[] pos_cr7_3 = { 0, 0, 0, 0, 0, 0 };
            core.SetSinglePoint(Model.CR7, pos_cr7_3);

            // 2.4.6 Move
            core.MoveSinglePoint(Model.CR7);
        }

        public void DualRobot_Scene1B_Demo()
        {
            // 0. Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "192.168.3.125", 60008);
            core.Connect(Model.CR7, "192.168.3.124", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // 3. define the tcp - scene1B
            float[] tcp_cr15 = { 0, -702, 842, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);

            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // 6. Scene 1B
            core.Scene1B(130, 0.5, 180, 90, 180, 45);
        }

        public void DualRobot_Scene2_Demo()
        {
            // 0. Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // 3. define the tcp - Scene2
            float[] tcp_cr15 = {0,55,700,0,0,0};
            core.SetTCP(Model.CR15, tcp_cr15);
            
            float[] tcp_cr7 = {-55,-140,183,0,0,0};
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // 6. Scene 2
            core.Scene2(100,10,15,15);
        }

        public void DualRobot_Simulation_Demo()
        {
            // Initialize the Core Class
            DualRobotLib.Core core = new Core();

            // 1. establish connection
            core.Connect(Model.CR15, "127.0.0.1", 9021);
            core.Connect(Model.CR7, "127.0.0.1", 60008);

            // 2. reset robot movement
            core.ResetMovement(Model.CR15);
            core.ResetMovement(Model.CR7);

            // // 3. define the tcp (for scene2)
            // float[] tcp_cr15 = {0,55,700,0,0,0};
            // core.SetTCP(Model.CR15, tcp_cr15);
            //
            // float[] tcp_cr7 = {-55,-140,183,0,0,0};
            // core.SetTCP(Model.CR7, tcp_cr7);

            // 3. define the tcp (for scene1)
            float[] tcp_cr15 = { 0, -702, 842, 0, 45, -90 };
            core.SetTCP(Model.CR15, tcp_cr15);

            float[] tcp_cr7 = { 9, 0, 123, 0, -45, 0 };
            core.SetTCP(Model.CR7, tcp_cr7);

            // 4. define the tcp speed
            core.SetSpeed(Model.CR15, 100);
            core.SetSpeed(Model.CR7, 100);

            // 5. define the user frame, the current tcp position will be the origin.
            core.SetUserFrame(Model.CR15);
            core.SetUserFrame(Model.CR7);

            // // . move -- get move flag - cr15
            // Thread th1 = new Thread(() => core.thread_MoveFlag());
            // th1.Start();

            // 6. Scene 1
            core.Scene1B(130, 0.5, 180, 90, 180, 45);

            // 7. Scene 2
            // core.Scene2(100,10,15,15);

            // Get Methods
            //
            // // 1. get current position
            // core.GetCurPos(Model.CR15);
            // core.GetCurPos(Model.CR7);

            // // 2. get current move status
            // core.GetMoveFlag(Model.CR15);

            // // 3. get current speed
            // var cr15_speed = core.GetSpeed(Model.CR15);
            // var cr7_speed = core.GetSpeed(Model.CR7);

            // Set Methods
            //
            // // 1. set move status after 5g measurement
            // core.SetMoveFlag(Model.CR15, 3);

            // // 2. Set Single Point
            // float[] pos_cr15 = { 0, 0, 0, 0, 0, 0 };
            // core.SetSinglePoint(Model.CR15, pos_cr15);
            // core.MoveSinglePoint(Model.CR15);

        }
    }
}
