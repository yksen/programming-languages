function* fibonacci() {
    let previous = BigInt(0);
    let current = BigInt(1);
    while (true) {
        yield current;
        [previous, current] = [current, previous + current];
    }
}

function fibo() {
    this.previous = BigInt(0);
    this.current = BigInt(1);
}
fibo.prototype.next = function () {
    let result = { value: this.current, done: false };
    [this.previous, this.current] = [this.current, this.previous + this.current];
    return result;
}

function test_equal() {
    let ex1 = fibonacci();
    let ex2 = new fibo();
    for (let i = 0; i < 200; ++i) {
        let val1 = ex1.next().value;
        let val2 = ex2.next().value;
        console.log(val1 == val2, val1, val2);
    }

    ex1 = fibonacci();
    ex2 = new fibo();
    setInterval(() => {
        let val1 = ex1.next().value;
        let val2 = ex2.next().value;
        console.log(val1 == val2, val1, val2);
    }, 500);
}

test_equal();