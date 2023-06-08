# creational 

## Factory method 

**Factory method** could be used to deal with the problem of creating objects without having to specify the exact class of the object that will be created. 

![factory-method](https://upload.wikimedia.org/wikipedia/commons/4/43/W3sDesign_Factory_Method_Design_Pattern_UML.jpg)

## Abstract factory 

The purpose of the **Abstract Factory** is to provide an interface for creating families of related objects, without specifying concrete classes.

![abstract-factory](https://upload.wikimedia.org/wikipedia/commons/a/aa/W3sDesign_Abstract_Factory_Design_Pattern_UML.jpg)

The example above could be confused with another design patter - **factory method**, because this implementation could be considered as an extension of factory method. 
Plus if there's necessety to implement more specific logic while creating some of the products (when there's more than one methods specific for each of the products), this approach could violate Single-responsibility principle. 

But there is another implementation of abstract factory: 

![example-abstractfactory](https://www.javatpoint.com/images/designpattern/abstractfactory.jpg)

In the example above, it seems like extending the interface `AbstractFactory` by classes `BankFactory` and `LoanFactory` could violate **Liskov substitution principle**. 
However this example allows you to be more flexible in terms of creating more specific products. 
And it's better to use factory method for creating banks and loans separately. 

I suppose that you can **abstract factory principle** in a little bit different context. 
For instance, if you assume that creating similar products (e.g. vihicle: truck, car etc) requires specific logic for each of the product, then **abstract factory** could be an excellent choice. 

And if you need some more specific logic for creating a car (e.g. if BMW, Audi, Lamborghini could be created in their own way), then you can use **abstract factory** one more time to create more specific products on a lower level. 

## Builder 

The intent of the **Builder design pattern** is to separate the construction of a complex object from its representation. By doing so, the same construction process can create different representations.

![builder](https://upload.wikimedia.org/wikipedia/commons/8/87/W3sDesign_Builder_Design_Pattern_UML.jpg)

## Singleton

**Singleton pattern** is a software design pattern that restricts the instantiation of a class to a singular instance. 

![singleton](https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Singleton_UML_class_diagram.svg/330px-Singleton_UML_class_diagram.svg.png)

## Protorype 

**Prototype** is used when the type of objects to create is determined by a prototypical instance, which is cloned to produce new objects. 

![prototype](https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Prototype_UML.svg/900px-Prototype_UML.svg.png)

There's [C++ example](https://en.wikipedia.org/wiki/Prototype_pattern#Example) where concrete prototype has two types of constructors: 
1. for creating by external classes; 
2. for creating using `clone()` method. 

```C++
class Door : public MapSite {
public:
  Door(Room* r1 = nullptr, Room* r2 = nullptr)
    :room1(r1), room2(r2) {}
  Door(const Door& other)
    :room1(other.room1), room2(other.room2) {}
  virtual void enter() {}
  virtual Door* clone() const {
    return new Door(*this);
  }
  virtual void initialize(Room* r1, Room* r2) {
    room1 = r1;
    room2 = r2;
  }
  Door& operator=(const Door&) = delete;
private:
  Room* room1;
  Room* room2;
};
```
