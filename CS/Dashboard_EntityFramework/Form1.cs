using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;
using DevExpress.DataAccess.EntityFramework;

namespace Dashboard_EntityFramework {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            InitializeDashboard();          
        }

        public void InitializeDashboard() {
            Dashboard dashboard = new Dashboard();
            DashboardEFDataSource efDataSource = new DashboardEFDataSource();
            efDataSource.ConnectionParameters =
                new EFConnectionParameters(typeof(OrdersContext));
            dashboard.DataSources.Add(efDataSource);

            PivotDashboardItem pivot = new PivotDashboardItem();
            pivot.DataMember = "Orders";
            pivot.DataSource = dashboard.DataSources[0];
            pivot.Rows.AddRange(new Dimension("ShipCountry"), new Dimension("ShipCity"));
            pivot.Columns.Add(new Dimension("OrderDate"));
            pivot.Values.Add(new Measure("Freight"));

            ChartDashboardItem chart = new ChartDashboardItem();
            chart.DataSource = dashboard.DataSources[0];
            chart.DataMember = "Orders";
            chart.Arguments.Add(new Dimension("OrderDate", DateTimeGroupInterval.Year));
            chart.Panes.Add(new ChartPane());
            SimpleSeries freightSeries = new SimpleSeries(SimpleSeriesType.Bar);
            freightSeries.Value = new Measure("Freight");
            chart.Panes[0].Series.Add(freightSeries);

            dashboard.Items.AddRange(pivot, chart);
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
