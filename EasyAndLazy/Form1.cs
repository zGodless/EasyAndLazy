using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            if (keyList == null)
            {
                keyList = new List<MyKey>();
            }

            keyList.AddRange(new[]
            {
                //上一页
                new MyKey{ id = 101,  keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.K, op = operateMode.prePage},
                new MyKey{ id = 1011, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.I, op = operateMode.prePage },
                new MyKey{ id = 1012, keyA = HotKey.KeyModifiers.Alt, keyB = Keys.I, op = operateMode.prePage },
                new MyKey{ id = 1013, keyA = HotKey.KeyModifiers.Alt, keyB = Keys.S, op = operateMode.prePage },
                //下一页
                new MyKey{ id = 100,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.J, op = operateMode.nextPage },
                new MyKey{ id = 1001, keyA = HotKey.KeyModifiers.Alt, keyB = Keys.D, op = operateMode.nextPage },
                new MyKey{ id = 1002, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.J, op = operateMode.nextPage },
                new MyKey{ id = 1003, keyA = HotKey.KeyModifiers.None, keyB = Keys.MButton, op = operateMode.nextPage },
                new MyKey{ id = 1004, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.LButton, op = operateMode.nextPage },
                //关闭
                new MyKey{ id = 102,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.Q, op = operateMode.close },
                new MyKey{ id = 1021, keyA = HotKey.KeyModifiers.Alt, keyB = Keys.O, op = operateMode.close },
                new MyKey{ id = 1022, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.O, op = operateMode.close },
                //搜索
                new MyKey{ id = 103,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.F, op = operateMode.search },
                //透明度
                new MyKey{ id = 105,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.Up, op = operateMode.addOpacity },
                new MyKey{ id = 1051,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.E, op = operateMode.addOpacity },
                new MyKey{ id = 106,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.Down, op = operateMode.reduceOpacity },
                new MyKey{ id = 106,  keyA = HotKey.KeyModifiers.Alt, keyB = Keys.W, op = operateMode.reduceOpacity },
                //窗体移动
                new MyKey{ id = 1071,  keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.Up, op = operateMode.formUp },
                new MyKey{ id = 1072, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.Down, op = operateMode.formDown },
                new MyKey{ id = 1073, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.Left, op = operateMode.formLeft },
                new MyKey{ id = 1074, keyA = HotKey.KeyModifiers.Ctrl, keyB = Keys.Right, op = operateMode.formRight }

            });
            //注册热键
            keyList.ForEach(m => HotKey.RegisterHotKey(Handle, m.id, m.keyA, m.keyB));

            //窗体置顶层
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2);

            textWide = HowMuchWord() - 5;

        }


        public void InitEvent()
        {
            Load += Form1_Load;

            //鼠标点击事件
            simpleButton1.MouseDown += Form1_MouseDown;
            simpleButton1.MouseMove += Form1_MouseMove;
            simpleButton1.MouseUp += Form1_MouseUp;
            textEdit1.DoubleClick += TextEdit1_DoubleClick;

            MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)    //鼠标滚轮向下滚
            {
                CurIndex++;
                textEdit1.Text = StoryText[CurIndex];
            }
            else    //鼠标滚轮向上滚
            {
                CurIndex--;
                textEdit1.Text = StoryText[CurIndex];
            }
        }

        #endregion
        #region 属性

        public string[] StoryText { get; set; }   //放置当前文本
        public StreamReader HoleReader { get; set; }    //文件流
        public int CurIndex { get; set; }   //当前阅读行
        private INIClass ini { get; set; }      //配置类
        private string section { get; set; }    //当前配置项
        private DateTime timeTag = DateTime.Now;     //上一次快捷键输入时间
        private int increaseRatio = 1;     //位移增量

        //用于拖动窗口
        bool beginMove = false;//初始化鼠标位置  
        int currentXPosition;
        int currentYPosition;

        //记录当前文本框宽度
        private int textWide { get; set; }

        //热键集合
        List<MyKey> keyList = new List<MyKey>();

        //搜索窗体
        public FormSearch2 form1;
        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(50, 1020);
            var open = new OpenFileDialog
            {
                Filter = @"Files (*.txt)|*.txt"
            };
            if (open.ShowDialog() == DialogResult.OK)   //选择文件并加载文本到内存
            {
                LoadText(open.FileName);
                WindowState = FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show("打开失败");
                Environment.Exit(0);
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
                ini.IniWriteValue(section, "Opacity", "0.4");
            }
            if (string.IsNullOrEmpty(ini.IniReadValue(section, "ReadIndex")))
            {
                ini.IniWriteValue(section, "ReadIndex", "1");
            }
            if (string.IsNullOrEmpty(ini.IniReadValue(section, "Opacity")))
            {
                ini.IniWriteValue(section, "Opacity", "0.4");
            }
            CurIndex = Convert.ToInt32(ini.IniReadValue(section, "ReadIndex"));     //获取上次阅读行数
            if (CurIndex != 0)
            {
                textEdit1.Text = StoryText[CurIndex];
            }
            Opacity = Convert.ToDouble(ini.IniReadValue(section, "Opacity"));     //获取上次透明度
        }


        private void TextEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (!textBox.Visible)
            {
                Height += textBox.Height;
                textBox.Visible = true;
                textBox.Top = textEdit1.Top + textEdit1.Height;
                textBox.Text = StoryText[CurIndex];
            }
            else
            {
                Height -= textBox.Height;
                textBox.Visible = false;
                textBox.Text = "";
            }
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
                Console.WriteLine(MousePosition.X.ToString() + "; "+ MousePosition.Y.ToString() + "; " + currentXPosition.ToString() + "; " + currentYPosition);
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e )
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
            StoryText = new string[3000000];
            Encoding ed = EncodingType.GetType(filePath);
            HoleReader = new StreamReader(filePath, ed);     //读取文本
            string line = "";
            for (int i = 1; (line = HoleReader.ReadLine()) != null; i++)
            {
                if (line == "")
                {
                    i--;
                    continue;
                }
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
            //卸载快捷键
            keyList.ForEach(m => HotKey.UnregisterHotKey(Handle, m.id));

            //记录当前阅读行数
            ini.IniWriteValue(section, "ReadIndex", CurIndex.ToString().Trim());
            //记录当前透明度
            ini.IniWriteValue(section, "Opacity", Opacity.ToString());
            Close();
        }
        //重写WndProc()方法，通过监视系统消息，来调用过程
        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_HOTKEY = 0x0312;
            const int WM_MBUTTONDOWN = 0x0207;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    int id = m.WParam.ToInt32();
                    operateMode op = keyList.Find(k => k.id == id) == null ? operateMode.notExist : keyList.Find(k => k.id == id).op;
                    switch (op)
                    {
                        case operateMode.notExist: break;
                        //上一页
                        case operateMode.prePage:
                            if (CurIndex != 0)
                            {
                                CurIndex--;
                                textEdit1.Text = StoryText[CurIndex];
                            }
                            break;
                        //下一页
                        case operateMode.nextPage:
                            CurIndex++;
                            textEdit1.Text = StoryText[CurIndex];
                            break;
                        //关闭
                        case operateMode.close:
                            FormClose();
                            break;
                        //搜索
                        case operateMode.search:
                            if (form1 == null || form1.IsDisposed)
                            {
                                form1 = new FormSearch2();
                                form1.StoryText = StoryText.ToList();
                                if (form1.ShowDialog() == DialogResult.OK)
                                {
                                    CurIndex = form1.ChoosedIndex;
                                    textEdit1.Text = StoryText[CurIndex];
                                }
                            }
                            else
                            {
                                form1.Dispose();
                                form1.Close();//已打开，关闭
                            }
                            break;
                        //减透明度
                        case operateMode.reduceOpacity:
                            if (Opacity > 0.01f)
                            {
                                Opacity -= 0.01;
                            }
                            break;
                        //加透明度
                        case operateMode.addOpacity:
                            if (Opacity < 0.99f)
                            {
                                Opacity += 0.01;
                            }
                            break;
                        //窗体移动
                        case operateMode.formUp:
                            checkIncrease(0, false);
                            break;
                        case operateMode.formDown:
                            checkIncrease(0, true);
                            break;
                        case operateMode.formLeft:
                            checkIncrease(1, false);
                            break;
                        case operateMode.formRight:
                            checkIncrease(1, true);
                            break;

                    }
                    break;
                case WM_MBUTTONDOWN:
                    if (CurIndex != 0)
                    {
                        CurIndex--;
                        textEdit1.Text = StoryText[CurIndex];
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        //窗体移动渐增
        private void checkIncrease(int positionType, bool addOrnot)
        {
            RECT currentRect = new RECT();
            GetWindowRect(Handle, ref currentRect);
            int increaseNum;
            if (DateTime.Now.Ticks - timeTag.Ticks < 500000)
            {
                if (increaseRatio >= 10)
                {
                    increaseNum = (addOrnot ? (increaseRatio / 5) : (-1 * (increaseRatio / 5)));
                }
                else
                {
                    increaseNum = (addOrnot ? 1 : -1);
                }
                increaseRatio++;
            }
            else
            {
                increaseNum = (addOrnot ? 1 : -1);
                increaseRatio = 0;
            }
            if(positionType == 0)
            {
                Top += increaseNum;
            }
            else
            {
                Left += increaseNum;
            }
            timeTag = DateTime.Now;
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
            [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,                //要定义热键的窗口的句柄
                int id,                     //定义热键ID（不能与其它ID重复）           
                KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
                Keys vk                     //定义热键的内容
            );

            [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
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

        class MyKey
        {
            public int id { get; set; }
            public HotKey.KeyModifiers keyA { get; set; }
            public Keys keyB { get; set; }
            public operateMode op { get; set; }
            public MyKey()
            {

            }
        }

        /// <summary>
        /// 操作模式枚举
        /// </summary>
        enum operateMode
        {
            [Description("上一页")]
            notExist = -1,
            [Description("上一页")]
            prePage = 1,
            [Description("下一页")]
            nextPage = 2,
            [Description("关闭")]
            close = 3,
            [Description("搜索")]
            search = 4,
            [Description("减透明度")]
            reduceOpacity = 5,
            [Description("加透明度")]
            addOpacity = 6,
            [Description("窗体拖动：上")]
            formUp = 7,
            [Description("窗体拖动：下")]
            formDown = 8,
            [Description("窗体拖动：左")]
            formLeft = 9,
            [Description("窗体拖动：右")]
            formRight = 10

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

    }
}