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

const handler = {
    set: function (target, property, value) {
        if (property.startsWith('a')) {
            target.a(parseInt(property.substring(1)), value);
        } else if (property.startsWith('sum')) {
            target.sum(parseInt(property.substring(3)), value);
        } else if (property == 'r') {
            target.r(value);
        }
        return true;
    },
    get: function (target, property) {
        if (property.startsWith('a')) {
            return target.a(parseInt(property.substring(1)));
        } else if (property.startsWith('sum')) {
            return target.sum(parseInt(property.substring(3)));
        } else if (property == 'r') {
            return target.r();
        }
    }
}
let object = new ArithmeticSequence();

seq = new Proxy(object, handler);
seq.a5 = 7;
seq.a8 = 13;

for (let i = 1; i <= 8; i++) {
    process.stdout.write(seq['a' + i] + ' ');
}
console.log();

console.log(seq.a3 + seq.a7);
console.log(seq.sum3);