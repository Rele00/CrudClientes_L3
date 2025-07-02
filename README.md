# Práctica de CRUD - Gestión de Clientes

## Consigna

**Objetivo:** Desarrollar una aplicación web completa para la gestión de clientes utilizando Blazor Web App (Server) con .NET 8 y Entity Framework Core.

### Requerimientos Técnicos

- **Framework:** Blazor Web App (Server)
- **Autenticación:** Cuentas Individuales (Identity)
- **Base de datos:** SQL Server con Entity Framework Core
- **Arquitectura:** Separación por capas

### Estructura del Proyecto

```
ClientesCrud/
├── Data/
│   ├── Entities/          # Entidades del dominio
│   ├── Dtos/             # Objetos de transferencia de datos
│   └── Services/         # Servicios/Repositorios
├── Components/
│   ├── Shared/           # Componentes reutilizables
│   └── Pages/            # Páginas de la aplicación
```

### Funcionalidades Requeridas

1. **Listado de Clientes**
   - Mostrar todos los clientes activos
   - Botones de acción (Editar/Eliminar)
   - Mensaje cuando no hay registros

2. **Crear Cliente**
   - Formulario con validaciones
   - Campos: Nombre, Email, Teléfono, Dirección
   - Navegación después de guardar

3. **Editar Cliente**
   - Pre-cargar datos del cliente
   - Mismas validaciones que crear
   - Actualización en base de datos

4. **Eliminar Cliente**

### Modelo de Datos - Cliente

| Campo | Tipo | Restricciones |
|-------|------|--------------|
| Id | int | Primary Key, Identity |
| Nombre | string | Required, MaxLength(100) |
| Email | string | Required, Email, MaxLength(100), Unique |
| Telefono | string | Optional, Phone, MaxLength(15) |
| Direccion | string | Optional, MaxLength(200) |
| FechaCreacion | DateTime | Default: DateTime.Now |
| Activo | bool | Default: true |

### Validaciones Requeridas

- **Nombre:** Obligatorio, máximo 100 caracteres
- **Email:** Obligatorio, formato válido, único en base de datos
- **Teléfono:** Formato de teléfono válido (opcional)
- **Dirección:** Máximo 200 caracteres (opcional)

### Criterios de Evaluación

1. **Arquitectura (25%)**
   - Separación correcta en capas
   - Uso apropiado de DTOs
   - Implementación del patrón Repository

2. **Funcionalidad (35%)**
   - CRUD completo funcionando
   - Validaciones del lado cliente y servidor
   - Navegación correcta entre páginas

3. **Interfaz de Usuario (25%)**
   - Uso correcto de Bootstrap
   - Responsividad básica
   - Mensajes de confirmación y error

4. **Buenas Prácticas (15%)**
   - Uso de async/await
   - Manejo de excepciones básico
   - Código limpio y bien estructurado

### Entregables

- [ ] Proyecto completo funcionando
- [ ] Base de datos creada con migraciones
- [ ] Todas las funcionalidades CRUD operativas
- [ ] Validaciones implementadas
- [ ] Interfaz responsive con Bootstrap

### Tiempo Estimado
90 minutos (2 períodos de clase)

---

## Instrucciones Adicionales

- Solo usuarios autenticados pueden acceder al módulo de clientes
- Usar eliminación lógica (no física) para mantener integridad referencial
- Implementar confirmación JavaScript para eliminaciones
- Seguir las convenciones de nomenclatura de C#
- Usar ConfigureAwait(true) en llamadas async

## Notas Técnicas - Entity Framework

### Comandos de Migración

**Crear una nueva migración:**
```bash
dotnet ef migrations add NombreDeLaMigracion
```

**Aplicar migraciones a la base de datos:**
```bash
dotnet ef database update
```

**Ver el estado de las migraciones:**
```bash
dotnet ef migrations list
```

**Eliminar la última migración (si no se ha aplicado):**
```bash
dotnet ef migrations remove
```

### Secuencia Recomendada para este Proyecto

1. **Después de crear las entidades y modificar el DbContext:**
   ```bash
   dotnet ef migrations add AgregarClientes
   ```

2. **Aplicar la migración:**
   ```bash
   dotnet ef database update
   ```

3. **Verificar que la tabla se creó correctamente en SQL Server Management Studio**

### Comandos Adicionales Útiles

**Generar script SQL de la migración:**
```bash
dotnet ef migrations script
```

**Recrear la base de datos completamente:**
```bash
dotnet ef database drop
dotnet ef database update
```

> **Importante:** Asegúrate de tener instalado el paquete `Microsoft.EntityFrameworkCore.Tools` para poder usar estos comandos.