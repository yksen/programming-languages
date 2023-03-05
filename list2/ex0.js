class Range {
    constructor(a, b) {
        this.a = a;
        this.b = b;
    }

    *[Symbol.iterator]() {
        for (let i = this.a; i <= this.b; i++)
            yield i;
    }

    [Symbol.toPrimitive](hint) {
        return hint == "number"
            ? (this.a + this.b) * (this.b - this.a + 1) / 2
            : this.a + ".." + this.b;
    }
}

r = new Range(10, 15);
console.log(r);

for (let x of r)
    console.log(x);

console.log("suma(" + r + ")=" + +r);
console.log(Array.from(r));