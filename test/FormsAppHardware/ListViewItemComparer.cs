using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsAppHardware
{
    class ListViewItemComparer : IComparer
    {
        private int _colunbIndex;
        public int ColumnIndex
        {
            get
            {
                return _colunbIndex;
            }
            set
            {
                _colunbIndex = value;
            }
        }

        private SortOrder _sortDirection;
        public SortOrder SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                _sortDirection = value;
            }
        }

        public ListViewItemComparer()
        {
            _sortDirection = SortOrder.None;
        }
        public int Compare(object x, object y)
        {
            ListViewItem listViewItemX = x as ListViewItem;
            ListViewItem listViewItemY = y as ListViewItem;

            int result;

            switch (_colunbIndex)
            {
                case 0:
                    result = string.Compare(listViewItemX.SubItems[_colunbIndex].Text,
                        listViewItemY.SubItems[_colunbIndex].Text, false);
                    break;

                case 1:
                    double valueX = double.Parse(listViewItemX.SubItems[_colunbIndex].Text);
                    double valueY = double.Parse(listViewItemY.SubItems[_colunbIndex].Text);

                    result = valueX.CompareTo(valueY);
                    break;

                default:
                    result = string.Compare(listViewItemX.SubItems[_colunbIndex].Text,
                        listViewItemY.SubItems[_colunbIndex].Text, false);
                    break;
            }
            if (_sortDirection == SortOrder.Descending)
            {
                return -result;
            }
            else
            {
                return result;
            }
        }
    }
}
