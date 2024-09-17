using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Viewer
{
    public partial class Form1 : Form
    {
        private int SelectedImageIndex = 0;
        private List<Image> LoadedImages { get; set; }
        public Form1()
        {
            InitializeComponent();
        }


        private void LoadImagesFromFolder(string[] paths)
        {
            LoadedImages = new List<Image>();
            foreach(var path in paths)
            { 
                var tempImage = Image.FromFile(path);
                LoadedImages.Add(tempImage);
            }
        }

        private void imageList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (imageList.SelectedIndices.Count > 0)
            {
                var selectedIndex = imageList.SelectedIndices[0];
                Image selectedImg = LoadedImages[selectedIndex];
                selectedImage.Image = selectedImg;
                SelectedImageIndex = selectedIndex;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                var selectedDirectory = folderBrowser.SelectedPath;
                var imagePaths = Directory.GetFiles(selectedDirectory);
                LoadImagesFromFolder (imagePaths);
                ImageList images =new ImageList();
                images.ImageSize = new Size(130, 40);
                foreach (var image in LoadedImages)
                {
                    images.Images.Add (image);
                }
                imageList.LargeImageList = images;
                for (int itemIndex = 1; itemIndex <= LoadedImages.Count; itemIndex++)
                {
                    imageList.Items.Add(new ListViewItem($"Image (itemindex)", itemIndex-1));
                }
            }
        }

        private void button_navigation(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            if (clickedButton.Text.Equals("<"))
            {
                if (SelectedImageIndex > 0)
                { 
                    SelectedImageIndex--;
                    Image selectedImg = LoadedImages[SelectedImageIndex];
                    selectedImage.Image = selectedImg;
                    SelectedTheClickedItem(imageList, SelectedImageIndex);
                }
            }
            else
            {
                if (SelectedImageIndex < (LoadedImages.Count - 1))
                {
                    SelectedImageIndex++;
                    Image selectedImg = LoadedImages[SelectedImageIndex];
                    selectedImage.Image = selectedImg;
                    SelectedTheClickedItem(imageList, SelectedImageIndex);
                }
            }
        }
        private void SelectedTheClickedItem(ListView list, int index)
        {
            for (int item = 0; item < list.Items.Count; item++)
            {
                if (item == index)
                {
                    list.Items[item].Selected = true;
                }
                else
                {
                    list.Items[item].Selected = false;
                }
            }
        }
    }
}
