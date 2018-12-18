using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace raidbot
{
    public partial class Form1 : Form
    {
        DiscordSocketClient client = new DiscordSocketClient();
        private ControlWriter _writer;
        ulong gid;
        string token;
        int done;
        int error;
        int liczba;
        public void logit(String text)
        {
            DateTime dt = DateTime.Now;

            string Timeonly = dt.ToLongTimeString();
            Console.WriteLine(Timeonly + " "+text);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
           
        }

        public Form1()
        {
            InitializeComponent();
            _writer = new ControlWriter(listBox1);
            Console.SetOut(_writer);
            

        }
        public async void init(string token)
        {
            

            client.Log += Log;
            client.MessageReceived += MessageReceived;
            client.Ready += whenconn;



            try
            {
                if (radioButton1.Checked == true)
                {
                    await client.LoginAsync(TokenType.Bot, token);
                }
                else
                {
                    await client.LoginAsync(TokenType.User, token);
                }
                await client.StartAsync();
                
                
                
            }
            catch (Exception)
            {
                logit("Wystąpił błąd podczas łaczenia z botem. (Kod błędu: 01)");
            }

            
            


            // Block this task until the program is closed.
            await Task.Delay(-1);

        }

        private async Task whenconn()
        {
            try
            {

                
                gid = Convert.ToUInt64(textBox2.Text);
                
            }
            catch (Exception) { logit("Wystąpił błąd podczas łączenia z serwerem. (Kod błędu: 03)");  await client.StopAsync(); }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            token = textBox1.Text;
            //label9.Text = client.GetGuild(gid).Users.Count.ToString();
            init(textBox1.Text);
        }

        private async void button8_Click(object sender, EventArgs e)
                {
            try
            {
                logit("Rozpoczynam wysyłanie na DM...");
                button8.Visible = false;
                done = 0;
                liczba = 0;
                liczba = client.GetGuild(gid).Users.Count - 1;
                label9.Text = liczba.ToString();
                progressBar1.Maximum = liczba;
                progressBar1.Visible = true;
            }
            catch (Exception)
            {
                logit("wystapil blad");
            }
                for (int i = 0; i < client.GetGuild(gid).Users.Count; i++)
                    {
                        try
                        {
                            var u = client.GetGuild(gid).Users.ElementAt(i);
                           await u.SendMessageAsync(richTextBox2.Text);
                    logit("Wysłano wiadomość do "+u.Username);
                    done++;
                    label10.Text = done.ToString();
                    progressBar1.Value = done;
                        }
                        catch (Exception)
                        {
                    error++;
                    label11.ToString();
                        }
                int br = Convert.ToInt32(numericUpDown1.Value);
                await Task.Delay(br);
            }
                    logit("Zakończono wysyłanie.");
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            button8.Visible = true;

                }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/doteq");

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/xJMpT2q");
        }
    }
}
