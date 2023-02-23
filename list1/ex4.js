class Square {
    constructor(a) { this.a = a; }
    get side() { return this.a; }
    set side(a) { this.a = a; }
    get area() { return this.a * this.a; }
    set area(a) { this.a = Math.sqrt(a); }
    get circumference() { return 4 * this.a; }
    set circumference(l) { this.a = l / 4; }
    toString() { return "a = ${ this.side } \nL = ${ this.circumference } \nP = ${ this.area } \n"; }
}

class Circle {
    constructor(r) { this.r = r; }
    get radius() { return this.radius; }
    set radius(r) { this.radius = r; }
    get diameter() { return 2 * this.r; }
    set diameter(d) { this.r = d / 2; }
    get area() { return Math.PI * this.r ** 2; }
    set area(a) { this.r = Math.sqrt(a / Math.PI); }
    get circumference() { return 2 * Math.PI * this.r; }
    set circumference(l) { this.r = l / (2 * Math.PI); }
    toString() { return ""; }
}

let s = new Square(1);
console.log(s);
s.side = 12;
console.log(s);

// var c = new Circle(4);
// console.log(c);
// c.radius = 6;