using System;
using System.Windows.Forms;

namespace OneBitOfEngine.Tools.Forms
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == ArchiveCreatorButton)
                ShowToolForm<ArchiveCreatorForm>();
            else if (sender == TileMapEnumCreatorButton)
                ShowToolForm<TilemapEnumCreatorForm>();
            else if (sender == TilemapAnimationMakerButton)
                ShowToolForm<TilemapAnimationMakerForm>();
        }

        private void ShowToolForm<T>() where T: Form, new()
        {
            using (T form = new T())
            {
                form.ShowDialog();
            }
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
