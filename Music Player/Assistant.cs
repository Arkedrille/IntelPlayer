using System.IO;
using System.Windows.Forms;

namespace Music_Player {
    public class Assistant {
        /// <summary>
        /// objet de la classe Serialisation
        /// </summary>
		Serialisation serialisation = new Serialisation();

        /// <summary>
        /// affiche le titre de la chanson
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="label"></param>
		public void SetMaintenantJoueText(ListBox listbox, Label label) {
            var playing = Path.GetFileName(listbox.SelectedItem.ToString());
            label.Text = "Maintenant joue :    { playing }";
        }

        /// <summary>
        /// Bouton previous et next sont actives
        /// </summary>
        /// <param name="listbox"></param>
        /// <param name="nextBtn"></param>
        /// <param name="previousBtn"></param>
        public void previousNextEnabled(ListBox listbox, Button nextBtn, Button previousBtn) {
            if (listbox.SelectedIndex == (listbox.Items.Count - 1)) {
                nextBtn.Enabled = false;
                previousBtn.Enabled = true;
            } else if (listbox.SelectedIndex == 0) {
                previousBtn.Enabled = false;
                nextBtn.Enabled = true;
            } else {
                nextBtn.Enabled = true;
                previousBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Active les boutons pause et stop + label
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="label"></param>
        /// <param name="pauseBtn"></param>
        /// <param name="stopBtn"></param>
        public void setButtons(bool enabled, Label label, Button pauseBtn, Button stopBtn) {
            if (enabled) {
                label.Visible = false;
                pauseBtn.Enabled = false;
                stopBtn.Enabled = false;
            } else {
                label.Visible = true;
                pauseBtn.Enabled = true;
                stopBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Ajoute la musique dee l'openFile dans la liste
        /// </summary>
        /// <param name="playlist"></param>
		public void addMusic(ListBox playlist) {
			var dlg = new OpenFileDialog();
			dlg.Filter = "Music (*.mp3) | *.mp3";
			dlg.Multiselect = true;
			var result = dlg.ShowDialog();

			if (result == DialogResult.OK) {
				try {
					foreach (var file in dlg.FileNames) {
						serialisation.SetFilenames(playlist, file);
					}
				} catch {
					MessageBox.Show("Could not add file");
				}
			}
		}

        /// <summary>
        /// Boite de dialogue pour sauvegarder la playlist
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		public DialogResult showSaveDialog(string filename) {
			var dialog = new SaveFileDialog();
			dialog.Filter = "Data File (*.dat, *.play) | *.dat, .play";
			var result = dialog.ShowDialog();

			if (result == DialogResult.OK) {
				filename = dialog.FileName;
			}

			return result;
		}
	}
}
