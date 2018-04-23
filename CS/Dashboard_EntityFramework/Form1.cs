using System.Windows.Forms;
using DevExpress.DashboardCommon;
using System.Data.Entity;
using DevExpress.XtraEditors;

namespace Dashboard_EntityFramework {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            InitializeDashboard();          
        }

        public void InitializeDashboard() {
            Dashboard dashboard = new Dashboard();
            using (OrdersContext context = new OrdersContext()) {
                context.Orders.Load();
                dashboard.AddDataSource("Data Source 1", context.Orders.Local.ToBindingList());
            }
            PivotDashboardItem pivot = new PivotDashboardItem();
            pivot.DataSource = dashboard.DataSources[0];
            pivot.Rows.AddRange(new Dimension("ShipCountry"), new Dimension("ShipCity"));
            pivot.Columns.Add(new Dimension("OrderDate"));
            pivot.Values.Add(new Measure("Freight"));

            ChartDashboardItem chart = new ChartDashboardItem();
            chart.DataSource = dashboard.DataSources[0];
            chart.Arguments.Add(new Dimension("OrderDate", DateTimeGroupInterval.QuarterYear));
            chart.Panes.Add(new ChartPane());
            SimpleSeries freightSeries = new SimpleSeries(SimpleSeriesType.Bar);
            freightSeries.Value = new Measure("Freight");
            chart.Panes[0].Series.Add(freightSeries);

            dashboard.Items.AddRange(pivot, chart);
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
