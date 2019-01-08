using System;
using System.Windows.Forms;

namespace Music_Player {
    public partial class interfaceForm : Form {

        // Objets
        MusicPlayer player = new MusicPlayer();
        Serialisation serialisation = new Serialisation();
        Assistant helper = new Assistant();

        // Variables
        private Timer timer;

        private string filename = "";

        public interfaceForm() {
            InitializeComponent();
        }

        private void addMusicBtn_Click(object sender, EventArgs e) {
			helper.addMusic(playlist);
        }

        private void playBtn_Click(object sender, EventArgs e) {
            try {
                if (playlist.SelectedIndex == -1) {
                    if (playlist.Items.Count >= 1) {
                        playlist.SelectedIndex = 0;
                    }
                }

                helper.setButtons(false, label1, pauseBtn, stopBtn);
                helper.SetMaintenantJoueText(playlist, label1);

                var item = playlist.SelectedItem as ListBoxItem;
				TagLib.File tagFile = TagLib.File.Create(item.Path);
				albumLabel.Text = tagFile.Tag.Album;

				player.open(item.Path);
                player.play();
                helper.previousNextEnabled(playlist, nextBtn, previousBtn);
            } catch {
                helper.setButtons(true, label1, pauseBtn, stopBtn);
                MessageBox.Show("Not a valid mp3 file!");
            }
        }

        private void stopBtn_Click(object sender, EventArgs e) {
            player.stop();
            label1.Visible = false;
            pauseBtn.Enabled = false;
        }

        private void pauseBtn_Click(object sender, EventArgs e) {
            player.pause();
        }

        private void nextBtn_Click(object sender, EventArgs e) {
            try {
                player.stop();
                playlist.SelectedIndex += 1;
                playBtn.PerformClick();
            } catch {
                MessageBox.Show("No more songs in list");
            }
        }

        private void previousBtn_Click(object sender, EventArgs e) {
            try {
                player.stop();
                playlist.SelectedIndex -= 1;
                playBtn.PerformClick();
            } catch {
                MessageBox.Show("No more songs in list!");
            }
        }
		
        private void saveBtn_Click(object sender, EventArgs e) {
            if (helper.showSaveDialog(filename) != DialogResult.OK) {
                return;
            }

            serialisation.SavePlaylist(playlist, filename);
        }

        /// <summary>
        /// Ouvre l explorateur de fichier pour charger une playlist existante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadBtn_Click(object sender, EventArgs e) {
            serialisation.OpenPlaylist(playlist);
        }

        /// <summary>
        /// Timer todo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(this.TimerTick);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e) {
            //TODO
        }

        /// <summary>
        /// Au double click : Lecture ou stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e) {
            stopBtn.PerformClick();
            playBtn.PerformClick();
        }

        /// <summary>
        /// Supprimer la playlist courante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearPlaylist_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("Etes-vous sur de vouloir supprimer la playlist courante ?", "", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK) {
                player.stop();
                playlist.Items.Clear();
                helper.setButtons(false, label1, pauseBtn, stopBtn);
            }
        }
	}
}