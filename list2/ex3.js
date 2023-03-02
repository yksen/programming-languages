function* fibonacci() {
    let previous = BigInt(0);
    let current = BigInt(1);
    while (true) {
        yield current;
        [previous, current] = [current, previous + current];
    }
}

function* fragment(iter, skip, limit = 1) {
    for (let i = 0; i < skip; ++i)
        iter.next();
    for (let i = 0; i < limit; ++i)
        yield iter.next().value;
}

for (let x of fragment(fibonacci(), 100, 3))
    console.log(x);