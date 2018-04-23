Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports System.Data.Entity
Imports DevExpress.XtraEditors

Namespace Dashboard_EntityFramework
    Partial Public Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            InitializeDashboard()
        End Sub

        Public Sub InitializeDashboard()
            Dim dashboard As New Dashboard()
            Using context As New OrdersContext()
                context.Orders.Load()
                dashboard.AddDataSource("Data Source 1", context.Orders.Local.ToBindingList())
            End Using
            Dim pivot As New PivotDashboardItem()
            pivot.DataSource = dashboard.DataSources(0)
            pivot.Rows.AddRange(New Dimension("ShipCountry"), New Dimension("ShipCity"))
            pivot.Columns.Add(New Dimension("OrderDate"))
            pivot.Values.Add(New Measure("Freight"))

            Dim chart As New ChartDashboardItem()
            chart.DataSource = dashboard.DataSources(0)
            chart.Arguments.Add(New Dimension("OrderDate", DateTimeGroupInterval.QuarterYear))
            chart.Panes.Add(New ChartPane())
            Dim freightSeries As New SimpleSeries(SimpleSeriesType.Bar)
            freightSeries.Value = New Measure("Freight")
            chart.Panes(0).Series.Add(freightSeries)

            dashboard.Items.AddRange(pivot, chart)
            dashboardViewer1.Dashboard = dashboard
        End Sub
    End Class
End Namespace
