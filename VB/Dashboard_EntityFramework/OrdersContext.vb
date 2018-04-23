Imports System.Data.Entity
Imports System

Namespace Dashboard_EntityFramework

    Partial Public Class OrdersContext
        Inherits DbContext

        Public Sub New()
            MyBase.New("name=OrdersContext")
        End Sub
        Public Overridable Property Orders() As DbSet(Of Order)
    End Class

    Partial Public Class Order
        Public Property OrderID() As Long
        Public Property CustomerID() As String
        Public Property EmployeeID() As Long?
        Public Property OrderDate() As Date?
        Public Property RequiredDate() As Date?
        Public Property ShippedDate() As Date?
        Public Property ShipVia() As Long?
        Public Property Freight() As Decimal?
        Public Property ShipName() As String
        Public Property ShipAddress() As String
        Public Property ShipCity() As String
        Public Property ShipRegion() As String
        Public Property ShipPostalCode() As String
        Public Property ShipCountry() As String
    End Class
End Namespace
