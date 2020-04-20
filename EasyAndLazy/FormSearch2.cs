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
    public partial class FormSearch2 : Form
    {
        public FormSearch2()
        {
            InitializeComponent();
            InitEvent();
        }
        public void InitEvent()
        {
            Load += FormSearch2_Load; ;
            Paint += FormSearch2_Paint; ;
            gvSearch.DoubleClick += GvSearch_DoubleClick; ;
            LostFocus += FormSearch2_LostFocus; ;
            textSearch.KeyUp += TextSearch_KeyUp1; ;
        }

        private void TextSearch_KeyUp1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchList = new List<DataModel>();
                for (int i = 0; i < StoryText.Count; i++)
                {
                    if (StoryText[i] == null) continue;
                    if (StoryText[i].Contains(textSearch.Text))
                    {
                        SearchList.Add(new DataModel
                        {
                            Index = i,
                            TextString = StoryText[i]
                        });
                    }
                }
                gvSearch.DataSource = SearchList;
            }
        }

        private void FormSearch2_LostFocus(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void GvSearch_DoubleClick(object sender, EventArgs e)
        {
            var item = gvSearch.CurrentRow.DataBoundItem as DataModel;
            if (item != null)
            {
                ChoosedIndex = item.Index;
                DialogResult = DialogResult.OK;
            }
        }

        private void FormSearch2_Paint(object sender, PaintEventArgs e)
        {
            textSearch.Focus();
        }

        private void FormSearch2_Load(object sender, EventArgs e)
        {
            Location = new Point(120, 140);
            ActiveControl = textSearch;
        }

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
