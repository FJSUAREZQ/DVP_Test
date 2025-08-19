import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { FacturaFacade } from '../../Servicios/factura-Facade.service';
import { ClientesCombosModel } from '../../Modelos/ClientesCombos.Model';
import { ProductosCombosModel } from '../../Modelos/ProductosCombos.Model';
import { FacturaCrearModel } from '../../Modelos/Factura-Crear/Factura-Crear.Model';
import { FacturaDetalleLineaModel } from '../../Modelos/Factura-Crear/Factura-DetalleLinea.Model';

@Component({
  selector: 'app-factura-crear',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './factura-Crear.page.html',
})
export class CrearFacturaPage {
  private facade = inject(FacturaFacade);//Inyectar el servicio de FacturaFacade
  private fb = inject(NonNullableFormBuilder);//Igual a FormBuilder, pero no permite valores nulos

  // Formulario
  form = this.fb.group({
    numeroFactura: this.fb.control('', { validators: [Validators.required] }),
    clienteId: this.fb.control<number | null>(null, { validators: [Validators.required] })
  });

  // Estado de los elementos de la pagina
  clientes = signal<ClientesCombosModel[]>([]);
  productosCombo = signal<ProductosCombosModel[]>([]);
  detalles = signal<FacturaDetalleLineaModel[]>([]);// Detalles sin info adicional como nombre o imagen para enviar al backend

  subtotal = signal(0);
  impuestos = signal(0);
  total = signal(0);

  error = signal<string | null>(null); //gestionar errores en la página

  // Signal para manejar nombre e imagen de los productos en las líneas y que no hacen parte del modelo a enviar al backend
  detallesConInfo = signal<(FacturaDetalleLineaModel & { nombre: string; imagen: string })[]>([]);

  constructor() {
    this.facade.traerClientesCombo().subscribe((data) => {
      this.clientes.set(data);
    });

    this.facade.traerProductosCombo().subscribe((data) => {
      this.productosCombo.set(data);
    });
  }

  // Método para agregar una línea de detalle al formulario
  agregarLinea(event: Event): void {
    const id = Number((event.target as HTMLSelectElement).value);// Obtener el ID del producto seleccionado
    
    if (!id) return;

    const index = this.detallesConInfo().findIndex(d => d.productoId === id);// Verificar si el producto ya existe en el detalle

    if(index > -1){
      alert(`Producto ${id} ya existe en el detalle. Por favor actualice la cantidad en lugar de agregarlo nuevamente.`);
      return;
    }else{
      this.facade.buscarProductosById(id).subscribe({
      next: (producto) => {
        // Crear una nueva línea de detalle con la información del producto addionando nombre e imagen
        const nuevaLinea: FacturaDetalleLineaModel & {
          nombre: string;
          imagen: string;
        } = {
          productoId: producto.productoId,
          cantidad: 1,
          precioUnitario: producto.precioUnitario,
          totalLinea: producto.precioUnitario,
          nombre: producto.nombre, // nuevo
          imagen: producto.imagenUrl ?? "", // nuevo
        };

        this.detalles.update((det) => [...det, nuevaLinea]);// Agregar la nueva línea al detalle sin info adicional para enviar al backend
        this.detallesConInfo.update((det) => [...det, nuevaLinea]); // Agregar la nueva línea al detalle con info adicional para mostrar en la UI
        this.error.set(null); // Limpiar errores
        this.recalcular(); // Recalcular valores
      },
      error: () => this.error.set('Error al cargar producto'),
    });
    }
  }

  // Método para actualizar la cantidad de una línea de detalle
  actualizarCantidad(linea: FacturaDetalleLineaModel, cantidad: number): void {
    this.detalles.update((det) =>
      det.map((l) =>
        l.productoId === linea.productoId
          ? { ...l, cantidad, totalLinea: cantidad * l.precioUnitario }
          : l
      )
    );

    this.detallesConInfo.update((det) =>
      det.map((l) =>
        l.productoId === linea.productoId
          ? { ...l, cantidad, totalLinea: cantidad * l.precioUnitario }
          : l
      )
    );

    this.recalcular();
  }

  // Método para eliminar una línea de detalle
  eliminarLinea(productoId: number) {
    this.detalles.set(
      this.detalles().filter((l) => l.productoId !== productoId)
    );
    this.detallesConInfo.set(
      this.detallesConInfo().filter((l) => l.productoId !== productoId)
    );
    this.recalcular();
  }

  // Método para recalcular subtotal, impuestos y total
  private recalcular(): void {
    const subtotal = this.detalles().reduce((s, l) => s + l.totalLinea, 0);
    this.subtotal.set(subtotal);
    this.impuestos.set(subtotal * 0.19);
    this.total.set(subtotal * 1.19);
  }

  // Método para iniciar un nuevo formulario, limpiando todos los campos y señales
  nuevo(): void {
    this.form.reset({
      numeroFactura: '',
      clienteId: null,
    });
    this.detalles.set([]);
    this.detallesConInfo.set([]);
    this.subtotal.set(0);
    this.impuestos.set(0);
    this.total.set(0);
    this.error.set(null);
  }

  // Métodos para obtener los valores actuales y enviarlos al backend
  guardar(): void {
    if (this.form.invalid || this.detalles().length === 0) {
      this.error.set('Complete el formulario y agregue al menos un producto.');
      return;
    }

    // Crear el objeto factura a enviar al backend
    const factura: FacturaCrearModel = {
      numeroFactura: this.form.controls.numeroFactura.value,
      clienteId: this.form.controls.clienteId.value!,
      usuarioId: 1,
      subtotal: this.subtotal(),
      impuestos: this.impuestos(),
      total: this.total(),
      detalles: this.detalles(),
    };

    // Llamar al servicio para crear la factura
    this.facade.crearFactura(factura).subscribe(() => {
      this.error.set(null);
      this.form.reset();
      this.detalles.set([]);
      this.detallesConInfo.set([]);
      this.subtotal.set(0);
      this.impuestos.set(0);
      this.total.set(0);
    });
  }
}
