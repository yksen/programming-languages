function bigger(a) {
    return (x) => {
        return x > a;
    }
}

function smaller(a) {
    return (x) => {
        return x < a;
    }
}

function between(a, b) {
    return (x) => {
        return x > a && x < b;
    }
}

function test(array, f, a, b) {
    console.log(array);
    console.log("filter", array.filter(f(a, b)));
    console.log("find", array.find(f(a, b)));
    console.log("findIndex", array.findIndex(f(a, b)));
    console.log("every", array.every(f(a, b)));
    console.log("some", array.some(f(a, b)));
}

a = [2, 31, 5, 3, 6];
test(a, bigger, 3);
test(a, smaller, 6);
test(a, between, 4, 7);

b = [1, 3, 5, 7, 11, 19];
test(b, bigger, 19);
test(b, smaller, 4);
test(b, between, 3, 12);