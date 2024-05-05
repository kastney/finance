using DevExpress.Maui.CollectionView;
using DevExpress.Maui.Controls;

namespace Finance.BottomSheets;

public partial class SelectWalletBottomSheet : BottomSheet {

    public SelectWalletBottomSheet() {
        InitializeComponent();
    }

    private void IndicatorButton_Clicked(object sender, EventArgs e) {
        Close();
    }

    private void CollectionView_SelectionChanged(object sender, CollectionViewSelectionChangedEventArgs e) {
        Close();
    }
}