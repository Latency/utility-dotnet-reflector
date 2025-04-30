using ReflectorKG.Forms;

namespace ReflectorKG.Classes;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormParent());
    }
}