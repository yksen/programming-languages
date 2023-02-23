function fa(a, b) {
    return b - a;
}

function fb(a, b) {
    return a % 10 - b % 10;
}

function fc(a, b) {
    return a.toString().slice(-2) - b.toString().slice(-2);
}

function fd(a, b) {
    return sumOfDigits(a) - sumOfDigits(b);
}

function sumOfDigits(n) {
    let sum = 0;
    while (n > 0) {
        sum += n % 10;
        n = Math.floor(n / 10);
    }
    return sum;
}

function test(array) {
    console.log("Test", array);
    console.log(array.sort(fa));
    console.log(array.sort(fb));
    console.log(array.sort(fc));
    console.log(array.sort(fd));
}

a = [1, 2, 3, 271, 12313, 123, 21313, 541, 42];
test(a);

b = [5, 9, 321, 5, 16, 6666, 792, 90009];
test(b);