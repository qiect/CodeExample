using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace AutoClick
{
    public partial class Form1 : Form
    {
        private Timer clickTimer = new Timer();
        private DateTime targetTime;
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
            clickTimer.Tick += new EventHandler(ClickTimer_Tick);
        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            // 模拟鼠标点击事件
            const int MOUSEEVENTF_LEFTDOWN = 0x02;
            const int MOUSEEVENTF_LEFTUP = 0x04;
            Cursor.Position = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y); // 移动一下鼠标位置
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            }
            clickTimer.Stop();
            toggleButton.Text = "启动";
            isRunning = false;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private void toggleButton_Click_1(object sender, EventArgs e)
        {
            if (isRunning)
            {
                // 停止定时器
                clickTimer.Stop();
                toggleButton.Text = "启动";
            }
            else
            {
                // 启动定时器
                targetTime = dateTimePicker1.Value;
                TimeSpan timeToGo = targetTime - NetworkTimeHelper.GetNetworkTime();
                if (timeToGo.TotalMilliseconds > 0)
                {
                    clickTimer.Interval = (int)timeToGo.TotalMilliseconds;
                    clickTimer.Start();
                    toggleButton.Text = "停止";
                }
            }
            isRunning = !isRunning;
        }
    }
}
