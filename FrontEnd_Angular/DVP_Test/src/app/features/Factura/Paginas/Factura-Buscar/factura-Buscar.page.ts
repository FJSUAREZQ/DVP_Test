import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, NonNullableFormBuilder, Validators } from '@angular/forms';
import { FacturaFacade } from '../../Servicios/factura-Facade.service';
import { ClientesCombosModel } from '../../Modelos/ClientesCombos.Model';
import { FacturaModel } from '../../Modelos/Factura.Model';

@Component({
  selector: 'app-factura-buscar',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './factura-Buscar.page.html'
})
export class BuscarFacturaPage {
  private facade = inject(FacturaFacade);
  private fb = inject(NonNullableFormBuilder); //similar to FormBuilder, pero no permite valores nulos

  // Formulario reactivo
  form = this.fb.group({
    tipoBusqueda: this.fb.control<'cliente' | 'numero'>('cliente', { validators: [Validators.required] }),
    clienteId: this.fb.control<number | null>(null, { validators: [Validators.required] }),
    numeroFactura: this.fb.control<string>({ value: '', disabled: true })
  });

  // Signals para manejar los estados
  clientes = signal<ClientesCombosModel[]>([]);
  facturas = signal<FacturaModel[]>([]);
  error = signal<string | null>(null);

  constructor() {
    // cargar clientes para el combo
    this.facade.traerClientesCombo().subscribe(data => {
      this.clientes.set(data);
  });

    // reaccionar al cambio de tipo de búsqueda
    this.form.controls.tipoBusqueda.valueChanges.subscribe(tipo => {
      if (tipo === 'cliente') {
        this.form.controls.clienteId.enable();
        this.form.controls.numeroFactura.setValue('');
        this.form.controls.numeroFactura.disable();
      } 
      else {
        this.form.controls.clienteId.disable();
        this.form.controls.numeroFactura.enable();
      }
      this.facturas.set([]);
      this.error.set(null);
    });
  }

 // Método para buscar facturas 
buscar(): void {
  //obtener los valores del formulario incluso de los controles deshabilitados con getRawValue()
  const { tipoBusqueda, clienteId, numeroFactura } = this.form.getRawValue();

  if (tipoBusqueda === 'cliente') {
    if (!clienteId || clienteId <= 0) {
      this.error.set('Seleccione un cliente válido.');
      return;
    }

    this.facade.buscarFacturasPorCliente(clienteId).subscribe(res => {
      this.facturas.set(res);
      if (res.length === 0) {
        this.error.set('No se encontraron facturas para ese cliente.');
      } else {
        this.error.set(null);
      }
    });

  } else {
    if (!numeroFactura?.trim()) {
      this.error.set('Ingrese un número de factura.');
      return;
    }

    this.facade.buscarFacturaPorNumero(numeroFactura).subscribe(res => {
      this.facturas.set(res);
      if (res.length === 0) {
        this.error.set('Factura no encontrada.');
      } else {
        this.error.set(null);
      }
    });
  }
}


}
