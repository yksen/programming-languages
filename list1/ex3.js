function sum(...args) {
    let sum = 0;
    args = args.flat(Infinity).filter((x) => { return Number.isInteger(x) });
    for (let x of args) {
        sum += Number(x);
    }
    return sum;
}

function test(...args) {
    console.log(sum(...args));
}

test(1, 2, 3, 10, [], 20, 30, "marek", { a: 4 });
test(1, 2, 3, [4, 5, "aa"], 10, "asda");
test(1, 2, 3, [4, 5, [5, 5]], 10);