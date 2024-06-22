using System.Drawing.Imaging;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Capture
{
    public partial class ScreenCapture : Form
    {
        private Rectangle _selectedArea;
        private string _sessionFolderPath;
        private int _counter = 0;
        private readonly string screenshotsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shots");
        public ScreenCapture()
        {
            _sessionFolderPath = string.Empty;
            _counter = 0;
            InitializeComponent();
            CreateScreenshotsFolder();
            this.TopMost = true;
            //SetStartPositionToBottomLeft();
            btn_ToWord.Enabled = false;
        }

        private void SetStartPositionToBottomLeft()
        {
            var screen = Screen.FromPoint(Cursor.Position);
            int x = screen.Bounds.X;
            int y = screen.Bounds.Bottom - this.Height - 30;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }
        private void btn_Capture_Click(object sender, EventArgs e)
        {
            if (!IsAreaSelected)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }
            btn_Capture.Enabled = false;
            if (string.IsNullOrEmpty(_sessionFolderPath))
            {
                CreateSessionFolder();
            }

            using (var bitmap = new Bitmap(_selectedArea.Width, _selectedArea.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(_selectedArea.X, _selectedArea.Y), Point.Empty, _selectedArea.Size);
                }
                var screenshotPath = Path.Combine(_sessionFolderPath, $"{DateTime.Now.ToString("yyMMddHHmmss")}.jpg");
                bitmap.Save(screenshotPath, ImageFormat.Jpeg);
                btn_Capture.Text = $"Capture ({(++_counter).ToString()})";
                btn_Capture.Enabled = true;
            }

        }

        private void btn_SelectArea_Click(object sender, EventArgs e)
        {
            using (var selectAreaForm = new SelectAreaForm())
            {
                if (selectAreaForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedArea = selectAreaForm.SelectedArea;
                    if (selectedArea != Rectangle.Empty && selectedArea.Width > 0 && selectedArea.Height > 0)
                    {
                        _selectedArea = selectedArea;
                        _counter = 0;
                        btn_Capture.Enabled = true;
                        btn_Capture.Text = "Capture";
                        btn_ToWord.Enabled = true;
                        CreateSessionFolder();
                    }
                }
            }
        }

        private void btn_ToWord_Click(object sender, EventArgs e)
        {
            CreateWordDocumentWithScreenshots(_sessionFolderPath);
        }
        private void CreateScreenshotsFolder()
        {
            if (!Directory.Exists(screenshotsFolderPath))
            {
                Directory.CreateDirectory(screenshotsFolderPath);
            }
        }

        private void CreateSessionFolder()
        {
            CreateScreenshotsFolder();
            var sessionFolderPath = Path.Combine(screenshotsFolderPath, DateTime.Now.ToString("yMM_dd_HHmmss"));
            Directory.CreateDirectory(sessionFolderPath);
            _sessionFolderPath = sessionFolderPath;
        }
        private bool IsAreaSelected => _selectedArea != Rectangle.Empty && _selectedArea.Width > 0 && _selectedArea.Height > 0;

        public void CreateWordDocumentWithScreenshots(string sessionFolderPath)
        {
            if (!Directory.Exists(sessionFolderPath))
            {
                MessageBox.Show($"The session folder '{sessionFolderPath}' does not exist.");
                return;
            }

            var imageFiles = Directory.GetFiles(sessionFolderPath, "*.jpg");

            if (imageFiles.Length == 0)
            {
                MessageBox.Show("No image files found in the session folder.");
                return;
            }

            using (var document = DocX.Create(Path.Combine(sessionFolderPath, "Screenshots.docx")))
            {
                document.InsertParagraph("Screenshots").FontSize(20).Bold().Alignment = Alignment.center;
                const float maxWidth = 450f; 
                const float maxHeight = 600f;
                foreach (var imageFile in imageFiles)
                {

                    using (var img = System.Drawing.Image.FromFile(imageFile))
                    {
     
                        var ratioX = maxWidth / img.Width;
                        var ratioY = maxHeight / img.Height;
                        var ratio = Math.Min(ratioX, ratioY);

                        var newWidth = (int)(img.Width * ratio);
                        var newHeight = (int)(img.Height * ratio);

                        var picture = document.AddImage(imageFile).CreatePicture(newHeight, newWidth);
                        var paragraph = document.InsertParagraph();
                        paragraph.AppendPicture(picture);
                        //paragraph.InsertParagraphBeforeSelf("Screenshot taken on: " + File.GetCreationTime(imageFile).ToString("G")).FontSize(14).Bold();
                        paragraph.AppendLine();
                    }

                    //var paragraph = document.InsertParagraph();
                    //paragraph.AppendPicture(document.AddImage(imageFile).CreatePicture());
                    ////paragraph.InsertParagraphBeforeSelf("Screenshot taken on: " + File.GetCreationTime(imageFile).ToString("G")).FontSize(14).Bold();
                    //paragraph.AppendLine();
                }
                document.Save();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
