function* divisors(n) {
    for (let i = 1; i <= n; i++) {
        if (n % i === 0) {
            yield i;
        }
    }
}

function* primes(n) {
    for (let i = 2; i <= n; i++) {
        let divisor_count = 0;
        for (let j of divisors(i)) {
            divisor_count++;
        }
        if (divisor_count === 2) {
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