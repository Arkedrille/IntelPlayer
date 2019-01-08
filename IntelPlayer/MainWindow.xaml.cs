using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace WpfTutorialSamples.Audio_and_Video
{
    public partial class MediaPlayerAudioControlSample : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        //instanciation et utilisation du timer ici
        public MediaPlayerAudioControlSample()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        Window playlist;
        public void MainWindow()
        {
            playlist = new Window();
            playlist.Show();
        }

        /// <summary>
        /// Affiche un timer avec le temps passant en secondes/minutes & le max secondes/minutes de la chanson
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
                lblStatus.Content = string.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            else
                lblStatus.Content = "Veuillez ouvrir un fichier .mp3";
        }

        /// <summary>
        /// Bouton "Jouer" qui joue la musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        /// <summary>
        /// Bouton "Pause" qui joue la musique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        /// <summary>
        /// Bouton "Stop" qui remet à zero la musique et l'arrête
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        /// <summary>
        /// Bouton "ouvrir" pour ajouter des morceaux
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            //Création d'un objet openFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Filtre pour n'avoir que des mp3
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            string mediaPath = (listBox.SelectedValue).ToString();
            mediaPlayer.Open(new Uri(mediaPath));

            if (openFileDialog.ShowDialog() == true)
            
            mediaPlayer.Open(new Uri(openFileDialog.FileName));

            string fileN = Path.GetFileName(openFileDialog.FileName);
            listBox.Items.Add(fileN);
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
            string mediaPath = (listBox.SelectedValue).ToString();
            mediaPlayer.Open(new Uri(mediaPath));
            mediaPlayer.Play();
        }

        /// <summary>
        /// utilisateur peut avec sa souris placer ses fichiers TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        /// <summary>
        /// les fichiers sont récupérés et stockés dans la listbox TODO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_DragDrop(object sender, DragEventArgs e)
        {
            if (listBox.Items.Count != 0)
            {
                listBox.Items.Clear();
            }
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                listBox.Items.Add(Path.GetFileName(s[i]));
        }

        private void listBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.MainWindow();
        }
    }
}