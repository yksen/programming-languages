function* divisors(n) {
    for (let i = 1; i <= n; i++) {
        if (n % i === 0) {
            yield i;
        }
    }
}

function print(gen) {
    for (let x of gen)
        console.log(x);
}

function print2(gen) {
    while (!(x = gen.next()).done)
        console.log(x.value);
}