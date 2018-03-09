using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyAndLazy
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
            InitEvent();
        }
        public void InitEvent()
        {
            Load += FormSearch_Load;
            Paint += FormSearch_Paint;
            textSearch.EditValueChanged += TextSearch_EditValueChanged;
            gvSearch.DoubleClick += GvSearch_DoubleClick;
            LostFocus += FormSearch_LostFocus;
        }

        private void FormSearch_Paint(object sender, PaintEventArgs e)
        {
            textSearch.Focus();
        }

        private void FormSearch_LostFocus(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void GvSearch_DoubleClick(object sender, EventArgs e)
        {var item = gvSearch.GetFocusedRow() as DataModel;
            if (item != null)
            {
                ChoosedIndex = item.Index;
                DialogResult = DialogResult.OK;
            }
        }

        private void TextSearch_EditValueChanged(object sender, EventArgs e)
        {
            SearchList = new List<DataModel>();
            for (int i = 0; i < StoryText.Count; i++)
            {
                if(StoryText[i] == null) continue;
                if (StoryText[i].Contains(textSearch.Text))
                {
                    SearchList.Add(new DataModel
                    {
                        Index = i,
                        TextString = StoryText[i]
                    });
                }
            }
            gcSearch.DataSource = SearchList;
            gcSearch.RefreshDataSource();
        }
        private void FormSearch_Load(object sender, EventArgs e)
        {
            Location = new Point(120, 140);
            ActiveControl = textSearch;}

        public List<string> StoryText { get; set; }   //放置当前文本
        private List<DataModel> SearchList { get; set; }   //放置搜索结果文本

        public int ChoosedIndex { get; set; }   //选择的行
        

        class DataModel
        {
            public int Index { get; set; }  //行号
            public string TextString { get; set; }  //文本
        }
    }
}
