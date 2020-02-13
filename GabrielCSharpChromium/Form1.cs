using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace GabrielCSharpChromium
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser chrome;
        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings cefSettings = new CefSettings();
            CefSettings settings = cefSettings;
            //initialization
            Cef.Initialize(settings);
            txtURL.Text = "https://start.duckduckgo.com";
            chrome = new ChromiumWebBrowser(txtURL.Text);
            this.pContainer.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
            chrome.AddressChanged += Chrome_AddressChanged;
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                txtURL.Text = e.Address;
            }));
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }
        //This is the core function that will perform all navigation and post processing.
        private void NavigateToPage()
        {
            chrome.Load(txtURL.Text);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chrome.Refresh();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoForward)
                chrome.Forward();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoBack)
                chrome.Back();
        }

        private void txtURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
                NavigateToPage();
            //chrome.Load(txtURL.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        
    }
}
