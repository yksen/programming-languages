function* fibonacci() {
    let previous = BigInt(0);
    let current = BigInt(1);
    while (true) {
        yield current;
        [previous, current] = [current, previous + current];
    }
}

let fib = fibonacci();
for (let i = 0; i < 200; ++i)
    console.log(fib.next().value);

fib = fibonacci();
setInterval(() => console.log(fib.next().value), 500);