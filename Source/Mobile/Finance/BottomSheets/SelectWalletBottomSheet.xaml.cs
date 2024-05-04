using DevExpress.Maui.Controls;
using Finance.Models;

namespace Finance.BottomSheets;

public partial class SelectWalletBottomSheet : BottomSheet {

    public SelectWalletBottomSheet() {
        InitializeComponent();
    }

    private void IndicatorButton_Clicked(object sender, EventArgs e) {
        Close();
    }
}