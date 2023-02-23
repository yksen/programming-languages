class Square {
    constructor(a) { this.a = a; }
    get side() { return this.a; }
    set side(a) { this.a = a; }
    get area() { return this.a * this.a; }
    set area(a) { this.a = Math.sqrt(a); }
    get circumference() { return 4 * this.a; }
    set circumference(l) { this.a = l / 4; }
    toString() { return `a = ${this.side}\nA = ${this.area}\nL = ${this.circumference}`; }
}

class Circle {
    constructor(r) { this.r = r; }
    get diameter() { return 2 * this.r; }
    set diameter(d) { this.r = d / 2; }
    get area() { return Math.PI * this.r ** 2; }
    set area(a) { this.r = Math.sqrt(a / Math.PI); }
    get circumference() { return 2 * Math.PI * this.r; }
    set circumference(l) { this.r = l / (2 * Math.PI); }
    toString() { return `r = ${this.r}\nA = ${this.area}\nL = ${this.circumference}`; }
}

function checkCircleGettersAndSetters(circle) {
    circle.radius = 5;
    console.log(circle.toString());
    circle.diameter = 10;
    console.log(circle.toString());
    circle.area = 50;
    console.log(circle.toString());
    circle.circumference = 20;
    console.log(circle.toString());
}

checkCircleGettersAndSetters(new Circle(1));