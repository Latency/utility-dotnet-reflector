using ReflectorKG.Classes;

namespace ReflectorKG.Forms;

public partial class FormAbout : Form
{
    private Point lastLocation;

    /*
         Main Form > Mouse Down
         deals with moving form around on screen
     */

    private bool mouseDown;

    public FormAbout()
    {
        InitializeComponent();
        var ver     = AppInfo.Version;
        var product = AppInfo.Title;
        var tm      = AppInfo.Trademark;

        txt_Version.Text = $@"v{ver} by {tm}";
        lbl_Product.Text = product;

        var file_name = "readme.md";
        var textLine  = "";

        if (File.Exists(file_name))
        {
            var objReader = new StreamReader(file_name);

            do
            {
                textLine = $"{textLine}{objReader.ReadLine()}\r\n";
            }
            while (objReader.Peek() != -1);

            objReader.Close();
        }
        else
        {
            MessageBox.Show($@"No such file {file_name}");
        }

        txt_Terms.Value = textLine;
        txt_Terms.Text  = textLine;
    }

    private void FormAbout_Load(object sender, EventArgs e)
    { }

    private void FormAbout_MouseDown(object sender, MouseEventArgs e)
    {
        mouseDown    = true;
        lastLocation = e.Location;
    }

    /*
        Main Form > Mouse Up
        deals with moving form around on screen
    */

    private void FormAbout_MouseUp(object sender, MouseEventArgs e) => mouseDown = false;

    /*
        Main Form > Mouse Move
        deals with moving form around on screen
    */

    private void FormAbout_MouseMove(object sender, MouseEventArgs e)
    {
        if (!mouseDown)
            return;

        Location = new Point(Location.X - lastLocation.X + e.X, Location.Y - lastLocation.Y + e.Y);
        Update();
    }

    /*
        Window > Button > Close
    */

    private void btn_Window_Close_Click(object sender, EventArgs e) => Close();

    /*
        Window > Button > Close > Mouse Enter
    */

    private void btn_Window_Close_MouseEnter(object sender, EventArgs e) => closeBtn.ForeColor = Color.FromArgb(222, 31, 100);

    /*
        Window > Button > Close > Mouse Leave
    */

    private void btn_Window_Close_MouseLeave(object sender, EventArgs e) => closeBtn.ForeColor = Color.FromArgb(255, 255, 255);

    private void lbl_Serial_Click(object sender, EventArgs e)
    { }

    /*
        Window > Logo
    */

    private void mat_Logo_Click(object sender, EventArgs e)
    { }

    /*
        Window > Button > Close
    */

    private void llblLink_TPB_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://thepiratebay.org/search.php?q=user:Aetherinox");

    private void panel1_Paint(object sender, PaintEventArgs e)
    { }

    private void lbl_Product_Click(object sender, EventArgs e)
    { }

    private void txt_Terms_Load(object sender, EventArgs e)
    { }

    private void lnk_Github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/Aetherinox/NetReflectorKeygen");
}