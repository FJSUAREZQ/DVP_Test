import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";


export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = '';

      if (error.error instanceof ErrorEvent) {
        // Error del lado del cliente (ej. red de conexión)
        errorMessage = `Error del cliente: ${error.error.message}`;
      } else {
        // Error del lado del servidor
        errorMessage = `Error del servidor [${error.status}]: ${error.message}`;

        // Mensaje según código HTTP
        switch (error.status) {
          case 400:
            console.warn('Solicitud incorrecta (400).');
            break;
          case 401:
            console.warn('No autorizado (401).');
            break;
          case 403:
            console.warn('Acceso prohibido (403).');
            break;
          case 404:
            console.warn('Recurso no encontrado (404).');
            break;
          case 500:
            console.warn('Error interno del servidor (500).');
            break;
          default:
            console.warn(`Error inesperado [${error.status}]: ${error.message}`);
            break;
        }
      }

      // Mostrar el mensaje general en consola
      console.error(errorMessage);

      // Propagar el error para que el componente pueda manejarlo si lo desea
      return throwError(() => new Error(errorMessage));
    })
  );
};
