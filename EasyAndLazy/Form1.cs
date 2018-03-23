using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EasyAndLazy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitEvent();
            Init();
        }

        #region 初始化
        public void Init()
        {
            //注册热键
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Alt, Keys.J);
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Alt, Keys.K);
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt, Keys.Q);
            HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.Alt, Keys.F);
            HotKey.RegisterHotKey(Handle, 104, HotKey.KeyModifiers.Alt, Keys.H);
            //窗体置顶层
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2);
//             var control = textEdit1.Controls[0] as TextBox;
//             control.Multiline = true;
//             control.WordWrap = true;

            textWide = HowMuchWord() - 5;
        }


        public void InitEvent()
        {
            Load += Form1_Load;
            btnClose.Click += BtnClose_Click;

            //鼠标点击事件
            simpleButton1.MouseDown += Form1_MouseDown;
            simpleButton1.MouseMove += Form1_MouseMove;
            simpleButton1.MouseUp += Form1_MouseUp;
        }
        
        #endregion
        #region 属性

        public string[] StoryText { get; set; }   //放置当前文本
        public StreamReader HoleReader { get; set; }    //文件流
        public int CurIndex { get; set; }   //当前阅读行
        private INIClass ini { get; set; }      //配置类
        private string section { get; set; }    //当前配置项

        //用于拖动窗口
        bool beginMove = false;//初始化鼠标位置  
        int currentXPosition;
        int currentYPosition;

        //记录当前文本框宽度
        private int textWide { get; set; }
        #endregion


        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(120, 100);
            var open = new OpenFileDialog{
                Filter = @"Files (*.txt)|*.txt"
            };
            if (open.ShowDialog() == DialogResult.OK)   //选择文件并加载文本到内存
            {
                LoadText(open.FileName);
            }
            else
            {
                MessageBox.Show("打开失败");
                return;
            }
            string inipath = Application.StartupPath + @"\indexConfig.ini";     //读取配置文件获取上次阅读行
            ini = new INIClass(inipath);
            section = Path.GetFileNameWithoutExtension(open.FileName);
            if (!ini.ExistINIFile())    //不存在ini文件则新建并初始化
            {
                FileStream filest = new FileStream(inipath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                filest.Flush();
                filest.Close();
                ini.IniWriteValue(section, "Path", open.FileName);
                ini.IniWriteValue(section, "ReadIndex", "1");
            }
            if (string.IsNullOrEmpty(ini.IniReadValue(section, "ReadIndex")))
            {
                ini.IniWriteValue(section, "Path", open.FileName);
                ini.IniWriteValue(section, "ReadIndex", "1");
            }
            CurIndex = Convert.ToInt32(ini.IniReadValue(section, "ReadIndex"));     //获取上次阅读行数
            if (CurIndex != 0)
            {
                textEdit1.EditValue = StoryText[CurIndex];
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            FormClose();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态  
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标  
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标  
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="filePath">加载路径</param>
        private void LoadText(string filePath)
        {
            StoryText = new string[100000];
            HoleReader = new StreamReader(filePath, Encoding.UTF8);     //读取文本
            string line = "";
            for (int i = 1; (line = HoleReader.ReadLine()) != null; i++)
            {
                int j = 1;
                if (line.Length > textWide) //换行
                {
                    int len = line.Length;
                    StoryText[i] = line.Substring(0, textWide);
                    while (len - j * textWide > 0)
                    {
                        int leftWord = len - j * textWide;
                        if (leftWord > textWide)
                        {
                            StoryText[i + j] = line.Substring(j * textWide, textWide);
                        }
                        else
                        {
                            StoryText[i + j] = line.Substring(j * textWide, leftWord);
                        }
                        j++;
                    }
                    i += j - 1;
                }
                else
                {
                    StoryText[i] = line;
                }
            }
        }

        private void FormClose()
        {
            HotKey.UnregisterHotKey(Handle, 100);//卸载第1个快捷键
            HotKey.UnregisterHotKey(Handle, 101); //卸载第2个快捷键
            HotKey.UnregisterHotKey(Handle, 102); //卸载第3个快捷键
            HotKey.UnregisterHotKey(Handle, 103); //卸载第4个快捷键
            HotKey.UnregisterHotKey(Handle, 104); //卸载第4个快捷键
            //记录当前阅读行数
            ini.IniWriteValue(section, "ReadIndex", CurIndex.ToString().Trim());
            Close();
        }
        //重写WndProc()方法，通过监视系统消息，来调用过程
        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32()){
                        case 100:    //按下的是ALTER+J
                            //if(StoryText[CurIndex + 1] == null) break;
                            CurIndex++;
                            textEdit1.EditValue = StoryText[CurIndex];
                            break;
                        case 101:    //按下的是ALTER+K
                            if (CurIndex != 0)
                            {
                                CurIndex--;
                                textEdit1.EditValue = StoryText[CurIndex];
                            }
                            break;
                        case 102:    //按下的是Alt+Q
                            FormClose();
                            break;
                        case 103:    //按下的是Alt+F
                            FormSearch form = new FormSearch();
                            form.StoryText = StoryText.ToList();
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                CurIndex = form.ChoosedIndex;
                                textEdit1.EditValue = StoryText[CurIndex];}
                            break;
                        case 104:    //按下的是Alt+H

                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        //计算当前文本框一行可以装多少个字
        private int HowMuchWord()
        {
            Graphics graphics = CreateGraphics();

            SizeF sizeF = graphics.MeasureString("文", new Font("宋体", 9));
            float wordNum = textEdit1.Width / sizeF.Width;
            return Convert.ToInt32(wordNum);
        }
        #endregion

        class HotKey
        {
            //如果函数执行成功，返回值不为0。
            //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,                //要定义热键的窗口的句柄
                int id,                     //定义热键ID（不能与其它ID重复）           
                KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
                Keys vk                     //定义热键的内容
            );

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(
                IntPtr hWnd,                //要取消热键的窗口的句柄
                int id                      //要取消热键的ID
            );

            //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
            [Flags()]
            public enum KeyModifiers
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                WindowsKey = 8
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary>
        /// 得到当前活动的窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    }
}