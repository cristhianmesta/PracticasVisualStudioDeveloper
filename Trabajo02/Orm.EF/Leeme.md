# Entity Framework

Es un ORM (Object Relational Mapper) que permite el desarrollo de aplicaciones que interactuan con datos.


## ¿Por qué usarlo?

Nos permite explotar la potencia de un lenguaje orientado a objetos para escribir aplicaciones que interactúan con datos. Podemos usar **Linq** para poder realizar consultas.


## Enfoques de Desarrollo

### Database First
Este enfoque es el indicado cuando se tiene que desarrollar sobre una base de datos heredada.

Es posible usar EF para poder hacer ingenieria reversa y generar el modelo de clases para el modelo de la base de datos.
Usando el EF Designer, es que todos los cambios se hacen primero en la Base de Datos.

### Code First 
Este enfoque primero se genera el modelo de clases, con el cual es posible generar la base de datos.





# Usando EF para el Acceso a Datos

## El Modelo
Describe el dominio de negocio de la aplicación que estamos desarrollando.

### Entidad
Es un objeto, cuya principal caracteristica es tener una identidad que lo diferencia del resto de los objetos del sistema.

> Ej. Empleado, Proveedor, Producto, Album, Artista, etc.

El identificador debe estar compuesto por un único atributo (Llave primaria de una sola columna)

``` csharp
class Empleado 
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    // .... mas atributos
}
```

### Value Object
Son objetos que no necesitan tener un identificador único. Dos objetos son iguales si todas sus propiedades son iguales.

> Ej. Direccion, Color, Posición, etc.

``` csharp
class Direccion 
{
    public string Ciudad {get; set;}
    public string Direccion {get; set;}
    public int Numero {get; set;}
    public string Interior {get; set;}
}
```

### Relaciones
Las relaciones en el modelo se pueden representar de la siguiente forma.

***Uno a Muchos (One To Many):*** No es necesario que ambas clases tengan una propiedad que representa la otra clase que referencia (Propiedad de Navegación), poner la referencia a la otra clase donde es realmente neccesario.

***Muchos a Muchos (Many to Many):*** No es necesario tener una clase intermedia si solo se va a tener Claves foráneas, para eso debemos añadir colecciones en ambas clases de la relación.


### Requisitos 
El modelo debe cumplir ciertos requisitos para poder ser utilizado con EF.

+ Las clases pueden tener varios constructores con parámetros, pero al menos debe tener un constructor sin parametros.
+ Adicionalmente a las Propiedades de Navegación se recomienda tambien crear propiedades de clave foránea que deben cumplir la siguiente convencion: [Nombre Entidad]+Id


## La Base de Datos

### Consideraciones

+ La correspondencia existente entre tablas y entidades es normalmente de 1 a 1 (La herencia en un caso especial)
+ La correspondencia entre columnas y propiedades es de 1 a 1 (Value Objects son un caso especial)

### Claves Primarias
+ Se recomienda que las tablas tenga una clave primaria ID al igual que las entidades.
+ Por defecto EF sólo permite el uso de columnas Identity para la asignación secuencial de Ids. Es posible la implementación de otras estrategias de generación de Ids, pero estas deben codificarse manualmente.



## Mapeo del Modelo

### Convenciones
Es el mapeo por defecto que el enfoque Code First de EF usa, el cual se basa en el nombre de las clases y atributos.

#### Ventajas
Poco esfuerzo requerido, facil de mantener.

#### Desventajas
+ Dificil de Mantener si no existen ciertos estandares en la base de datos.
+ No es posible usar convenciones personalizadas.


| Convenciones por defecto
|--------------
| La tabla tiene el mismo nombre de la entidad o su pluralización
| El identificador es una propiedad llamada Id
| Todas las columnas tienen su correspondiente propiedad del mismo nombre
| Las columnas de Clave foránea (FK) deben ser Entidad_Id. Salvo que exista una Propiedad de Clave foránea en la clase, la columna debe tener el mismo nombre de la propiedad.
| Las columnas correspondientes a Value Objects se deben llamar [ValueObject]_[Propiedad].


### Fluente

EF tienen un API fluente que nos permite configurar como sera el mapeo entre nuestro modelo y la base de datos.

```csharp
class EmpleadoMapeo : EntityTypeConfiguration<Empleado>
{
    public EmpleadoMapeo()
    {
        HasKey(p=> p.EmpleadoId);
        Property(p=> p.Nombre);
        //... las demas columnas que no cumplem la convención.
    }
}
```

#### Ventajas

+ Es Type-Safe
+ Soporta Escenarios que pueden ser realizados mediante convenciones.

#### Desventajas

+ Cada Mapeo requiere un mantenimiento constante.

