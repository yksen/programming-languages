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
        for (let _ of divisors(i)) {
            divisor_count++;
        }
        if (divisor_count === 2) {
            yield i;
        }
    }
}

function* factorize(n) {
    for (let i of primes(n)) {
        while (n % i === 0) {
            yield i;
            n /= i;
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

function glue_together(gen, glue = ',') {
    let result = "";
    for (let x of gen) {
        result += x + glue;
    }
    return result;
}

function sum(gen) {
    let result = 0;
    for (let x of gen) {
        result += x;
    }
    return result;
}

function product(gen) {
    let result = 1;
    for (let x of gen) {
        result *= x;
    }
    return result;
}

console.log("> Array.from");
console.log(Array.from(divisors(10)));
console.log(Array.from(primes(10)));
console.log(Array.from(factorize(10)));

console.log("> print");
print(divisors(10));
print(primes(10));
print(factorize(10));

console.log("> print2");
print2(divisors(10));
print2(primes(10));
print2(factorize(10));

console.log("> glue_together");
console.log(glue_together(divisors(10)));
console.log(glue_together(primes(10)));
console.log(glue_together(factorize(10)));

console.log("> sum");
console.log(sum(divisors(10)));
console.log(sum(primes(10)));
console.log(sum(factorize(10)));

console.log("> product");
console.log(product(divisors(10)));
console.log(product(primes(10)));
console.log(product(factorize(10)));