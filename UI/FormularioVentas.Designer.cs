namespace ProyectoFinalProgramacionV.UI
{
    partial class FormularioVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            //
            // lblCliente
            //
            this.lblCliente.Location = new System.Drawing.Point(20, 20);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(70, 23);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente:";
            //
            // cmbClientes
            //
            this.cmbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientes.Location = new System.Drawing.Point(95, 18);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(320, 23);
            this.cmbClientes.TabIndex = 1;
            //
            // lblProducto
            //
            this.lblProducto.Location = new System.Drawing.Point(20, 55);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(70, 23);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "Producto:";
            //
            // cmbProductos
            //
            this.cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductos.Location = new System.Drawing.Point(95, 53);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(320, 23);
            this.cmbProductos.TabIndex = 3;
            //
            // lblCantidad
            //
            this.lblCantidad.Location = new System.Drawing.Point(425, 55);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(70, 23);
            this.lblCantidad.TabIndex = 4;
            this.lblCantidad.Text = "Cantidad:";
            //
            // txtCantidad
            //
            this.txtCantidad.Location = new System.Drawing.Point(495, 53);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(60, 23);
            this.txtCantidad.TabIndex = 5;
            //
            // btnAgregarProducto
            //
            this.btnAgregarProducto.Location = new System.Drawing.Point(95, 90);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(180, 30);
            this.btnAgregarProducto.TabIndex = 6;
            this.btnAgregarProducto.Text = "Agregar producto a la venta";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.BtnAgregarProducto_Click);
            //
            // dgvDetalle
            //
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.Location = new System.Drawing.Point(20, 135);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(540, 220);
            this.dgvDetalle.TabIndex = 7;
            //
            // lblTotal
            //
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(20, 365);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 25);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total: ₡0.00";
            //
            // btnRegistrarVenta
            //
            this.btnRegistrarVenta.Location = new System.Drawing.Point(340, 400);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(120, 30);
            this.btnRegistrarVenta.TabIndex = 9;
            this.btnRegistrarVenta.Text = "Registrar venta";
            this.btnRegistrarVenta.UseVisualStyleBackColor = true;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.BtnRegistrarVenta_Click);
            //
            // btnRegresar
            //
            this.btnRegresar.Location = new System.Drawing.Point(460, 400);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(100, 30);
            this.btnRegresar.TabIndex = 10;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.BtnRegresar_Click);
            //
            // FormularioVentas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnRegistrarVenta);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cmbClientes);
            this.Controls.Add(this.lblCliente);
            this.Name = "FormularioVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Venta";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRegistrarVenta;
        private System.Windows.Forms.Button btnRegresar;
    }
}