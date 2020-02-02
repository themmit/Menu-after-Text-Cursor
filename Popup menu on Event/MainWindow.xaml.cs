using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Popup_menu_on_Event {

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            msg.ContextMenuOpening += new ContextMenuEventHandler(richTextBox_ContextMenuOpening);
        }

        private void msg_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.RightCtrl) {
                richTextBox_ContextMenuOpening(sender, null);
                e.Handled = true;
                try {
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // This method is intended to listen for the ContextMenuOpening event from a RichTextBox.
        // It will position the custom context menu at the end of the current selection.
        void richTextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e) {
            // Sender must be RichTextBox.
            RichTextBox rtb = sender as RichTextBox;
            if (rtb == null) return;

            ContextMenu contextMenu = rtb.ContextMenu;
            contextMenu.PlacementTarget = rtb;

            // This uses HorizontalOffset and VerticalOffset properties to position the menu,
            // relative to the upper left corner of the parent element (RichTextBox in this case).
            contextMenu.Placement = PlacementMode.RelativePoint;

            // Compute horizontal and vertical offsets to place the menu relative to selection end.
            TextPointer position = rtb.Selection.End;

            if (position == null) return;

            Rect positionRect = position.GetCharacterRect(LogicalDirection.Forward);
            contextMenu.HorizontalOffset = positionRect.X;
            contextMenu.VerticalOffset = positionRect.Y;

            // Finally, mark the event has handled.
            contextMenu.IsOpen = true;
            if (e != null) {
                e.Handled = true;
            }
        }
    }
}
