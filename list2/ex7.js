class ArithmeticSequence {
    constructor() {
        this.ax = [];
        this.sx = [];
    }

    a = (i, x) => {
        if (x == undefined) { return this.a1 + this.rv * (i - 1); }
        else {
            this.ax.push({ i, x });
            if (this.ax.length == 2) {
                this.rv = (this.ax[1].x - this.ax[0].x) / (this.ax[1].i - this.ax[0].i);
                this.a1 = this.ax[0].x - this.rv * (this.ax[0].i - 1);
            } else if (this.rv != undefined) {
                this.a1 = x - this.rv * (i - 1);
            } else if (this.sx.length == 1 && this.ax.length == 1) {
                this.rv = (2 * this.sx[0].x - 2 * this.ax[0].x * this.sx[0].i) /
                (this.sx[0].i * (this.sx[0].i - 2 * this.ax[0].i + 1));
                this.a1 = (2 * this.sx[0].x / this.sx[0].i - this.rv * (this.sx[0].i - 1)) / 2;
            }
        }
    }
    sum = (i, x) => {
        if (x == undefined) { return (this.a1 + this.a(i)) * i / 2; }
        else {
            this.sx.push({ i, x });
            if (this.sx.length == 2) {
                this.rv = (2 * this.sx[1].i * this.sx[0].x - 2 * this.sx[0].i * this.sx[1].x) /
                    (this.sx[0].i ** 2 * this.sx[1].i - this.sx[0].i * this.sx[1].i ** 2);
                this.a1 = (2 * this.sx[0].x / this.sx[0].i - this.rv * (this.sx[0].i - 1)) / 2;
            } else if (this.rv != undefined) {
                this.a1 = (2 * x / i - this.rv * (i - 1)) / 2;
            }
        }
    }
    r = (x) => {
        if (x == undefined) { return this.rv; }
        else {
            this.rv = x;
            if (this.ax.length == 1) {
                this.a1 = this.ax[0].x - this.rv * (this.ax[0].i - 1);
            } else if (this.sx.length == 1) {
                this.a1 = (2 * this.sx[0].x / this.sx[0].i - this.rv * (this.sx[0].i - 1)) / 2;
            }
        }
    }
}

let t = new ArithmeticSequence();
t.r(2);
t.a(7, 9);
console.log(t.a(1), t.a(2), t.a(3), t.a(4), t.a(5));

t = new ArithmeticSequence();
t.a(3, 8);
t.a(5, 2);
console.log(t.a(1), t.a(2), t.a(3), t.a(4), t.a(5));

t = new ArithmeticSequence();
t.sum(5, 15);
t.r(1);
console.log(t.a(1), t.a(2), t.a(3), t.a(4), t.a(5));

t = new ArithmeticSequence();
t.sum(3, 12);
t.sum(6, 42);
console.log(t.a(1), t.a(2), t.a(3), t.a(4), t.a(5));

t = new ArithmeticSequence();
t.sum(5, 20);
t.a(2, 13);
console.log(t.a(1), t.a(2), t.a(3), t.a(4), t.a(5));