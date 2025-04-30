using System.ComponentModel;
using System.Drawing.Drawing2D;

/*

    Aetherx > Control > Textbox

    Textbox customization is absolutely horrible. So we're
    creating our own.

        >   borderColor
        >   borderSize
        >   underlineStyle

*/

namespace ReflectorKG.Controls;

[DefaultEvent("_TextChanged")]
public partial class AetherxTextBox : UserControl
{
    /*
        Fields
    */

    private Color borderColor = Color.MediumSlateBlue;
    private int   borderSize  = 1;
    private bool  isFocused;
    private bool  underlineStyle;

    /*
        Constructor
    */

    public AetherxTextBox()
    {
        InitializeComponent();
    }

    /*
        Properties > Border Color
    */

    [Category("Aetherx")]
    public Color BorderColor
    {
        get => borderColor;

        set
        {
            borderColor = value;
            Invalidate();
        }
    }

    /*
        Properties > Border Size
    */

    [Category("Aetherx")]
    public int BorderSize
    {
        get => borderSize;

        set
        {
            borderSize = value;
            Invalidate();
        }
    }

    /*
        Properties > Underline Style
    */

    [Category("Aetherx")]
    public bool UnderlineStyle
    {
        get => underlineStyle;

        set
        {
            underlineStyle = value;
            Invalidate();
        }
    }

    /*
        Properties > Password Char
    */

    [Category("Aetherx")]
    public bool PasswordChar
    {
        get => textBox1.UseSystemPasswordChar;
        set => textBox1.UseSystemPasswordChar = value;
    }

    /*
        Properties > Multiline
    */

    [Category("Aetherx")]
    public bool Multiline
    {
        get => textBox1.Multiline;

        set
        {
            textBox1.Multiline  = value;
            textBox1.ScrollBars = ScrollBars.Both;
            UpdateControlHeight();
        }
    }

    /*
        Properties > Readonly
    */

    [Category("Aetherx")]
    public bool ReadOnly
    {
        get => textBox1.ReadOnly;
        set => textBox1.ReadOnly = value;
    }

    /*
        Properties > Background Color
    */

    [Category("Aetherx")]
    public override Color BackColor
    {
        get => base.BackColor;
        set
        {
            base.BackColor     = value;
            textBox1.BackColor = value;
        }
    }

    /*
        Properties > Foreground Color
    */

    [Category("Aetherx")]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set
        {
            base.ForeColor     = value;
            textBox1.ForeColor = value;
        }
    }

    /*
        Properties > Font
    */

    [Category("Aetherx")]
    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font     = value;
            textBox1.Font = value;
            if (DesignMode)
                UpdateControlHeight();
        }
    }

    /*
        Properties > Value (replaces Text)
    */

    [Category("Aetherx")]
    public string Value
    {
        get => textBox1.Text;
        set => textBox1.Text = value;
    }

    /*
        Properties > Focus Border Color
    */

    [Category("Aetherx")]
    public Color BorderFocusColor { get; set; } = Color.HotPink;

    /*
        Events
    */

    public event EventHandler _TextChanged;

    /*
        Override Methods > onPaint
    */

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var graph = e.Graphics;

        // Border
        using (var penBorder = new Pen(borderColor, borderSize))
        {
            penBorder.Alignment = PenAlignment.Inset;

            if (!isFocused)
            {
                if (underlineStyle)
                    // underline
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                else
                    // normal style
                    graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
            }
            else
            {
                penBorder.Color = BorderFocusColor;

                if (underlineStyle)
                    // underline
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                else
                    // normal style
                    graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
            }
        }
    }

    /*
        Override Methods > onResize
    */

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (DesignMode)
            UpdateControlHeight();
    }

    /*
        Override Methods > onLoad
    */

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        UpdateControlHeight();
    }

    /*
        Override Methods > Update Control Height
    */

    private void UpdateControlHeight()
    {
        if (textBox1.Multiline == false)
        {
            var txtHeight = TextRenderer.MeasureText("Text", Font).Height + 1;
            textBox1.Multiline   = true;
            textBox1.MinimumSize = new Size(0, txtHeight);
            textBox1.Multiline   = false;

            Height = textBox1.Height + Padding.Top + Padding.Bottom;
        }
    }

    private void textBox1_TextChanged(object? sender, EventArgs e)
    {
        if (_TextChanged != null)
            _TextChanged.Invoke(sender, e);
    }

    private void textBox1_Click(object? sender, EventArgs e)
    {
        OnClick(e);
    }

    private void textBox1_MouseEnter(object? sender, EventArgs e)
    {
        OnMouseEnter(e);
    }

    private void textBox1_MouseLeave(object? sender, EventArgs e)
    {
        OnMouseLeave(e);
    }

    private void textBox1_KeyPress(object? sender, KeyPressEventArgs e)
    {
        OnKeyPress(e);
    }

    private void textBox1_Enter(object? sender, EventArgs e)
    {
        isFocused = true;
        Invalidate();
    }

    private void textBox1_Leave(object? sender, EventArgs e)
    {
        isFocused = false;
        Invalidate();
    }
}