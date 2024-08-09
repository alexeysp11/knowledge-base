# SOLID 

Доступно на других языках: [English/Английский](solid.md), [Russian/Русский](solid.ru.md). 

**SOLID** - это совокупность, состоящая из 5 принципов, которые помогают облегчить проектирование, понимание, разработку и поддержку ПО, написанного в стиле ООП:
- **S**ingle-responsibility principle (рус. "Принцип единственной ответственности");
- **O**pen-closed principle (рус. "Принцип открытости-закрытости");
- **L**iskov substitution principle (рус. "Принцип подстановки Лисков");
- **I**nterface segregation principle (рус. "Принцип разделения интерфейса");
- **D**ependency-inversion principle (рус. "Принцип инверсии зависимости").

## Single-responsibility principle

Для каждого класса должно быть определено единственное назначение. 
Все ресурсы, необходимые для его осуществления, должны быть инкапсулированы в этот класс и подчинены только этой задаче.

### Плохо

![srp-bad](../img/solid/srp-bad.png)

### Лучше

![srp-better](../img/solid/srp-better.png)

## Open-closed principle

«программные сущности … должны быть открыты для расширения, но закрыты для модификации».

### Плохо

![ocp-bad](../img/solid/ocp-bad.png)

### Лучше

![ocp-better](../img/solid/ocp-better.png)

## Liskov substitution principle

«функции, которые используют базовый тип, должны иметь возможность использовать подтипы базового типа не зная об этом». 

### Плохо

![lsp-bad](../img/solid/lsp-bad.png)

### Лучше

Одно из решений, которое можно рассматривать в качестве решения проблемы принципа **LSP**, представлено по [данной ссылке](https://github.com/alexeysp11/mindbox-lib).

## Interface segregation principle

«много интерфейсов, специально предназначенных для клиентов, лучше, чем один интерфейс общего назначения».

### Плохо

![isp-bad](../img/solid/isp-bad.png)

### Лучше

![isp-better](../img/solid/isp-better.png)

## Dependency-inversion principle

«Зависимость на Абстракциях. Нет зависимости на что-то конкретное». 

### Плохо

![dip-bad](../img/solid/dip-bad.png)

### Лучше

![dip-better](../img/solid/dip-better.png)
